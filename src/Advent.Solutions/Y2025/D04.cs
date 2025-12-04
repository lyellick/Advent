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
            Assert.Inconclusive("Part 2 not implemented.");
        }

        public int AdjacentCell(int[][] grid, int row, int col)
        {
            if (row >= 0 && row < grid.Length && col >= 0 && col < grid[row].Length)
            {
                return grid[row][col];
            }
            else
            {
                return 0;
            }
        }
    }
}
