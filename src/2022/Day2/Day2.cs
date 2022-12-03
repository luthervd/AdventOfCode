using core;

namespace TwentyTwo;
    public class Day2 : IPuzzle<IList<Day2Args>, Day2Result>
    {
        public IList<Day2Args> LoadArgs()
        {
            var result = File.ReadAllLines("Day2/Day2Data.txt").ToList();
            return result.Select(x =>
                {
                    var chars = x.Split(" ");
                    return new Day2Args
                    {
                        Left = chars[0][0],
                        Right = chars[1][0]
                    };
                }
            ).ToList();
            
           
        }

        public Day2Result Run(IList<Day2Args> input)
        {
            var results1 = new Dictionary<string, Result>
            {
                {"AX", Result.DRAW },{"AY", Result.WIN },{"AZ", Result.LOSS },
                {"BX", Result.LOSS },{"BY", Result.DRAW },{"BZ", Result.WIN },
                {"CX", Result.WIN },{"CY", Result.LOSS },{"CZ", Result.DRAW },
            };
            var results2 = new Dictionary<string, (Result,char)>
            {
                {"AX", (Result.LOSS,'Z') },{"AY", (Result.DRAW,'X') },{"AZ", (Result.WIN,'Y') },
                {"BX", (Result.LOSS,'X') },{"BY", (Result.DRAW,'Y') },{"BZ", (Result.WIN,'Z') },
                {"CX", (Result.LOSS,'Y') },{"CY", (Result.DRAW,'Z')},{"CZ", (Result.WIN,'X')},
            };
            var scores = new Dictionary<char, int>
            {
                {'X',1 },{'Y',2 },{'Z',3 }
            };
            var total1Score = 0;
            var total2Score = 0;
            foreach (var x in input)
            {
                var result1 = results1[x.ToString()];
                var result2 = results2[x.ToString()];
                var result1Score = (int)result1;
                var result2Score = (int)result2.Item1;
                var choice1Score = scores[x.Right];
                var choice2Score = scores[result2.Item2];
                total1Score += (result1Score + choice1Score);
                total2Score += (result2Score + choice2Score);

            }
            var answer = new Day2Result();
            answer.Part1 = total1Score;
            answer.Part2 = total2Score;
            return answer;
        }

        private enum Result
        {
            WIN = 6,LOSS = 0,DRAW = 3
        }
    }
}
