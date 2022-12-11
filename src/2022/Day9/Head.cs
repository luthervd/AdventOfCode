using System.Drawing;

namespace TwentyTwo;

public class Head
{
    
    private Point _currentPosition;

    public Head()
    {
        _currentPosition = new Point(0, 0);
        Tail = new Tail(new Point(0, 0));
    }

    public Point CurrentPosition => new Point(_currentPosition.X, _currentPosition.Y);

    public Tail Tail { get; init; }

    public void Move(MoveDirection direction, int amount)
    {
        for(var i = 0; i < amount; i++)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    _currentPosition.X -= amount;
                    break;
                case MoveDirection.Right:
                    _currentPosition.X += amount;
                    break;
                case MoveDirection.Up:
                    _currentPosition.Y += amount;
                    break;
                    break;
                case MoveDirection.Down:
                    _currentPosition.Y -= amount;
                    break;
            }
            CheckY();
            CheckX();
        }
       
     
       
     

    }

    private void CheckY()
    {
        if (Math.Abs(_currentPosition.Y - Tail.CurrentPosition.Y) > 1)
        {
            Console.WriteLine("Y diff detecetd");
            var yTarget = _currentPosition.Y > Tail.CurrentPosition.Y ? _currentPosition.Y - 1 : _currentPosition.Y + 1;
            Tail.MoveTo(new Point(_currentPosition.X,yTarget));
        }
    }

    private void CheckX()
    {
        if (Math.Abs(_currentPosition.X - Tail.CurrentPosition.X) > 1)
        {
            Console.WriteLine("X diff detecetd");
            var yTarget = _currentPosition.Y != Tail.CurrentPosition.Y ? _currentPosition.Y : Tail.CurrentPosition.Y;
            var xTarget = _currentPosition.X > Tail.CurrentPosition.X ? _currentPosition.X - 1 : _currentPosition.X + 1;
            Tail.MoveTo(new Point(xTarget, yTarget));
        }
    }
}

public enum MoveDirection
{
    Up,Down,Left,Right
}
