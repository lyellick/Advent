using Advent.Shared.Attributes;
using Advent.Shared.Models;

namespace Advent.Solutions.Y2025
{
    [TestClass]
    [Puzzle(2025, 8)]
    public sealed class D08 : AdventTestBase
    {
        [TestMethod]
        public void P01()
        {
            //(long x, long y, long z)[] coords = Puzzle.Input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(r => (long.Parse(r.Split(",")[0]), long.Parse(r.Split(",")[1]), long.Parse(r.Split(",")[2]))).ToArray();
            (long x, long y, long z)[] coords = "162,817,812\n57,618,57\n906,360,560\n592,479,940\n352,342,300\n466,668,158\n542,29,236\n431,825,988\n739,650,466\n52,470,668\n216,146,977\n819,987,18\n117,168,530\n805,96,715\n346,949,466\n970,615,88\n941,993,340\n862,61,35\n984,92,344\n425,690,689".Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(r => (long.Parse(r.Split(",")[0]), long.Parse(r.Split(",")[1]), long.Parse(r.Split(",")[2]))).ToArray();

            var start = coords[0];
            var next = coords.Skip(1).ToArray();

            var closest = GetClosest(start, next);

            Assert.Inconclusive("Part 1 not implemented.");
        }

        [TestMethod]
        public void P02()
        {
            Assert.Inconclusive("Part 2 not implemented.");
        }

        private (long x, long y, long z) GetClosest((long x, long y, long z) start, (long x, long y, long z)[] coords)
        {
            var closest = coords[0];

            long smallest = GetDistance(start, coords[0]);

            foreach (var coord in coords)
            {
                long distance = GetDistance(start, coord);

                if (distance < smallest)
                {
                    smallest = distance;
                    closest = coord;
                }
            }

            return closest;
        }

        private static long GetDistance((long x, long y, long z) start, (long x, long y, long z) end)
        {
            long betweenX = start.x - end.x;
            long betweenY = start.y - end.y;
            long betweenZ = start.z - end.z;

            return betweenX * betweenX + betweenY * betweenY + betweenZ * betweenZ;
        }
    }
}
