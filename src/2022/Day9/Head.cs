using System.Drawing;

namespace TwentyTwo;

public class Head : Leader
{
    
    private Point _currentPosition;

    public Head(int followers)
    {
        _currentPosition = new Point(0, 0);
        if(followers > 0)
        {
            Tailer = new Tail(new Point(0, 0), followers - 1);
        }
      
    }

    public override Point CurrentPosition => new Point(_currentPosition.X, _currentPosition.Y);


    public void Move(MoveDirection direction, int amount, Action<Head> draw)
    {
        for(var i = 0; i < amount; i++)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    _currentPosition.X -= 1;
                    break;
                case MoveDirection.Right:
                    _currentPosition.X += 1;
                    break;
                case MoveDirection.Up:
                    _currentPosition.Y += 1;
                    break;
                case MoveDirection.Down:
                    _currentPosition.Y -= 1;
                    break;
            }
            CheckY();
            CheckX();
            draw.Invoke(this);
        }
    }

    
}

public enum MoveDirection
{
    Up,Down,Left,Right
}
