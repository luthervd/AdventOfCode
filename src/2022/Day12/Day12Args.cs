using core;

namespace TwentyTwo;

public class Day12Args
{
    private char Start = 'S';
    private char End = 'E';

    public Day12Args(string[] args)
    {
        var rows = args.Length;
        var cols = args[0].Length;
        PathGrid = new PathGrid<char>(rows, cols, (col, row) => args[row][col], Start, End);
    }

    public PathGrid<char> PathGrid { get; set; }
}
