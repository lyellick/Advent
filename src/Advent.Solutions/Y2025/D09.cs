using Advent.Shared.Attributes;
using Advent.Shared.Models;

namespace Advent.Solutions.Y2025
{
    [TestClass]
    [Puzzle(2025, 9)]
    public sealed class D09 : AdventTestBase
    {
        [TestMethod]
        public void P01()
        {
            (long x, long y)[] coordinates = Puzzle.Input
                .Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(coordinate => (long.Parse(coordinate.Split(',')[0]), long.Parse(coordinate.Split(',')[1])))
                .ToArray();

            long max = 0;

            for (int start = 0; start < coordinates.Length; start++)
            {
                for (int end = 0; end < coordinates.Length; end++)
                {
                    var area = GetArea(coordinates[start], coordinates[end]);

                    if (area > max)
                        max = area;
                }
            }

            Assert.AreEqual(4748826374, max);
        }

        [TestMethod]
        public void P02()
        {
            Assert.Inconclusive("Part 2 not implemented.");
        }

        private static long GetArea((long x, long y) start, (long x, long y) end)
        {
            long width = Math.Abs(start.x - end.x) + 1;
            long height = Math.Abs(start.y - end.y) + 1;

            return width * height;
        }

    }
}
