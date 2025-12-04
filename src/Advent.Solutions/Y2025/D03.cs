using Advent.Shared.Attributes;
using Advent.Shared.Models;
using System.Linq;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Advent.Solutions.Y2025
{
    [TestClass]
    [Puzzle(2025, 3)]
    public sealed class D03 : AdventTestBase
    {
        [TestMethod]
        public void P01()
        {
            var batteries = Puzzle.Input.TrimEnd('\n').Split("\n").Select(pack => pack.Select(battery => int.Parse(battery.ToString())).ToArray());

            List<int> joltages = [];

            foreach (var battery in batteries) 
            {
                List<int> combos = [];
                for (int i = 0; i < battery.Length; i++)
                {
                    for (int n = 0; n < battery.Length; n++)
                    {
                        if (n > i)
                        {
                            var joltage = int.Parse($"{battery[i]}{battery[n]}");
                            combos.Add(joltage);
                        }
                    }
                }
                
                var highest = combos.Max();

                joltages.Add(highest);
            }

            var total = joltages.Sum();

            Assert.AreEqual(17207, total);
        }

        [TestMethod]
        public void P02()
        {
            var batteries = Puzzle.Input.TrimEnd('\n').Split("\n").Select(pack => pack.Select(battery => int.Parse(battery.ToString())).ToArray());

            batteries = "811111111111119\n234234234234278\n818181911112111".Split("\n").Select(pack => pack.Select(battery => int.Parse(battery.ToString())).ToArray());

            List<long> joltages = [];

            foreach (var battery in batteries)
            {
                var joltage = GetFirstLargestNumber(battery);

                var combined = long.Parse(string.Join("", joltage));

                joltages.Add(combined);
            }

            var total = joltages.Sum();

            Assert.AreEqual(17207, total);
        }

        private static List<int> GetFirstLargestNumber(int[] battery, List<int> results = null)
        {
            results ??= [];

            if (battery.Length == 0)
            {
                return results;
            }

            int largestNumber = battery[0];
            int largestNumberIndex = 0;

            for (int i = 0; i < battery.Length; i++) 
            {
                if (battery[i] > largestNumber)
                {
                    largestNumber = battery[i];
                    largestNumberIndex = i;
                }
            }

            results.Add(largestNumber);

            if (results.Count != 12)
            {
                int[] remainingBattery = new int[battery.Length - (largestNumberIndex + 1)];
                Array.Copy(battery, (largestNumberIndex + 1), remainingBattery, 0, remainingBattery.Length);

                results = GetFirstLargestNumber(remainingBattery, results);
            }

            return results;
        }
    }
}
