using core;
using System.Drawing;

namespace TwentyTwo;

public class Day9 : IPuzzle<Day9Args, Day9Results>
{
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
        var head = new Head();
        input.Instructions.ForEach(x => { 
            head.Move(x.Direction, x.Amount);
            var rows = 20;
            var cols = 20;
            DrawGrid(rows,cols,head);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.Clear();
        });
        return new Day9Results
        {
            Part1 = head.Tail.GetVisitedCount()
        };
    }

    private void DrawGrid(int rows, int cols, Head head)
    {
        var xZero = rows / 2;
        var yZero = cols/2;
        Console.WriteLine("###############################");
        Console.WriteLine($"Head {head.CurrentPosition.X},{head.CurrentPosition.Y} Tail {head.Tail.CurrentPosition.Y},{head.Tail.CurrentPosition.Y}");
        for(var x = -20; x < rows; x++)
        {
            for(var y = -20 ; y < cols; y++)
            {
                var headPos = head.CurrentPosition;
                var tailPos = head.Tail.CurrentPosition;
                if(headPos.Y == x && headPos.Y == y)
                {
                    Console.Write(" H  ");
                }
                else if(tailPos.Y == x && tailPos.Y == y)
                {
                    Console.Write(" T  ");
                }
                else
                {
                    Console.Write(" [] ");
                }
                
            }
            Console.WriteLine();
        }
    }
}
