namespace TwentyTwo;

public class Day4Args
{
    public List<(Section Left, Section Right)> Values { get; } = new List<(Section Left, Section Right)> ();
}

public struct Section
{
    public Section(int start, int end)
    {
        Start = start;
        End = end;
    }

    public int Start { get; }

    public int End { get;  }

    public bool FullyContains(Section section)
    {
        return section.Start >= Start && section.End <= End;
    }

    public bool Overlaps(Section section)
    {
        return section.Start <= End && section.End >= Start;
    }
}