namespace UrlShortener.Client.Models.DTOs;

public record ShortenedUrl(string? ShortUrl, string? LongUrl, DateTime CreatedOnUtc);