using core;

namespace TwentyOne.Day6
{
    public class Day6 : IPuzzle<List<int>, long>
    {
        public List<int> LoadArgs()
        {
            var args = File.ReadAllText("Day6/Day6Args.txt");
            return args.Split(",").Select(x => int.Parse(x)).ToList();
        }

        public long Run(List<int> input)
        {
            var days = new long[9];
            foreach(var i in input)
            {
                days[i]++;
            }
            var totalDays = 0;
            while (totalDays < 256)
            {
                var reproducers = days[0];
                for(var i = 0; i < days.Length-1; i++)
                {
                    days[i] = days[i + 1];
                }
                days[6] += reproducers;
                days[8] = reproducers;
                totalDays++;
            }
            return days.Sum(x => x);
        }
    }
}
