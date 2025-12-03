using Advent.Shared.Attributes;
using Advent.Shared.Models;

namespace Advent.Solutions.Y2025;

[TestClass]
[Puzzle(2025, 2)]
public sealed class D02 : AdventTestBase
{
    [TestMethod]
    public void P01()
    {
        var products = Puzzle.Input.Split(",");
        var results = new List<long>();

        foreach (var product in products) 
        {
            long invalid = 0;
            var start = long.Parse(product.Split('-')[0]);
            var end = long.Parse(product.Split('-')[1]);


            for (long item = start; item <= end; item++)
            {
                var str = item.ToString();
                string firsthalf = str.Substring(0, str.Length / 2);
                string lasthalf = str.Substring(str.Length / 2, str.Length / 2);

                if (firsthalf == lasthalf && str.Length % 2 == 0)
                {
                    invalid += item;
                    results.Add(item);
                }
            }
        }

        var total = results.Sum();

        Assert.AreEqual(64215794229, total);
    }

    [TestMethod]
    public void P02()
    {
        var products = Puzzle.Input.Split(",");
        var results = new HashSet<long>();

        foreach (var product in products)
        {
            var start = long.Parse(product.Split('-')[0]);
            var end = long.Parse(product.Split('-')[1]);


            for (long item = start; item <= end; item++)
            {
                var str = item.ToString();

                for (int i = 1; i <= str.Length; i++)
                {
                    var chunks = Chunk(str, i);
                    var hasDups = CheckDuplicates(chunks.ToArray());
                    var hasMoreThanOneDup = chunks.Count() > 1;
                    var isSameLength = string.Join("", chunks).Length == str.Length;

                    if (hasDups && hasMoreThanOneDup && isSameLength)
                    {
                        results.Add(item);
                    }
                }
            }
        }

        var total = results.Sum();

        Assert.AreEqual(85513235135, total);
    }

    static IEnumerable<string> Chunk(string str, int chunkSize)
    {
        return Enumerable.Range(0, str.Length / chunkSize).Select(i => str.Substring(i * chunkSize, chunkSize));
    }

    static bool CheckDuplicates(string[] list)
    {
        List<bool> dups = new();

        int n = list.Length;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                dups.Add(list[i] == list[j]);
            }
        }

        return dups.All(x => x);
    }
}
