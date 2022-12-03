using core;

namespace TwentyTwo;

public class Day3 : IPuzzle<Day3Args, Day3Result>
{
    public Day3Args LoadArgs()
    {
        var contents = File.ReadAllLines("Day3/Day3Data.txt");
        var args = new Day3Args();
        foreach(var line in contents)
        {
            var arg1 = line.Substring(0, line.Length / 2);
            var arg2 = line.Substring(line.Length / 2);
            args.PerLineValues.Add((arg1, arg2));
        }
        for(var i = 0; i < contents.Length; i+=3)
        {
            if(i+3 <= contents.Length)
            {
                var one = contents[i];
                var two = contents[i + 1];
                var three = contents[i + 2];
                var next = new List<string>
                {
                    one,two,three
                };
                args.GroupedBy3.Add(next);
            }
        }
        return args;
    }

    public Day3Result Run(Day3Args input)
    {
        //Part 1
        var total = 0;
        foreach (var arg in input.PerLineValues)
        {
            char[] shared = new char[1];
            foreach (var ch in arg.Item1)
            {
                if (arg.Item2.Contains(ch))
                {
                    shared[0] = ch;
                }
            }
            var value = input.GetCharValue(shared[0]);
            total += value;

        }

        //Part2
        var total2 = 0;
        foreach(var lines in input.GroupedBy3)
        {
            var result = new char[1];
            foreach(var ch in lines[0])
            {
                if(lines[1].Contains(ch) && lines[2].Contains(ch))
                {
                    result[0] = ch;
                }
            }
            var score = input.GetCharValue(result[0]);
            total2 += score;
        }

        //Part 2
        return new Day3Result
        {
            Part1 = total,
            Part2 = total2
        };
    }
}