namespace UrlShortener.API.Models;

public class ShortenedUrl
{
    public int Id { get; set; }
    public required string LongUrl { get; set; }
    public required string ShortUrl { get; set; } 
    public required string Code { get; set; }
    
}