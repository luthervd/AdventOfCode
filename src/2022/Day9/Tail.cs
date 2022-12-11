using System;
using System.Drawing;

namespace TwentyTwo;

public class Tail
{
    private HashSet<Point> _visited = new HashSet<Point>();

    public Tail(Point start)
    {
        CurrentPosition = start;
        _visited.Add(start);
    }

    public void MoveTo(Point point)
    {
        if(Math.Abs(point.Y - CurrentPosition.Y) > 0)
        {
            while(CurrentPosition.Y != point.Y)
            {
                var Y = point.Y > CurrentPosition.Y ? CurrentPosition.Y + 1 : CurrentPosition.Y - 1;
                var next = new Point(point.X, Y);
                _visited.Add(next);
                CurrentPosition = next;
            }
            
        }
        else if (Math.Abs(point.X - CurrentPosition.X) > 0)
        {
            while(CurrentPosition.X != point.X)
            {
                var x = point.X > CurrentPosition.X ? CurrentPosition.X + 1 : CurrentPosition.X - 1;
                var next = new Point(x, point.Y);
                CurrentPosition = next;
                if (!_visited.Contains(next))
                {
                    _visited.Add(next);
                }
            }
        }
        CurrentPosition = point;
    }

    public Point CurrentPosition { get; private set; }

    public int GetVisitedCount()
    {
        return _visited.Count;
    }
}
