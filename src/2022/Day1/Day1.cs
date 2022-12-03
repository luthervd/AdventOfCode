using core;

namespace TwentyTwo;

public class Day1 : IPuzzle<List<List<int>>, int>
{
    public List<List<int>> LoadArgs()
    {
        var args = File.ReadAllLines("Day1/Day1Data.txt");
        var result = new List<List<int>>();
        var next = new List<int>();
        foreach(var line in args)
        {
            if(int.TryParse(line,out var arg))
            {
                next.Add(arg);
            }
            else
            {
                result.Add(next);
                next = new List<int>();
            }
        }
        return result;
    }

    public int Run(List<List<int>> input)
    {
        var highest = input
                      .Select(x => x.Sum(y => y))
                      .OrderByDescending(x => x)
                      .Take(3)
                      .Sum();
        return highest;
    }
}
