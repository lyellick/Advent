using Advent.Shared.Attributes;

namespace Advent.Solutions.Y2025
{
    [TestClass]
    [Puzzle(2025, 8)]
    public sealed class D08 : AdventTestBase
    {
        [TestMethod]
        public void P01()
        {
            (long x, long y, long z)[] coordinates = Puzzle.Input
                .Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(coordinate => (long.Parse(coordinate.Split(',')[0]), long.Parse(coordinate.Split(',')[1]), long.Parse(coordinate.Split(',')[2])))
                .ToArray();

            int totalConnectionsToMake = 1000;
            int pointCount = coordinates.Length;

            var edges = new List<(int Point1, int Point2, long DistanceSquared)>();

            for (int indexA = 0; indexA < pointCount; indexA++)
            {
                for (int indexB = indexA + 1; indexB < pointCount; indexB++)
                {
                    long distance = GetDistance(coordinates[indexA], coordinates[indexB]);
                    edges.Add((indexA, indexB, distance));
                }
            }

            edges.Sort((edgeA, edgeB) => edgeA.DistanceSquared.CompareTo(edgeB.DistanceSquared));

            int[] parent = new int[pointCount];
            int[] componentSize = new int[pointCount];

            for (int i = 0; i < pointCount; i++)
            {
                parent[i] = i;
                componentSize[i] = 1;
            }

            int edgeIndex = 0;

            while (totalConnectionsToMake-- > 0)
            {
                var (pointA, pointB, _) = edges[edgeIndex++];

                int rootA = Find(pointA, parent);
                int rootB = Find(pointB, parent);

                if (rootA == rootB)
                    continue;

                if (componentSize[rootA] < componentSize[rootB])
                    (rootA, rootB) = (rootB, rootA);

                parent[rootB] = rootA;
                componentSize[rootA] += componentSize[rootB];
            }

            var sizeByRoot = new Dictionary<int, int>();

            for (int i = 0; i < pointCount; i++)
            {
                int root = Find(i, parent);

                if (!sizeByRoot.TryAdd(root, 1))
                    sizeByRoot[root]++;
            }

            var circuitSizes = sizeByRoot.Values.ToList();
            circuitSizes.Sort((a, b) => b.CompareTo(a));

            long result = circuitSizes[0] * circuitSizes[1] * circuitSizes[2];

            Assert.AreEqual(131150, result);
        }

        [TestMethod]
        public void P02()
        {
            (long x, long y, long z)[] coordinates = Puzzle.Input
                .Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(coordinate => (long.Parse(coordinate.Split(',')[0]), long.Parse(coordinate.Split(',')[1]), long.Parse(coordinate.Split(',')[2])))
                .ToArray();

            int pointCount = coordinates.Length;

            var edges = new List<(int Point1, int Point2, long DistanceSquared)>(pointCount * (pointCount - 1) / 2);

            for (int indexA = 0; indexA < pointCount; indexA++)
            {
                for (int indexB = indexA + 1; indexB < pointCount; indexB++)
                {
                    edges.Add((indexA, indexB, GetDistance(coordinates[indexA], coordinates[indexB])));
                }
            }

            edges.Sort((edgeA, edgeB) => edgeA.DistanceSquared.CompareTo(edgeB.DistanceSquared));

            int[] parent = new int[pointCount];
            int[] componentSize = new int[pointCount];

            for (int i = 0; i < pointCount; i++)
            {
                parent[i] = i;
                componentSize[i] = 1;
            }

            int remainingComponents = pointCount;

            (long X1, long X2) lastMergedXCoords = (0, 0);

            foreach (var (pointA, pointB, _) in edges)
            {
                int rootA = Find(pointA, parent);
                int rootB = Find(pointB, parent);

                if (rootA == rootB)
                {
                    continue;
                }

                if (componentSize[rootA] < componentSize[rootB])
                {
                    (rootA, rootB) = (rootB, rootA);
                }

                parent[rootB] = rootA;
                componentSize[rootA] += componentSize[rootB];
                remainingComponents--;

                lastMergedXCoords = (coordinates[pointA].x, coordinates[pointB].x);

                if (remainingComponents == 1)
                {
                    break;
                }
            }

            long result = lastMergedXCoords.X1 * lastMergedXCoords.X2;

            Assert.AreEqual(2497445, result);
        }


        private static int Find(int index, int[] coords)
        {
            if (coords[index] != index)
            {
                coords[index] = Find(coords[index], coords);
            }

            return coords[index];
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
