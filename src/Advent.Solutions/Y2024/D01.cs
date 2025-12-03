using Advent.Shared.Attributes;
using Advent.Shared.Models;

namespace Advent.Solutions.Y2024
{
    [TestClass]
    [Puzzle(2024, 1)]
    public sealed class D01 : AdventTestBase
    {
        [TestMethod]
        public void P01()
        {
            List<long> results = [];
            
            List<long> left = [];
            List<long> right = [];

            var input = Puzzle.Input.Split("\n");

            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split("   ");

                left.Add(long.Parse(parts[0]));
                right.Add(long.Parse(parts[1]));
            }

            long[] leftCompare = left.OrderBy(item => item).ToArray();
            long[] rightCompare = right.OrderBy(item => item).ToArray();

            for (int i = 0; i < leftCompare.Length; i++)
            {
                var range = (new long[] { leftCompare[i], rightCompare[i] }).OrderByDescending(number => number).ToArray();
                var distance = range[0] - range[1];
                
                results.Add(distance);
            }

            var total = results.Sum();

            Assert.AreEqual(2000468, total);
        }

        [TestMethod]
        public void P02()
        {
            List<long> results = [];

            List<long> left = [];
            List<long> right = [];

            var input = Puzzle.Input.Split("\n");

            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split("   ");

                left.Add(long.Parse(parts[0]));
                right.Add(long.Parse(parts[1]));
            }

            long[] leftCompare = left.ToArray();
            long[] rightCompare = right.ToArray();

            for (int i = 0; i < leftCompare.Length; i++)
            {
                var numberToSearch = leftCompare[i];
                var timesFoundInRightCompare = rightCompare.Count(number => number == numberToSearch);
                var distance = numberToSearch * timesFoundInRightCompare;

                results.Add(distance);
            }

            var total = results.Sum();

            Assert.AreEqual(18567089, total);
        }
    }
}
