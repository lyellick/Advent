using System.Text.Json;
using System.Text.RegularExpressions;
using Advent.Shared.Models;

namespace Advent.Shared.Services;

public interface IAdventService
{
    Task<Puzzle> GetPuzzleAsync(int year = 2015, int day = 1);
}

public class AdventService : IAdventService
{
    private readonly HttpClient _http;
    private readonly string _sessionToken;

    public AdventService(HttpClient httpClient)
    {
        _http = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        _sessionToken = Environment.GetEnvironmentVariable("AOCSession", EnvironmentVariableTarget.User)
            ?? throw new InvalidOperationException("Missing environment variable: AOCSession.");

        _http.DefaultRequestHeaders.Add("Cookie", $"session={_sessionToken}");
    }

    public async Task<Puzzle> GetPuzzleAsync(int year, int day)
    {
        string cachePath = Path.Join(Path.GetTempPath(), $"AOC{year}{day:00}.json");

        if (File.Exists(cachePath))
        {
            var json = await File.ReadAllTextAsync(cachePath);
            var cached = JsonSerializer.Deserialize<Puzzle>(json)!;
            return cached;
        }

        var inputTask = DownloadInputAsync(year, day);
        var pageTask = DownloadPageAsync(year, day);

        await Task.WhenAll(inputTask, pageTask);

        var (title, body) = ParsePuzzleHtml(pageTask.Result);

        var puzzle = new Puzzle
        {
            Year = year,
            Day = day,
            Title = title,
            Body = body,
            Input = inputTask.Result,
            CachePath = cachePath
        };

        var jsonOut = JsonSerializer.Serialize(
    puzzle,
    new JsonSerializerOptions
    {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    });

        await File.WriteAllTextAsync(cachePath, jsonOut);

        return puzzle;
    }

    private async Task<string> DownloadInputAsync(int year, int day)
    {
        var url = $"https://adventofcode.com/{year}/day/{day}/input";
        var response = await _http.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to get puzzle input: {response.StatusCode}");

        return await response.Content.ReadAsStringAsync();
    }

    private async Task<string> DownloadPageAsync(int year, int day)
    {
        var url = $"https://adventofcode.com/{year}/day/{day}";
        var response = await _http.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to get puzzle page: {response.StatusCode}");

        return await response.Content.ReadAsStringAsync();
    }

    private static (string Title, string Body) ParsePuzzleHtml(string html)
    {
        var articleMatch = Regex.Match(
            html,
            @"<article class=""day-desc"">([\s\S]*?)</article>",
            RegexOptions.IgnoreCase
        );

        if (!articleMatch.Success)
            return ("", "");

        string article = articleMatch.Groups[1].Value;

        var titleMatch = Regex.Match(article,
            @"<h2>--- Day \d+: (.*?) ---</h2>",
            RegexOptions.IgnoreCase);

        string title = titleMatch.Success ? titleMatch.Groups[1].Value : "";

        string body = Regex.Replace(article, @"<h2>.*?</h2>", "", RegexOptions.Singleline);

        body = body.Replace("\r", "").Replace("\n", "");

        return (title, body);
    }

}
