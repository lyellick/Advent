using Advent.Shared.Attributes;
using Advent.Shared.Models;

namespace Advent.Solutions.Y2025;

[TestClass]
[Puzzle(2025, 1)]
public sealed class D01 : AdventTestBase
{
    [TestMethod]
    public void P01()
    {
        var zeros = 0;
        var current = 50;
        var rotations = Puzzle.Input.ToLower().Split("\n");

        foreach (var rotation in rotations)
        {
            if (!string.IsNullOrEmpty(rotation))
            {
                var r = int.Parse(rotation.Substring(1));

                if (rotation.StartsWith("r"))
                {
                    for (int i = 0; i < r; i++)
                    {
                        current++;
                        if (current == 100)
                            current = 0;
                    }
                }
                else if (rotation.StartsWith("l"))
                {
                    for (int i = 0; i < r; i++)
                    {
                        current--;
                        if (current == -1)
                            current = 99;
                    }
                }

                if (current == 0)
                    zeros++;
            }
        }

        Assert.AreEqual(1048, zeros);
    }

    [TestMethod]
    public void P02()
    {
        var zeros = 0;
        var current = 50;
        var rotations = Puzzle.Input.ToLower().Split("\n");

        foreach (var rotation in rotations)
        {
            if (!string.IsNullOrEmpty(rotation))
            {
                var r = int.Parse(rotation.Substring(1));

                if (rotation.StartsWith("r"))
                {
                    for (int i = 0; i < r; i++)
                    {
                        current++;
                        if (current == 100)
                            current = 0;

                        if (current == 0)
                            zeros++;
                    }
                }
                else if (rotation.StartsWith("l"))
                {
                    for (int i = 0; i < r; i++)
                    {
                        current--;
                        if (current == -1)
                            current = 99;

                        if (current == 0)
                            zeros++;
                    }
                }
            }
        }

        Assert.AreEqual(6498, zeros);
    }
}
