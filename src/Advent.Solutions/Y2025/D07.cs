using Advent.Shared.Attributes;
using Advent.Shared.Models;

namespace Advent.Solutions.Y2025
{
    [TestClass]
    [Puzzle(2025, 7)]
    public sealed class D07 : AdventTestBase
    {
        [TestMethod]
        public void P01()
        {
            var rows = Puzzle.Input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(r => r.ToCharArray()).ToArray();

            int width = rows[0].Length;

            int splits = 0;

            HashSet<int> beams = [ rows[0].IndexOf('S') ];

            for (int row = 1; row < rows.Length && beams.Count > 0; row++)
            {
                HashSet<int> next = [];

                foreach (var col in beams)
                {
                    char cell = rows[row][col];

                    switch (cell)
                    {
                        case 'S':
                            break;
                        case '.':
                            next.Add(col);
                            break;
                        case '^':
                            splits++;

                            if (col > 0)
                            {
                                next.Add(col - 1);
                            }

                            if (col < width - 1)
                            {
                                next.Add(col + 1);
                            }
                            break;

                    }
                }

                beams = next;
            }

            Assert.AreEqual(1518, splits);
        }

        [TestMethod]
        public void P02()
        {
            Assert.Inconclusive("Part 2 not implemented.");
        }
    }
}
