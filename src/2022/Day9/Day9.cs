using core;

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
        var head = new Head(9);
        input.Instructions.ForEach(x => { 
            head.Move(x.Direction, x.Amount,DrawGrid);
            DrawGrid(head);
        });
        var tailer = head.Tailer;
        while(tailer.Tailer != null)
        {
            tailer = tailer.Tailer;
        }
        return new Day9Results
        {
            Part1 = head.Tailer.GetVisitedCount(),
            Part2 = tailer.GetVisitedCount()
        };
    }

    private void DrawGrid(Head head)
    {
        //Console.WriteLine("###############################");
        //Console.WriteLine($"Head {head.CurrentPosition.X},{head.CurrentPosition.Y} Tail {head.Tail.CurrentPosition.X},{head.Tail.CurrentPosition.Y}");
        //Console.WriteLine($"Visited {head.Tail.GetVisitedCount()}");
        //for(var y = -10; y < 10; y++)
        //{
        //    for(var x = -10 ; x < 10; x++)
        //    {
        //        var headPos = head.CurrentPosition;
        //        var tailPos = head.Tail.CurrentPosition;
        //        if(headPos.X == x && headPos.Y == y)
        //        {
        //            Console.Write(" H");
        //        }
        //        else if(tailPos.X == x && tailPos.Y == y)
        //        {
        //            Console.Write(" T");
        //        }
        //        else
        //        {
        //            Console.Write("[]");
        //        }
                
        //    }
        //    Console.WriteLine();
        //}
        //Thread.Sleep(10);
        //Console.Clear();
        
    }
}
