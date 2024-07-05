using Microsoft.EntityFrameworkCore;
using UrlShortener.API.Data;
using UrlShortener.API.Extensions;
using UrlShortener.API.Models.Domain;
using UrlShortener.API.Models.Dtos;
using UrlShortener.API.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString(ApplicationDbContext.PostgreSqlConnectionStringConfigPath)));

builder.Services.AddScoped<IUniqueUrlCodeGenerator, DefaultUniqueUrlCodeGenerator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.MigrateDatabaseAsync();
}

app.MapPost("/api/shorten", async (
    ShortenUrlRequest request,
    ApplicationDbContext db,
    IUniqueUrlCodeGenerator uniqueUrlCodeGenerator,
    HttpContext httpContext) =>
{
    if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
    {
        return Results.BadRequest("Invalid URL supplied.");
    }

    var code = await uniqueUrlCodeGenerator.GenerateUniqueCodeForUrl();

    var shortenedUrl = new ShortenedUrl()
    {
        Code = code,
        LongUrl = request.Url,
        ShortUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/{code}",
        CreatedOnUtc = DateTime.UtcNow
    };

    await db.ShortenedUrls.AddAsync(shortenedUrl);

    await db.SaveChangesAsync();

    return Results.Ok(new ShortenUrlResponse(shortenedUrl.ShortUrl, shortenedUrl.LongUrl));
});

app.MapGet("/api/{code}", async (string code, ApplicationDbContext db) =>
{
    var shortenedUrl = await db.ShortenedUrls.FirstOrDefaultAsync(x => x.Code == code);

    return shortenedUrl is null
        ? Results.NotFound("No shortened URl for given code")
        : Results.Redirect(shortenedUrl.LongUrl);
});

app.MapGet("/api/shorten/all", async (ApplicationDbContext db) =>
{
    var response = await db.ShortenedUrls.Select(x => new ShortenedUrlDto(x.ShortUrl, x.LongUrl, x.CreatedOnUtc))
        .ToListAsync();

    return Results.Ok(response);
});

app.Run();