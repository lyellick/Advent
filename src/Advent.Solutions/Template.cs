using Advent.Shared.Models;
using Advent.Shared.Providers;
using Advent.Shared.Services;

namespace Advent.Solutions;

[TestClass]
public sealed class Template
{
    private const int Year = 2015;
    private const int Day = 1;

    private static IAdventService _adventService = null!;
    private static Puzzle _puzzle = null!;
    private static string _cachePath = "";

    [ClassInitialize]
    public static async Task Init(TestContext _)
    {
        _adventService = AdventServiceProvider.Get<IAdventService>();

        _cachePath = Path.Join(Path.GetTempPath(), $"AOC{Year}{Day:00}.json");

        _puzzle = await _adventService.GetPuzzleAsync(Year, Day);

        Assert.IsNotNull(_puzzle, "Puzzle should not be null.");
        Assert.AreEqual(Year, _puzzle.Year);
        Assert.AreEqual(Day, _puzzle.Day);
        Assert.IsFalse(string.IsNullOrWhiteSpace(_puzzle.Title), "Puzzle title missing.");
        Assert.IsFalse(string.IsNullOrWhiteSpace(_puzzle.Body), "Puzzle HTML body missing.");
        Assert.IsFalse(string.IsNullOrWhiteSpace(_puzzle.Input), "Puzzle input missing.");
        Assert.IsTrue(File.Exists(_cachePath), $"Expected cache file at '{_cachePath}' to exist after puzzle load.");
    }

    [TestMethod]
    public void P01()
    {
        Assert.Fail("Part 1 not implemented.");
    }

    [TestMethod]
    public void P02()
    {
        Assert.Fail("Part 2 not implemented.");
    }

}
