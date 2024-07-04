namespace UrlShortener.API.Models.Dtos;

public record ShortenedUrlDto(string? ShortUrl, string? LongUrl, DateTime CreatedOnUtc);