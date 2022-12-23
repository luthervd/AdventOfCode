using core;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace TwentyTwo;

public class Day15 : IPuzzle<Day15Args, Day15Results>
{
    public Day15Args LoadArgs()
    {
        var result = new Day15Args();
        var pointArgs = File.ReadAllLines("Day15/Day15Data.txt");
        foreach(var line in pointArgs)
        {
            var leftRight = line.Split(":");
            var leftArgs = leftRight[0];
            var rightArgs = leftRight[1];
            var leftPoint = ExtractPoint(leftArgs, 2);
            var rightPoint = ExtractPoint(rightArgs, 5);
            result.SensorArgs.Add((leftPoint, rightPoint));
        }
        return result;
    }

    private Point ExtractPoint(string args, int start)
    {
        var split = args.Split(" ");
        var xArg = split[start];
        var yArg = split[start + 1];

        var x = int.Parse(xArg.Split("=")[1].Replace(",", ""));
        var y = int.Parse(yArg.Split("=")[1]);
        return new Point(x, y);
    }

    public Day15Results Run(Day15Args input)
    {
        var part1Y = 2_000_000;
        var part2Max = 4_000_000;
        var sensors = new List<Sensor>();
        var beaconsAtY = new HashSet<Point>();
        var lines = new Dictionary<int, List<Line>>();
        var part2 = new Dictionary<int, List<Line>>();
        var sen = new List<Sensor>();
        foreach(var sensorArgs in input.SensorArgs)
        {
            if(sensorArgs.Beacon.Y == part1Y)
            {
                beaconsAtY.Add(sensorArgs.Beacon);
            }
            var sensor = new Sensor(sensorArgs.Sensor, sensorArgs.Beacon);

            if(sensor.TryGetLine(part1Y, out var line))
            {
                
                if (lines.ContainsKey(part1Y))
                {
                   lines[part1Y].Add(line);
                }
                else
                {
                    lines[part1Y] = new List<Line>();
                    lines[part1Y].Add(line);
                }
            }  
            if(sensor.MaxX >  0 && sensor.MinX < part2Max)
            {
                sen.Add(sensor);
            }
        }
        sen = sen.OrderBy(x => x.MinY).ToList();
        for(var i = 0;i < part2Max; i++)
        {
            foreach (var sensor in sen.Where(x => x.MinY < i && x.MaxY > i))
            {
                if (sensor.TryGetLine(i, out var line))
                {
                    if (part2.ContainsKey(i))
                    {
                        part2[i].Add(line);
                    }
                    else
                    {
                        part2[i] = new List<Line>();
                        part2[i].Add(line);
                    }

                }
            }
        }

        var part1Result = Map(lines[part1Y]);
        var length = part1Result.Sum(x => x.Length);
        var part2Final = new Dictionary<int, List<Line>>();
        foreach(var kv in part2.Where(x => x.Value.Count > 1))
        {
            var mapped = Map(part2[kv.Key]);
            part2Final[kv.Key] = mapped;
        }

        var final = part2Final.Where(x => x.Value.Count > 1).First();
        var y = final.Key;
        var multiLines = final.Value.OrderBy(x => x.Start).ToList();
        if (multiLines.Count > 2)
        {
            throw new ArgumentException("More than expected");
        }
        BigInteger x = multiLines[0].End + 1;

        BigInteger result = (x * 4_000_000) + y;
        return new Day15Results
        {
            Part1 = length-beaconsAtY.Count(),
            Part2 = result
        };
    }

    private List<Line> Map(List<Line> lines)
    {
        return lines.OrderBy(x => x.Start)
        .Aggregate<Line, List<Line>>(new List<Line>(), (list, line) =>
        {
            var index = -1;
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i].Overlaps(line))
                {
                    line = list[i].Extend(line);
                    index = i;
                }
            }
            if (index >= 0)
            {
                list[index] = line;
            }
            else
            {
                list.Add(line);
            }
            return list;
        });
    }
}