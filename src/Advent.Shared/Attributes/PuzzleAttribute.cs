namespace Advent.Shared.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class PuzzleAttribute(int year, int day) : Attribute
{
    public int Year { get; } = year;
    public int Day { get; } = day;
}
