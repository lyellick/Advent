using Advent.Shared.Models;
using Advent.Shared.Providers;
using Advent.Shared.Services;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace Advent.Solutions.Y2025;

[TestClass]
public sealed class D01
{
    private const int Year = 2025;
    private const int Day = 1;

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
        var zeros = 0;
        var current = 50;
        var rotations = _puzzle.Input.ToLower().Split("\n");

        foreach (var rotation in rotations)
        {
            if (!string.IsNullOrEmpty(rotation))
            {
                var r = int.Parse(rotation.Substring(1));

                if (rotation.StartsWith("r"))
                {
                    for (int i = 0; i < r; i++)
                    {
                        current++;
                        if (current == 100)
                            current = 0;
                    }
                }
                else if (rotation.StartsWith("l"))
                {
                    for (int i = 0; i < r; i++)
                    {
                        current--;
                        if (current == -1)
                            current = 99;
                    }
                }

                if (current == 0)
                    zeros++;
            }
        }

        Assert.AreEqual(1048, zeros);
    }

    [TestMethod]
    public void P02()
    {
        var zeros = 0;
        var current = 50;
        var rotations = _puzzle.Input.ToLower().Split("\n");

        foreach (var rotation in rotations)
        {
            if (!string.IsNullOrEmpty(rotation))
            {
                var r = int.Parse(rotation.Substring(1));

                if (rotation.StartsWith("r"))
                {
                    for (int i = 0; i < r; i++)
                    {
                        current++;
                        if (current == 100)
                            current = 0;

                        if (current == 0)
                            zeros++;
                    }
                }
                else if (rotation.StartsWith("l"))
                {
                    for (int i = 0; i < r; i++)
                    {
                        current--;
                        if (current == -1)
                            current = 99;

                        if (current == 0)
                            zeros++;
                    }
                }
            }
        }

        Assert.AreEqual(6498, zeros);
    }
}
