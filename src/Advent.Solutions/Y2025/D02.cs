using Advent.Shared.Models;
using Advent.Shared.Providers;
using Advent.Shared.Services;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Advent.Solutions.Y2025;

[TestClass]
public sealed class D02
{
    private const int Year = 2025;
    private const int Day = 2;

    private static IAdventService _adventService = null!;
    private static Puzzle _puzzle = null!;

    [ClassInitialize]
    public static async Task Init(TestContext _)
    {
        _adventService = AdventServiceProvider.Get<IAdventService>();

        _puzzle = await _adventService.GetPuzzleAsync(Year, Day);

        Assert.IsNotNull(_puzzle, "Puzzle should not be null.");
        Assert.AreEqual(Year, _puzzle.Year);
        Assert.AreEqual(Day, _puzzle.Day);
        Assert.IsFalse(string.IsNullOrWhiteSpace(_puzzle.Title), "Puzzle title missing.");
        Assert.IsFalse(string.IsNullOrWhiteSpace(_puzzle.Body), "Puzzle HTML body missing.");
        Assert.IsFalse(string.IsNullOrWhiteSpace(_puzzle.Input), "Puzzle input missing.");
        Assert.IsTrue(File.Exists(_puzzle.CachePath), $"Expected cache file at '{_puzzle.CachePath}' to exist after puzzle load.");
    }

    [TestMethod]
    public void P01()
    {
        var products = _puzzle.Input.Split(",");
        var results = new List<long>();

        foreach (var product in products) 
        {
            long invalid = 0;
            var start = long.Parse(product.Split('-')[0]);
            var end = long.Parse(product.Split('-')[1]);


            for (long item = start; item <= end; item++)
            {
                var str = item.ToString();
                string firsthalf = str.Substring(0, str.Length / 2);
                string lasthalf = str.Substring(str.Length / 2, str.Length / 2);

                if (firsthalf == lasthalf && str.Length % 2 == 0)
                {
                    invalid += item;
                    results.Add(item);
                }
            }
        }

        var total = results.Sum();

        Assert.AreEqual(64215794229, total);
    }

    [TestMethod]
    public void P02()
    {
        var products = _puzzle.Input.Split(",");
        var results = new HashSet<long>();

        foreach (var product in products)
        {
            long invalid = 0;
            var start = long.Parse(product.Split('-')[0]);
            var end = long.Parse(product.Split('-')[1]);


            for (long item = start; item <= end; item++)
            {
                var str = item.ToString();

                for (int i = 1; i <= str.Length; i++)
                {
                    var chunks = Chunk(str, i);
                    var hasDups = CheckDuplicates(chunks.ToArray());
                    var hasMoreThanOneDup = chunks.Count() > 1;
                    var isSameLength = string.Join("", chunks).Length == str.Length;

                    if (hasDups && hasMoreThanOneDup && isSameLength)
                    {
                        results.Add(item);
                    }
                }
            }
        }

        var total = results.Sum();

        Assert.AreEqual(85513235135, total);
    }

    static IEnumerable<string> Chunk(string str, int chunkSize)
    {
        return Enumerable.Range(0, str.Length / chunkSize).Select(i => str.Substring(i * chunkSize, chunkSize));
    }

    static bool CheckDuplicates(string[] list)
    {
        List<bool> dups = new();

        int n = list.Length;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                dups.Add(list[i] == list[j]);
            }
        }

        return dups.All(x => x);
    }
}
