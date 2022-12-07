using core;

namespace TwentyTwo;

public class Day6 : IPuzzle<Day6Args, Day6Results>
{
    public Day6Args LoadArgs()
    {
        var args = File.ReadAllText("Day6/Day6Data.txt");
        return new Day6Args
        {
            Stream = args
        };
    }

    public Day6Results Run(Day6Args input)
    {
        var result = new Day6Results();
        result.Part1 = Scan(4, input);
        result.Part2 = Scan(14, input);
        return result;
    }

    private int Scan(int range, Day6Args input)
    {
        for (var i = 0; i < input.Stream.Length; i++)
        {
            var next = new char[range];
            var bufferIndex = 0;
            for (var y = i; y < i + range; y++)
            {
                next[bufferIndex++] = input.Stream[y];
            }
            var hasDuplicates = next.GroupBy(x => x).Any(x => x.Count() > 1);
            if (!hasDuplicates)
            {
                return i + range;
            }
        }
        return -1;
    }
}
