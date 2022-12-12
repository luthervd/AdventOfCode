using core;
using System.Drawing;

namespace TwentyTwo;

public enum MoveDirection
{
    Up, Down, Left, Right
}

public class Day9 : IPuzzle<Day9Args, Day9Results>
{
    private List<string> drawStack = new List<string>();
    
    public Day9Args LoadArgs()
    {
        var args = new Day9Args();
        var lines = File.ReadAllLines("Day9/Day9Data.txt");
        foreach (var line in lines)
        {
            var lineArgs = line.Split(" ");
            var direction = lineArgs[0] switch
            {
                "R" => MoveDirection.Right,
                "L" => MoveDirection.Left,
                "U" => MoveDirection.Up,
                "D" => MoveDirection.Down,
                _ => throw new ArgumentException("Unkown direction")
            };
            args.Instructions.Add((direction, int.Parse(lineArgs[1])));
          
        }
        return args;
    }

    public Day9Results Run(Day9Args input)
    {
        var day1 = Handle(input, 2, false);
        var day2 = Handle(input, 10, false);

        return new Day9Results
        {
            Part1 = day1,
            Part2 = day2
        };
       
    }
    
    private int Handle(Day9Args input, int number, bool drawGrid)
    {
        var toMove = new Point[number];
        var moveTo = new HashSet<Point>();
        for (var i = 0; i < toMove.Count(); i++)
        {
            toMove[0] = new Point(0, 0);
        }
        moveTo.Add(toMove.Last());
        foreach (var instruction in input.Instructions)
        {
            for (var i = 0; i < instruction.Amount; i++)
            {
                for (var mover = 0; mover < toMove.Length; mover++)
                {
                    var item = toMove[mover];
                    if (mover == 0)
                    {
                        var x = instruction.Direction == MoveDirection.Left ? item.X - 1 : instruction.Direction == MoveDirection.Right ? item.X + 1 : item.X;
                        var y = instruction.Direction == MoveDirection.Down ? item.Y + 1 : instruction.Direction == MoveDirection.Up ? item.Y - 1 : item.Y;
                        toMove[mover]= new Point(x, y);
                    }
                    if (mover + 1 < toMove.Length)
                    {
                        var newPoint = Check(toMove[mover], toMove[mover + 1]);
                        if(newPoint != null)
                        {
                            toMove[mover+1] = newPoint.Value;
                        }
                    }
                    if (mover == toMove.Length - 1)
                    {
                        moveTo.Add(toMove[mover]);
                    }
                }
            }
        }
        return moveTo.Count();
    }

    private Point? Check(Point left, Point right)
    {
        var next = new Point(right.X,right.Y);
        if (Math.Abs(left.Y - right.Y) > 1 || Math.Abs(left.X - right.X) > 1)
        {
            next.X = right.X + (right.X == left.X ? 0 : right.X < left.X ? 1 : -1);
            next.Y = right.Y + (right.Y == left.Y ? 0 : right.Y < left.Y ? 1 : -1);
        }
        return next != right ? next : null;
    }

}
