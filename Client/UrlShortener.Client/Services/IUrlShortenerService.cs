using Refit;
using UrlShortener.Client.Models.DTOs;

namespace UrlShortener.Client.Services;

public interface IUrlShortenerService
{
    [Post("/shorten")]
    Task<ShortenUrlResponse> GetShortenedUrl([Body] ShortenUrlRequest request);
    
    [Get("/{code}")]
    Task RedirectViaShortUrl(string code);

    [Get("/shorten/all")]
    Task<List<ShortenedUrl>> GetShortenedUrlsHistory();
}