namespace TwentyTwo;

public class Day2Args
{
    public char Left { get; set; }

    public char Right { get; set; }

    public override string ToString()
    {
        return $"{Left}{Right}";
    }
}
