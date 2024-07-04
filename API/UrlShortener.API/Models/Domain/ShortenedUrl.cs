namespace UrlShortener.API.Models.Domain;

public class ShortenedUrl
{
    public int Id { get; set; }
    public string LongUrl { get; set; }
    public string ShortUrl { get; set; } 
    public string Code { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}