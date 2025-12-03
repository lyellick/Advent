using Advent.Shared.Models;
using Advent.Shared.Services;
using System.Collections.Concurrent;

namespace Advent.Shared.Caching;

public static class AdventPuzzleCache
{
    private static readonly ConcurrentDictionary<(int Year, int Day), Puzzle> _cache = new();

    public static async Task<Puzzle> GetOrAddAsync(int year, int day, IAdventService service)
    {
        if (_cache.TryGetValue((year, day), out var existing))
            return existing;

        var puzzle = await service.GetPuzzleAsync(year, day);

        _cache.TryAdd((year, day), puzzle);

        return puzzle;
    }
}
