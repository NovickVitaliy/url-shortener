using Microsoft.EntityFrameworkCore;
using UrlShortener.API.Data;

namespace UrlShortener.API.Extensions;

public static class ApplicationExtension
{
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        await context.Database.MigrateAsync();
    }
}