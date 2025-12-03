using Advent.Shared.Attributes;
using Advent.Shared.Models;
using Advent.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Advent.Solutions;

[TestClass]
public abstract class AdventTestBase
{
    protected ServiceProvider Provider = null!;
    protected IAdventService Advent = null!;
    protected Puzzle Puzzle = null!;

    [TestInitialize]
    public async Task Init()
    {
        var type = GetType();
        var attr = (PuzzleAttribute?)Attribute.GetCustomAttribute(type, typeof(PuzzleAttribute))
            ?? throw new InvalidOperationException($"Missing [Puzzle(Y, D)] on {type.Name}");

        var services = new ServiceCollection();

        services.AddHttpClient<IAdventService, AdventService>();

        Provider = services.BuildServiceProvider();
        Advent = Provider.GetRequiredService<IAdventService>();
        Puzzle = await Advent.GetPuzzleAsync(attr.Year, attr.Day);

        if (Puzzle == null || string.IsNullOrWhiteSpace(Puzzle.Input))
            throw new InvalidOperationException("Puzzle failed to load.");
    }

    [TestCleanup]
    public void Cleanup()
    {
        Provider?.Dispose();
    }
}