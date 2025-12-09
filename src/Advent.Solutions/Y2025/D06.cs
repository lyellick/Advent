using Advent.Shared.Attributes;
using Advent.Shared.Models;
using System.Numerics;

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
            var rows = Puzzle.Input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(l => l.PadRight(l.Length)).ToArray();

            int rowCount = rows.Length - 1;
            int colCount = rows.Max(l => l.Length);

            long total = 0;
            int col = colCount - 1;

            while (col >= 0)
            {
                bool isEmptyColumn = true;
                for (int r = 0; r < rowCount; r++)
                {
                    if (col < rows[r].Length && rows[r][col] != ' ')
                    {
                        isEmptyColumn = false;
                    }
                }

                if (isEmptyColumn)
                {
                    col--;
                    continue;
                }

                var digits = new List<List<char>>();

                while (col >= 0)
                {
                    bool hasDigit = false;
                    List<char> number = [];

                    for (int r = 0; r < rowCount; r++)
                    {
                        char c = col < rows[r].Length ? rows[r][col] : ' ';
                        if (char.IsDigit(c))
                        {
                            hasDigit = true;
                            number.Add(c);
                        }
                    }

                    if (!hasDigit)
                        break;

                    digits.Add(number);
                    col--;
                }

                char op = rows[^1][col + 1];

                var numbers = digits.Select(colDigits => long.Parse(new string(colDigits.ToArray()))).ToList();

                total += op == '*'
                    ? numbers.Aggregate(1L, (a, b) => a * b)
                    : numbers.Sum();
            }

            Assert.AreEqual(11708563470209, total);
        }

    }
}
