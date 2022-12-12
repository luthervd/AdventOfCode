using System;
using System.Drawing;

namespace TwentyTwo;

public class Tail : Leader
{
    private HashSet<Point> _visited = new HashSet<Point>();

    public Tail(Point start, int followers)
    {
        CurrentPosition = start;
        _visited.Add(start);
        if(followers > 0)
        {
            Tailer = new Tail(start, followers-1);
        }
    }

    public void MoveTo(Point point)
    {
        CurrentPosition = point;
        _visited.Add(CurrentPosition); 
        if(Tailer != null)
        {
            CheckX();
            CheckY();
        }
    }

    public override Point CurrentPosition { get; set; }

    public int GetVisitedCount()
    {
        return _visited.Count;
    }
}
