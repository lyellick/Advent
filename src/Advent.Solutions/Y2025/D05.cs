using Advent.Shared.Attributes;
using Advent.Shared.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Advent.Solutions.Y2025
{
    [TestClass]
    [Puzzle(2025, 5)]
    public sealed class D05 : AdventTestBase
    {
        [TestMethod]
        public void P01()
        {
            var parts = Puzzle.Input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            (long lower, long upper)[] ranges = parts[0].Split("\n").Select(bounds => (long.Parse(bounds.Split("-")[0]), long.Parse(bounds.Split("-")[1]))).ToArray();
            long[] ingredients = parts[1].Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

            var total = 0;

            foreach (var ingredient in ingredients) 
            {
                var isFresh = ranges.Any(range => ingredient >= range.lower && ingredient <= range.upper);

                if (isFresh)
                {
                    total++;
                }
            }

            Assert.AreEqual(868, total);
        }

        [TestMethod]
        public void P02()
        {
            var parts = Puzzle.Input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            (long start, long end)[] ranges = parts[0].Split("\n").Select(bounds => (long.Parse(bounds.Split("-")[0]), long.Parse(bounds.Split("-")[1])))
                .OrderBy(range => range.Item1)
                .ThenBy(range => range.Item2)
                .ToArray();

            HashSet<(long lower, long upper)> cleanedRanges = [];

            var currentLineGraphPointStart = ranges[0].start;
            var currentLineGraphPointEnd = ranges[0].end;

            for (int point = 1; point < ranges.Length; point++)
            {
                var (start, end) = ranges[point];

                if (end <= currentLineGraphPointEnd)
                {
                    currentLineGraphPointEnd = currentLineGraphPointEnd > end ? currentLineGraphPointEnd : end;
                }
                else
                {
                    cleanedRanges.Add((currentLineGraphPointStart, currentLineGraphPointEnd));
                    currentLineGraphPointStart = start;
                    currentLineGraphPointEnd = end;
                }
            }

            long total = 0;

            foreach (var (start, end) in cleanedRanges)
            {
                total += end - start + 1;
            }

            Assert.Inconclusive("Part 2 not implemented.");
        }
    }
}
