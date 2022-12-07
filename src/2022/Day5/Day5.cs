using core;

namespace TwentyTwo;

public class Day5 : IPuzzle<Day5Args, Day5Results>
{
    public Day5Args LoadArgs()
    {
        var contents = File.ReadAllLines("Day5/Day5Data.txt");
        var args = new Day5Args();
        for(var i = 7; i >= 0; i--)
        {
            var crateArgs = contents[i].Chunk(4);
            var stackIndex = 0;
            foreach(var crate in crateArgs)
            {
                if (crate[0] == '[')
                {
                    var crateId = crate[1].ToString();
                    args.Stacks1[stackIndex].Push(crateId);
                    args.Stacks2[stackIndex].Push(crateId);
                }
                stackIndex++;
            }
        }
        var movements = contents.Skip(10).ToList();
        foreach(var movement in movements)
        {
            var moveArgs = movement.Split(" ");
            var number = int.Parse(moveArgs[1]);
            var from = int.Parse(moveArgs[3]);
            var to = int.Parse(moveArgs[5]);
            args.Args.Add((number, from, to));
        }
        return args;
    }

    public Day5Results Run(Day5Args input)
    {
        var results = new Day5Results();
        
        //Part 1
        foreach (var args in input.Args)
        {
            for(var i = 0; i < args.amount; i++)
            {
                var popped = input.Stacks1[args.fromStack-1].Pop();
                input.Stacks1[args.toStack-1].Push(popped);
            }
        }
        
        foreach(var stack in input.Stacks1)
        {
            results.Part1.Add(stack.Peek());
        }

        //part 2
        foreach (var args in input.Args)
        {
            var temp = new Stack<string>();
            for (var i = 0; i < args.amount; i++)
            {
                var popped = input.Stacks2[args.fromStack - 1].Pop();
                temp.Push(popped);
            }
            while(temp.Count > 0)
            {
                var item = temp.Pop();
                input.Stacks2[args.toStack - 1].Push(item);
            }
        }
        foreach (var stack in input.Stacks2)
        {
            results.Part2.Add(stack.Peek());
        }
        return results;
    }
}