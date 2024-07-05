using Microsoft.EntityFrameworkCore;
using UrlShortener.API.Models;
using UrlShortener.API.Models.Domain;

namespace UrlShortener.API.Data;

public class ApplicationDbContext : DbContext
{
    public const string PostgreSqlConnectionStringConfigPath = "Postgres";
    
    public DbSet<ShortenedUrl> ShortenedUrls => Set<ShortenedUrl>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}