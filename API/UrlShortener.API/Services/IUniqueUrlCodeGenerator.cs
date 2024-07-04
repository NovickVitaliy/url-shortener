namespace UrlShortener.API.Services;

public interface IUniqueUrlCodeGenerator
{
    Task<string> GenerateUniqueCodeForUrl();
}