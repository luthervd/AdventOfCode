using core;
using System.Text;

namespace TwentyTwo;


public class InstructionSet
{
    public int XValue { get; set; }

    public int CycleCount { get; set; }

    public int Total { get; set; }
}

public class Day10 : IPuzzle<Day10Args, Day10Result>
{
    private StringBuilder _crtDrawer = new StringBuilder();
    private int _pixelIndex = 0;

    public Day10Args LoadArgs()
    {
        var lines = File.ReadAllLines("Day10/Day10Data.txt");
        var results = lines.Select(x =>
        {
            var args = x.Split(' ');
            var instruction = args[0] == "noop" ? Instruction.None : Instruction.AddX;
            int value = 0;
            if(instruction == Instruction.AddX)
            {
                value = int.Parse(args[1]);
            }
            return (instruction,value);
        }).ToList();
        return new Day10Args
        {
            Values = results
        };
    }

    public Day10Result Run(Day10Args input)
    {
        var actions = new List<Func<InstructionSet, InstructionSet>>();
        var cycleIndex = new List<int>
        {
            20,60,100,140,180,220
        };
        foreach (var step in input.Values)
        {
            if(step.Item1 == Instruction.None)
            {
                actions.Add(instructionSet=>
                {
                    Draw(instructionSet.XValue - 1, 3, instructionSet.CycleCount);
                    var nextCycle = instructionSet.CycleCount+1;
                   
                    var total = instructionSet.Total;
                    if (cycleIndex.Any(x => x == nextCycle))
                    {
                        total = total += nextCycle * instructionSet.XValue;
                    }
                    return new InstructionSet
                    {
                        CycleCount = nextCycle,
                        Total = total,
                        XValue = instructionSet.XValue
                    };
                });
            }
            else
            {
                actions.Add(instructionSet =>
                {
                    Draw(instructionSet.XValue - 1, 3, instructionSet.CycleCount);
                    var step1 = instructionSet.CycleCount+1;
                    
                    var total = instructionSet.Total;
                    if (cycleIndex.Any(x => x == step1))
                    {
                        var toAdd = step1 * instructionSet.XValue;
                        total = total += toAdd;
                    }
                    var step2 = step1+1;
                    Draw(instructionSet.XValue - 1, 3, step1);
                    var xValue = instructionSet.XValue += step.Item2;
                    if (cycleIndex.Any(x => x == step2))
                    {
                        var toAdd = step2 * instructionSet.XValue;
                        total = total += toAdd;
                    }
                    return new InstructionSet
                    {
                        CycleCount = step2,
                        Total = total,
                        XValue = xValue
                    };
                });
            }
        }
        var instruction = new InstructionSet
        {
            CycleCount = 1, Total = 0, XValue = 1
        };
        foreach(var action in actions)
        {
            instruction = action.Invoke(instruction);
        }
        return new Day10Result
        {
            Part1 = instruction.Total,
            Part2 = _crtDrawer.ToString()
        };
    }

    private void Draw(int spriteStart, int spriteLength, int cycle)
    {
        if(_pixelIndex == 40)
        {
            _crtDrawer.AppendLine();
            _pixelIndex = 0;
        }
        if(_pixelIndex >= spriteStart && _pixelIndex <= spriteStart+spriteLength-1)
        {
            _crtDrawer.Append('#');
        }
        else
        {
            _crtDrawer.Append('.');
        }
        _pixelIndex++;
    }
}

