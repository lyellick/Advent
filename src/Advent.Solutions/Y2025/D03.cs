using Advent.Shared.Attributes;
using Advent.Shared.Models;
using System.Linq;
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
            // Stuck... notes for the goal...
            // Select exactly 12 digits from a sequence of 100 digits.
            // Selected 12 digit number is the largest.
            // Order is perserved. 

            var batteries = Puzzle.Input.TrimEnd('\n').Split("\n").Select(pack => pack.Select(battery => int.Parse(battery.ToString())).ToArray());

            
        }
    }
}
