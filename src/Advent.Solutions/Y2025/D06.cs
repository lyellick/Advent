using Advent.Shared.Attributes;
using Advent.Shared.Models;

namespace Advent.Solutions.Y2025
{
    [TestClass]
    [Puzzle(2025, 6)]
    public sealed class D06 : AdventTestBase
    {
        [TestMethod]
        public void P01()
        {
            var rows = Puzzle.Input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(row => row.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray()).ToArray();

            long total = 0;

            for (int i = 0; i < rows[0].Length; i++)
            {
                var a = long.Parse(rows[0][i]);
                var b = long.Parse(rows[1][i]);
                var c = long.Parse(rows[2][i]);
                var d = long.Parse(rows[3][i]);

                switch (rows[4][i])
                {
                    case "*":
                        total += a * b * c * d;
                        break;
                    case "+":
                        total += a + b + c + d;
                        break;
                }
            }

            Assert.AreEqual(8108520669952, total);
        }

        [TestMethod]
        public void P02()
        {
            var rows = Puzzle.Input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(row => row.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray()).ToArray();

            long total = 0;

            for (int i = 0; i < rows[0].Length; i++)
            {
                var a = rows[0][i];
                var b = rows[1][i];
                var c = rows[2][i];
                var d = rows[3][i];

                var problems = new[] { a, b, c, d };

                var width = problems.Max(w => w.Length);

                var cols = new List<char[]>();

                foreach (var problem in problems)
                {
                    var parts = problem.PadLeft(width).ToCharArray();

                    cols.Add(parts);
                }

                switch (rows[4][i])
                {
                    case "*":
                        //total += a * b * c * d;
                        break;
                    case "+":
                        //total += a + b + c + d;
                        break;
                }
            }

            Assert.AreEqual(8108520669952, total);
        }
    }
}
