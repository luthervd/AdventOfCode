namespace TwentyTwo;

public class Day11Args
{
    public Day11Args(int count)
    {
        Part1Monkeys = new Monkey[count];
        Part2Monkeys = new Monkey[count];
    }

    public Monkey[] Part1Monkeys { get; set; }

    public Monkey[] Part2Monkeys { get; set; }
}