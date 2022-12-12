using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyTwo;

public abstract class Leader
{
    public Tail? Tailer { get; init; }
    
    public virtual Point CurrentPosition { get; set; }

    protected void CheckY()
    {
        if (Math.Abs(CurrentPosition.Y - Tailer.CurrentPosition.Y) > 1)
        {
            var yTarget = CurrentPosition.Y > Tailer.CurrentPosition.Y ? CurrentPosition.Y - 1 : CurrentPosition.Y + 1;
            Tailer.MoveTo(new Point(CurrentPosition.X, yTarget));
        }
    }

    protected void CheckX()
    {
        if (Math.Abs(CurrentPosition.X - Tailer.CurrentPosition.X) > 1)
        {
            var yTarget = CurrentPosition.Y != Tailer.CurrentPosition.Y ? CurrentPosition.Y : Tailer.CurrentPosition.Y;
            var xTarget = CurrentPosition.X > Tailer.CurrentPosition.X ? CurrentPosition.X - 1 : CurrentPosition.X + 1;
            Tailer.MoveTo(new Point(xTarget, yTarget));
        }
    }
}
