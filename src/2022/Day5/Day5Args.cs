namespace TwentyTwo;

public class Day5Args
{
    public Day5Args()
    {
        for (var i = 0; i < 9; i++)
        {
            Stacks1[i] = new Stack<string>();
            Stacks2[i] = new Stack<string>();
        }
    }

    public Stack<string>[] Stacks1 { get; set; } = new Stack<string>[9];

    public Stack<string>[] Stacks2 { get; set; } = new Stack<string>[9];

    public List<(int amount, int fromStack, int toStack)> Args { get; set; } = new List<(int amount, int fromStack, int toStack)>();

}