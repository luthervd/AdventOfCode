using core;
using System.Net.WebSockets;

namespace TwentyTwo;


public class Day4 : IPuzzle<Day4Args, Day4Result>
{
    public Day4Args LoadArgs()
    {
        var contents = File.ReadAllLines("Day4/Day4Data.txt");
        var args = new Day4Args();
        foreach(var line in contents)
        {
            var leftRight = line.Split(",");
            var left = leftRight[0].Split("-");
            var right = leftRight[1].Split("-");

            var leftSection = new Section(int.Parse(left[0]), int.Parse(left[1]));
            var rightSection = new Section(int.Parse(right[0]), int.Parse(right[1]));

            args.Values.Add((leftSection, rightSection));
        }
        return args;
    }

    public Day4Result Run(Day4Args input)
    {
        //Part1
        var fullyContains = 0;
        foreach(var group in input.Values)
        {
            if (group.Left.FullyContains(group.Right) || group.Right.FullyContains(group.Left))
            {
                fullyContains += 1;
            }
        }

        //Part 2
        var overlaps = 0;
        foreach (var group in input.Values)
        {
            if (group.Left.Overlaps(group.Right))
            {
                overlaps += 1;
            }
        }
        return new Day4Result
        {
            Part1 = fullyContains,
            Part2 = overlaps
        };
    }
}