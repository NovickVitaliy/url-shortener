using System.Text;
using Microsoft.EntityFrameworkCore;
using UrlShortener.API.Data;

namespace UrlShortener.API.Services;

public class DefaultUniqueUrlCodeGenerator : IUniqueUrlCodeGenerator
{
    private readonly ApplicationDbContext _db;
    private const string AllowedSymbols = "0123456789abcdefghijklmnopqrstuvwxyz";
    private const int CodeLength = 7;
    private readonly Random _random = Random.Shared;
    public DefaultUniqueUrlCodeGenerator(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<string> GenerateUniqueCodeForUrl()
    {
        while (true)
        {
            StringBuilder sb = new();

            for (var _ = 0; _ < CodeLength; ++_)
            {
                var randomChar = AllowedSymbols[_random.Next(AllowedSymbols.Length)];
                sb.Append(randomChar);
            }

            var code = sb.ToString();
            if (!await _db.ShortenedUrls.AnyAsync(x => x.Code == code))
            {
                return code;
            }
        }
    }
}