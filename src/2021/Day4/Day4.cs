using core;

namespace TwentyOne.Day4
{
    public class Day4 : IPuzzle<BoardArgs, int>
    {
        private const string FilePath = "Day4/Day4Data.txt";
        private List<Board> _boards = new List<Board>();

        public BoardArgs LoadArgs()
        {
            var initialArgs = File.ReadAllLines(FilePath);
            var args = new BoardArgs();
            args.Balls = initialArgs[0].Split(",").ToList();
            var nextBoard = new List<string>();
            for(var index = 2; index < initialArgs.Length; index++)
            {
                nextBoard.Add(initialArgs[index]);
                if(nextBoard.Count == 5)
                {
                    args.Boards.Add(nextBoard);
                    index++;
                    nextBoard = new List<string>();
                }
            }
            return args;
        }

        public int Run(BoardArgs input)
        {
            foreach(var boardArgs in input.Boards)
            {
                var board = new Board(boardArgs);
                _boards.Add(board);
            }
            var numberOfWinners = 0;
            foreach(var ball in input.Balls.Select(x => int.Parse(x)))
            {
                foreach(var board in _boards)
                {
                    if (!board.Finished)
                    {
                        var result = board.CheckNumber(ball);
                        if (result && numberOfWinners == _boards.Count - 1)
                        {
                            var answer = board.GetResult(ball);
                            var visual = board.ToString();
                            return answer;
                        }
                        else if (result)
                        {
                            numberOfWinners++;
                        }
                    }
                   
                }
            }
            return -1;
        }
    }
}
