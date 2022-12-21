using System.Drawing;

namespace TwentyTwo;

public class Obstacle
{
    
    public Obstacle(IEnumerable<(Point,Point)> args)
    {
        Points = new List<Point>();
        Init(args);
    }

    private void Init(IEnumerable<(Point Left, Point Right)> args)
    {
        foreach(var movement in args)
        {
            var direction = GetDirection(movement.Left, movement.Right);
            switch (direction)
            {
                case MoveDirection.Left:
                    MoveLeft(movement.Left, movement.Right);
                    break;
                case MoveDirection.Right:
                    MoveRight(movement.Left, movement.Right);
                    break;
                case MoveDirection.Down:
                    MoveDown(movement.Left, movement.Right);
                    break;
                case MoveDirection.Up:
                    MoveUp(movement.Left, movement.Right);
                    break;
            }
        }
    }

    public IList<Point> Points { get; private set; }

    private MoveDirection GetDirection(Point from, Point to)
    {
        if(from.X != to.X)
        {
            return from.X > to.X ? MoveDirection.Left : MoveDirection.Right;
        }
        else
        {
            return from.Y < to.Y ? MoveDirection.Down : MoveDirection.Up;
        }
    }

    private void MoveLeft(Point start, Point end)
    {
        Points.Add(start);
        Points.Add(end);
        var next = new Point(start.X -1, start.Y);
        while(next.X > end.X)
        {
            Points.Add(next);
            next = new Point(next.X - 1, next.Y);
        }
    }

    private void MoveRight(Point start, Point end)
    {
        Points.Add(start);
        Points.Add(end);
        var next = new Point(start.X + 1, start.Y);
        while (next.X < end.X)
        {
            Points.Add(next);
            next = new Point(next.X + 1, next.Y);
        }
    }

    private void MoveDown(Point start, Point end)
    {
        Points.Add(start);
        Points.Add(end);
        var next = new Point(start.X, start.Y+1);
        while (next.Y < end.Y)
        {
            Points.Add(next);
            next = new Point(next.X, next.Y+1);
        }
    }

    private void MoveUp(Point start, Point end)
    {
        Points.Add(start);
        Points.Add(end);
        var next = new Point(start.X, start.Y - 1);
        while (next.Y > end.Y)
        {
            Points.Add(next);
            next = new Point(next.X, next.Y - 1);
        }
    }

    public int LowestPoint => Points.Select(x => x.Y).Max();

    public int FarthestPoint => Points.Select(x => x.X).Max();

    public int NearestPoint => Points.Select(x => x.X).Min();

}
