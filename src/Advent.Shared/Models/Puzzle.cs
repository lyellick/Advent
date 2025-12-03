namespace Advent.Shared.Models;

public sealed class Puzzle
{
    public required string Title { get; set; }
    public int Year { get; set; }
    public int Day { get; set; }
    public required string Body { get; set; }
    public required string Input { get; set; }

    public string? CachePath { get; set; }
}
