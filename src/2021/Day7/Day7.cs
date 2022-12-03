using core;

namespace TwentyOne.Day7
{
    public class Day7 : IPuzzle<List<int>, Day7Result>
    {
        public List<int> LoadArgs()
        {
            var args = File.ReadAllText("Day7/Day7Data.txt");
            return args.Split(",").Select(x => int.Parse(x)).ToList();
        }

        public Day7Result Run(List<int> input)
        {
            var result = new Day7Result();
            var ordered = input.OrderBy(x => x).ToList();
            var medianIndex = ordered.Count / 2;
            var alignment = ordered[medianIndex];
            var cost = 0;
            foreach(var item in ordered)
            {
                cost += Math.Abs(item - alignment);
            }
            var stage2 = DistanceCost(input);
            result.Stage1Answer = cost;
            result.Stage2Answer = stage2;
            return result;
        }

        private int DistanceCost(List<int> input)
        {
            var range = new List<int>();
            var max = input.Max();
            for(int i = 0; i < max; i++)
            {
                range.Add(i);
            }
            var pq = new PriorityQueue<int,int>();
            foreach(var target in range)
            {
                var totalCost = 0;
                foreach(var crab in input)
                {
                    var distance = Math.Abs(target - crab);
                    var cost = distance * (distance + 1) / 2;
                    totalCost += cost;
                }
                pq.Enqueue(totalCost, totalCost);
            }
            return pq.Dequeue();
        }
    }
}
