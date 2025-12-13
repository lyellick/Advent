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
            (long x, long y)[] coordinates = Puzzle.Input
                .Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(coordinate => (long.Parse(coordinate.Split(',')[0]), long.Parse(coordinate.Split(',')[1])))
                .ToArray();

            var edges = new List<((long x, long y) start, (long x, long y) end)>();

            for (int coordinate = 0; coordinate < coordinates.Length; coordinate++)
            {
                var a = coordinates[coordinate];
                var b = coordinates[(coordinate + 1) % coordinates.Length];
                edges.Add((a, b));
            }

            long max = 0;

            for (int currentIndex = 0; currentIndex < coordinates.Length; currentIndex++)
            {
                for (int nextIndex = currentIndex + 1; nextIndex < coordinates.Length; nextIndex++)
                {
                    var current = coordinates[currentIndex];
                    var next = coordinates[nextIndex];

                    long area = GetArea(current, next);

                    if (area <= max)
                    {
                        continue;
                    }

                    bool intersects = false;

                    foreach (var (start, end) in edges)
                    {
                        var doesIntersect = DoesIntersect(current, next, start, end);

                        if (doesIntersect)
                        {
                            intersects = true;
                            break;
                        }
                    }

                    if (!intersects && area > max)
                    {
                        max = area;
                    }
                }
            }

            Assert.AreEqual(1554370486, max);
        }

        private static long GetArea((long x, long y) start, (long x, long y) end)
        {
            long width = Math.Abs(start.x - end.x) + 1;
            long height = Math.Abs(start.y - end.y) + 1;

            return width * height;
        }

        private static bool DoesIntersect((long x, long y) start, (long x, long y) end, (long x, long y) edgeStart, (long x, long y) edgeEnd)
        {
            long rectLeft = Math.Min(start.x, end.x);
            long rectRight = Math.Max(start.x, end.x);
            long rectBottom = Math.Min(start.y, end.y);
            long rectTop = Math.Max(start.y, end.y);

            bool isHorizontal = edgeStart.y == edgeEnd.y;
            bool isVertical = edgeStart.x == edgeEnd.x;

            if (!isHorizontal && !isVertical)
                return false;

            if (isHorizontal)
            {
                long edgeY = edgeStart.y; 

                if (edgeY <= rectBottom || edgeY >= rectTop)
                {
                    return false;
                }

                long edgeLeftX = Math.Min(edgeStart.x, edgeEnd.x);
                long edgeRightX = Math.Max(edgeStart.x, edgeEnd.x);

                bool overlapsInX = edgeLeftX < rectRight && edgeRightX > rectLeft;

                return overlapsInX;
            }
            else
            {
                long edgeX = edgeStart.x;

                if (edgeX <= rectLeft || edgeX >= rectRight)
                {
                    return false;
                }

                long edgeBottomY = Math.Min(edgeStart.y, edgeEnd.y);
                long edgeTopY = Math.Max(edgeStart.y, edgeEnd.y);

                bool overlapsInY = edgeBottomY < rectTop && edgeTopY > rectBottom;

                return overlapsInY;
            }
        }
    }
}