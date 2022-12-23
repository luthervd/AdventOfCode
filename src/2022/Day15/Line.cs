using System.Net.Mail;

namespace TwentyTwo;

public struct Line
{
    public Line(int start, int end)
    {
        Start = start;
        End = end;
    }

    public int Start { get; }

    public int End { get; }

    public int Length
    {
        get
        {
            if(Start < 0)
            {
                var start = Math.Abs(Start);
                return start + End+1;
            }
            return (End+1) - Start;
        }
    }

    public bool Overlaps(Line line)
    {
        //overlaps
        if (Math.Abs(line.End - Start) == 1 || (Math.Abs(line.Start - End) == 1))
        {
            return true;
        }
        
        if (line.Start > End || line.End < Start)return false;
        return true;

    }

    public Line Extend(Line line)
    {
        if (!Overlaps(line))
        {
            throw new ArgumentException("Lines must be overlapping to extend");
        }
        var start = Start > line.Start ? line.Start : Start;
        var end = End > line.End ? End : line.End;
        return new Line(start, end);
    }
}
