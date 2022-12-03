namespace TwentyTwo;


public class Day3Args
{
    public List<(string, string)> PerLineValues { get; set; } = new List<(string, string)>();

    public List<IList<string>> GroupedBy3 = new List<IList<string>>();

    public int GetCharValue(char arg)
    {
        //UPERCASE
        if(arg >= 65 && arg <= 90)
        {
            return (int)arg - 38;
        }
        else if(arg >= 97 && arg <= 122)
        {
            return arg - 96;
        }
        return -1;
    }
}