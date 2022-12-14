using core;

namespace TwentyTwo;

public class Day11 : IPuzzle<Day11Args, Day11Results>
{
    public Day11Args LoadArgs()
    {
        var lines = File.ReadAllLines("Day11/Day11Data.txt");
        var args = lines.Chunk(7).ToList();
        var result = new Day11Args(args.Count());
        for(var i = 0; i < args.Count(); i++)
        {
            var itemsArgs = args[i][1].Split(":")[1].Split(",").Select(x => long.Parse(x));
            var opertaionArgs = args[i][2].Split("=")[1].Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            var divider = int.Parse(args[i][3].Trim().Split(" ").Last());
            var onTrue = int.Parse(args[i][4].Trim().Last().ToString());
            var onFalse = int.Parse(args[i][5].Trim().Last().ToString());
            var monkey1 = new Monkey(itemsArgs, opertaionArgs, (divider, onTrue, onFalse));
            var monkey2 = new Monkey(itemsArgs, opertaionArgs, (divider, onTrue, onFalse));
            result.Part1Monkeys[i] = monkey1;
            result.Part2Monkeys[i] = monkey2;
        }
        return result;
    }

    public Day11Results Run(Day11Args input)
    {
        //Part 1

        var part1 = Run(20, input.Part1Monkeys, false);

        //Part 2
        var part2 = Run(10000, input.Part2Monkeys, true);

        return new Day11Results
        {
            Part1 = part1,
            Part2 = part2

        };
    }

    private static long Run(int numberOfRounds, Monkey[] input, bool calm)
    {
        long commonDivisor = 1;
        if(calm)
        {
            input.ToList().ForEach(x => commonDivisor *= x.Divisor);
        }
      
        for (var i = 0; i < numberOfRounds; i++)
        {
            foreach (var monkey in input)
            {
                while (monkey.HasNext())
                {
                    var next = calm ? monkey.ThrowNext(commonDivisor) : monkey.ThrowNext(0);
                    if (next != null)
                    {
                        input[next.Value.throwTo].Receive(next.Value.item);
                    }
                }
            }
        }
        var highest = input.Select(x => x.InspectionCount).OrderByDescending(x => x).Take(2).ToArray();
        var result = highest[0] * highest[1];
        return result;
    }
}