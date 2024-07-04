using Microsoft.EntityFrameworkCore;
using UrlShortener.API.Models;

namespace UrlShortener.API.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<ShortenedUrl> ShortenedUrls => Set<ShortenedUrl>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}