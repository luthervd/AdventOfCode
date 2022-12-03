using core;

namespace TwentyOne.Day5
{
    public class Day5 : IPuzzle<PointArgs, Day5Answer>
    {

        private Dictionary<string, int> _intersections;

        public Day5()
        {
            _intersections = new Dictionary<string, int>();
        }

        public PointArgs LoadArgs()
        {
            var contents = File.ReadAllLines("Day5/Day5DataFull.txt");
            var pointArgs = new PointArgs();
            foreach(var line in contents)
            {
                var splitArgs = line.Split("->");
                var first = splitArgs[0].Split(",");
                var second = splitArgs[1].Split(",");
                var xStart = int.Parse(first[0]);
                var ystart = int.Parse(first[1]);
                var xEnd = int.Parse(second[0]);
                var yEnd = int.Parse(second[1]);
                var lineArg = new LineArgs
                {
                    X1 = xStart,
                    Y1 = ystart,
                    X2 = xEnd,
                    Y2 = yEnd
                };
                pointArgs.AddLineArg(lineArg);
            }
            return pointArgs;
        }

        public Day5Answer Run(PointArgs input)
        {
            var answer = new Day5Answer();
            foreach (var line in input.GetLines(false))
            {
                var points = line.GetPointArgs();
                if (points == null || points.Count == 0)
                {
                    Console.WriteLine("No Points");
                }
                foreach (var point in line.GetPointArgs())
                {
                    if (!_intersections.ContainsKey(point))
                    {
                        _intersections[point] = 1;
                    }
                    else
                    {
                        _intersections[point]++;
                    }
                }
            }
            answer.Overlaps = _intersections.Values.Where(x => x > 1).Count();
            return answer;
        }
    }
}
