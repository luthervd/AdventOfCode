using System.Drawing;

namespace TwentyTwo;

public class Sensor
{
   

    public Sensor(Point position, Point nearestBeacon)
    {
        Position = position;
        NearestBeacon = nearestBeacon;
        var manhattan = Manhattan(Position, NearestBeacon);
        MaxY = Position.Y + manhattan;
        MinY = Position.Y - manhattan;
        MaxX = Position.X + manhattan;
        MinX = Position.X - manhattan;
        
    }
    public Point Position { get; init; }

    public Point NearestBeacon { get; init; }
    
    public int MaxY { get; init; }

    public int MinY { get; init; }

    public int MaxX { get; init; }

    public int MinX { get; init; }
   
    private int _minY;
  
    public bool TryGetLine(int Y, out Line line)
    {
        var manhattan = Manhattan(Position, NearestBeacon);
        if (Y <= MaxY && Y >= MinY)
        {
            var dist = Math.Abs(Position.Y - Y);
            var adjustedMan = manhattan - dist;
            var startX = Position.X - adjustedMan;
            var endX = Position.X + adjustedMan;
            line = new Line(startX, endX);
            return true;
        }
        line = new Line();
        return false;
    }

    private int Manhattan(Point left, Point right) => Math.Abs(left.X - right.X) + Math.Abs(left.Y - right.Y);
}
