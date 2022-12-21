using System.Drawing;

namespace TwentyTwo;

public class Sand
{

    public Sand(int startX)
    {
        Point = new Point(startX, 0);
    }
    
    public Point Point { get; set; }

    public IList<Point> Points => new[] { Point };

    public bool IsSettled { get; set; }

    public void Move(HashSet<Point> colliders)
    {
        if(TryMoveDown(out var point, colliders))
        {
            Point = point;
        }
        else if(TryMoveLeftDown(out var leftDownPoint, colliders))
        {
            Point = leftDownPoint;
        }
        else if(TryMoveRightDown(out var rightDownPoint, colliders))
        {
            Point = rightDownPoint;
        }
        else
        {
            IsSettled = true;
        }
    }

    public bool TryMoveDown(out Point point, HashSet<Point> colliders)
    {
        point = new Point(Point.X, Point.Y + 1);
        return CanMove(point, colliders);

    }

    public bool TryMoveLeftDown(out Point point, HashSet<Point> colliders)
    {
        point = new Point(Point.X-1, Point.Y + 1);
        return CanMove(point, colliders);
    }

    public bool TryMoveRightDown(out Point point, HashSet<Point> colliders)
    {
        point = new Point(Point.X+1, Point.Y + 1);
        return CanMove(point, colliders);
    }

    private bool CanMove(Point point, HashSet<Point> colliders)
    {
        if (colliders.Contains(point))
        {
            return false;
        }
        return true;
    }
}
