using Advent.Shared.Models;

namespace Advent.Shared.Services
{
    public interface IAdventService
    {
        Task<Puzzle> GetPuzzleAsync(int year, int day);
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
            var inputUrl = $"https://adventofcode.com/{year}/day/{day}/input";
            var cachePath = Path.Join(Path.GetTempPath(), $"AOC{year}{day:00}.txt");

            string input;

            if (File.Exists(cachePath))
            {
                input = await File.ReadAllTextAsync(cachePath);
            }
            else
            {
                input = await DownloadAndCacheAsync(inputUrl, cachePath);
            }

            return new Puzzle
            {
                Year = year,
                Day = day,
                Input = input
            };
        }

        private async Task<string> DownloadAndCacheAsync(string url, string cachePath)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);

            using var response = await _http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Failed to download puzzle input. Status: {(int)response.StatusCode} {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();

            await File.WriteAllTextAsync(cachePath, content);

            return content;
        }
    }
}
