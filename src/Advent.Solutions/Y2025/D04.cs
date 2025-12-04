using Advent.Shared.Attributes;
using Advent.Shared.Models;
using System;

namespace Advent.Solutions.Y2025
{
    [TestClass]
    [Puzzle(2025, 4)]
    public sealed class D04 : AdventTestBase
    {
        [TestMethod]
        public void P01()
        {
            var grid = Puzzle.Input.Split("\n").Select(row => row.Select(c => c == '.' ? 0 : 1).ToArray()).ToArray();

            List<int> availableLocations = [];

            for (int row = 0; row < grid.Length; row++)
            {
                string r = "";
                for (int col = 0; col < grid[row].Length; col++)
                {
                    var thing = "";
                    
                    var cell = grid[row][col];

                    thing = cell == 1 ? $"@" : ".";

                    if (cell == 1)
                    {
                        var topLeft = AdjacentCell(grid, row - 1, col - 1);
                        var top = AdjacentCell(grid, row, col - 1);
                        var topRight = AdjacentCell(grid, row + 1, col - 1);
                        var midLeft = AdjacentCell(grid, row - 1, col);
                        var midRight = AdjacentCell(grid, row + 1, col);
                        var botLeft = AdjacentCell(grid, row - 1, col + 1);
                        var bot = AdjacentCell(grid, row, col + 1);
                        var botRight = AdjacentCell(grid, row + 1, col + 1);

                        var totalAdjacent = topLeft + top + topRight + midLeft + midRight + botLeft + bot + botRight;

                        if (totalAdjacent < 4)
                        {
                            availableLocations.Add(1);
                            thing = "X";
                        }
                    }

                    r += thing;
                }
                //Console.WriteLine(r);
            }

            var total = availableLocations.Sum();

            Assert.AreEqual(1397, total);
        }

        [TestMethod]
        public void P02()
        {
            var grid = Puzzle.Input.Split("\n").Select(row => row.Select(c => c == '.' ? 0 : 1).ToArray()).ToArray();

            List<int> availableLocations = [];

            RemoveRolls(grid, availableLocations);

            int total = availableLocations.Sum();

            Assert.Inconclusive("Part 2 not implemented.");
        }

        public string RemoveRolls(int[][] grid, List<int> availableLocations)
        {
            availableLocations = [];
            bool changed = true;
            string lastOutput = "";

            while (changed)
            {
                List<int> available = [];
                lastOutput = ProcessGrid(grid, available, out changed);
                availableLocations.Add(available.Count);
            }

            return lastOutput;
        }

        public string ProcessGrid(int[][] grid, List<int> removed, out bool changed)
        {
            changed = false;
            return ProcessCell(grid, 0, 0, "", removed, ref changed);
        }

        public string ProcessCell(int[][] grid, int row, int col, string r, List<int> availableLocations, ref bool changed)
        {
            if (row >= grid.Length)
                return r;

            if (col >= grid[row].Length)
                return ProcessCell(grid, row + 1, 0, r + "\n", availableLocations, ref changed);

            int cell = grid[row][col];
            string thing = cell == 1 ? "@" : ".";

            if (cell == 1)
            {
                var topLeft = AdjacentCell(grid, row - 1, col - 1);
                var top = AdjacentCell(grid, row, col - 1);
                var topRight = AdjacentCell(grid, row + 1, col - 1);
                var midLeft = AdjacentCell(grid, row - 1, col);
                var midRight = AdjacentCell(grid, row + 1, col);
                var botLeft = AdjacentCell(grid, row - 1, col + 1);
                var bot = AdjacentCell(grid, row, col + 1);
                var botRight = AdjacentCell(grid, row + 1, col + 1);

                var totalAdjacent = topLeft + top + topRight + midLeft + midRight + botLeft + bot + botRight;

                if (totalAdjacent < 4)
                {
                    availableLocations.Add(1);
                    thing = "X";
                    grid[row][col] = 0;
                    changed = true;
                }
            }

            return ProcessCell(grid, row, col + 1, r + thing, availableLocations, ref changed);
        }

        public int AdjacentCell(int[][] grid, int row, int col)
        {
            if (row >= 0 && row < grid.Length &&
                col >= 0 && col < grid[row].Length)
            {
                return grid[row][col];
            }

            return 0;
        }
    }
}
