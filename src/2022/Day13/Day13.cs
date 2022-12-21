using core;

namespace TwentyTwo;

public class Day13 : IPuzzle<Day13Args, Day13Results>
{
    public Day13Args LoadArgs()
    {
        var contents = File.ReadAllLines("Day13/Day13Data.txt");
        var args = new Day13Args();
        var chunks = contents.Chunk(3);
        foreach(var chunk in chunks)
        {
            var innerArgs = chunk.Take(2).ToArray();
            args.Data.Add((innerArgs[0], innerArgs[1]));
        }
        return args;
    }

    public Day13Results Run(Day13Args input)
    {
       
        var indexes = new List<int>();
        var packets = new List<Packet>();
        for(var i = 0; i < input.Data.Count;i++)
        {
            var pair = input.Data[i];
            var left = new Packet();
            var right = new Packet();
            _ = Parse(left,pair.Left.Substring(1,pair.Left.Length-2));
            _ = Parse(right,pair.Right.Substring(1, pair.Right.Length - 2));
            if(left.CompareTo(right) == -1)
            {
                indexes.Add(i + 1);
            }

            packets.AddRange(new[] { left, right });
           
            
        }
        var firstDivider = new Packet();
        var secondDivider = new Packet();
        firstDivider.Values.Add(new(2));
        secondDivider.Values.Add(new(6));
       
        var sorted = packets
                    .Append(firstDivider)
                    .Append(secondDivider)
                    .OrderBy(x => x)
                    .ToList();

        var firstIndex = sorted.IndexOf(firstDivider) + 1;
        var secondIndex = sorted.IndexOf(secondDivider) + 1;

        return new Day13Results
        {
            Part1 = indexes.Sum(),
            Part2 = firstIndex * secondIndex
        };
    }

    public int Parse(Packet packet,string args)
    {
        for(var i = 0; i < args.Length; i++)
        {
            char next = args[i];
            switch (next)
            {
                case '[':
                    var child = new Packet();
                    i += Parse(child, args.Substring(i+1, (args.Length)-(i+1)));
                    packet.Values.Add(child);
                    break;
                case ']':
                    return i + 1;
                case ',':
                    break;
                default:
                    var value = int.Parse(next.ToString());
                    if(value == 1)
                    {
                        if(i+1 < args.Length & args[i+1] == '0')
                        {
                            value += 9;
                            i++;
                        }
                    }
                    packet.Values.Add(new Packet(value));
                    break;
            }
        }
        return 0;
    }

   
}
