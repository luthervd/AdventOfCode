using core;

namespace TwentyOne.Day3;

public class Day3 : IPuzzle<IList<string>, Day3Result>
{
    private const string filePath = "Day3/Day3Input.txt";

    public IList<string> LoadArgs()
    {
        return File.ReadAllLines(filePath);
    }

    public Day3Result Run(IList<string> input)
    {
        var answer = new Day3Result();
        var counter = new (int one, int zero)[input[0].Count()];
        var oxygenGen = "";
        var co2Scrub = "";
        for(var i = 0; i < counter.Length; i++)
        {
            counter[i] = (0, 0);
        }
        for(int i = 0; i < input.Count(); i++)
        {
            for(int j = 0; j < input[i].Length; j++)
            {
                if(input[i][j] == '1')
                {
                    counter[j].one += 1;
                }
                else
                {
                    counter[j].zero += 1;
                }
            }
        }
        var gamma = "";
        var epsilon = "";
        foreach(var item in counter)
        {
            gamma += item.one > item.zero ? "1" : "0";
            epsilon += item.one > item.zero ? "0" : "1";
        }

        var oxygenBatch = new List<string>();
        var co2ScrubBatch = new List<string>();
        
        var oxygenSeed = counter[0].one > counter[0].zero ? '1' : '0';
        var co2ScrubSeed = counter[0].one > counter[0].zero ? '0' : '1';

        oxygenBatch = input.Where(x => x[0] == oxygenSeed).ToList();
        co2ScrubBatch = input.Where(x => x[0] == co2ScrubSeed).ToList();
        var index = 1;
        while(oxygenBatch.Count > 1)
        {
          
            oxygenBatch = Reduce(oxygenBatch, index);
            index++;
            if(index> oxygenBatch[0].Length)
            {
                index = 0;
            }
        }
        index = 1;
        while (co2ScrubBatch.Count > 1)
        {

            co2ScrubBatch = Reduce(co2ScrubBatch, index, false);
            index++;
            if (index > co2ScrubBatch[0].Length)
            {
                index = 0;
            }
        }

        var gammaValue = Convert.ToInt32(gamma, 2);
        var epsilonValue = Convert.ToInt32(epsilon, 2);
        var oxygenValue = Convert.ToInt32(oxygenBatch[0], 2);
        var co2ScrubValue = Convert.ToInt32(co2ScrubBatch[0], 2);
        answer.PowerConsumption = gammaValue * epsilonValue;
        answer.LifeSupportRating = oxygenValue * co2ScrubValue;
        return answer;
    }

    private List<string> Reduce(IList<string> items, int index, bool takeMost = true)
    {
        var result = (0, 0);
        foreach(var item in items)
        {
            if (item[index] == '1')
            {
                result.Item1 += 1;
            }
            else
            {
                result.Item2 += 1;
            }
        }
        if (takeMost)
        {
            var target = result.Item1 > result.Item2 || result.Item1 == result.Item2 ? '1' : '0';
            return items.Where(x => x[index] == target).ToList();
        }
        else
        {
            var target = result.Item1 > result.Item2 || result.Item1 == result.Item2 ? '0' : '1';
            return items.Where(x => x[index] == target).ToList();
        }
    }
}
