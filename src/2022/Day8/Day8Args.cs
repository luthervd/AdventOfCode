using System.Data;

namespace TwentyTwo;

public class Day8Args
{
    public Day8Args(int rows, int cols)
    {
        RowCount = rows;
        ColCount = cols;
        Grid = new int[rows, cols];
    }
    public int[,] Grid { get; init; }

    public int RowCount { get; init; }

    public int ColCount { get; set; }
}
