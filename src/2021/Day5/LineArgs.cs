namespace TwentyOne.Day5
{
    public class LineArgs
    {
        public int X1 { get; set; }

        public int Y1 { get; set; }

        public int X2 { get; set; }

        public int Y2 { get; set; }
        
        public List<string> GetPointArgs()
        {
            if(LineDirection == LineDirection.Horizontal)
            {
                return GetHorizontalPoints();
            }
            else if (LineDirection == LineDirection.Vertical)
            {
                return GetVerticalPoints();
            }
            return GetDiagonalPoints();
        }

        private List<string> GetHorizontalPoints()
        {
            var result = new List<string>();
            var currentPoint = X1;
            if (currentPoint > X2)
            {
                while (currentPoint >= X2)
                {
                    result.Add($"{currentPoint},{Y1}");
                    currentPoint--;
                }
            }
            else
            {
                while (currentPoint <= X2)
                {
                    result.Add($"{currentPoint},{Y1}");
                    currentPoint++;
                }
            }
            return result;
        }

        private List<string> GetVerticalPoints()
        {
            var result = new List<string>();
            var currentPoint = Y1;
            if (currentPoint > Y2)
            {
                while (currentPoint >= Y2)
                {
                    result.Add($"{X1},{currentPoint}");
                    currentPoint--;
                }
            }
            else
            {
                while (currentPoint <= Y2)
                {
                    result.Add($"{X1},{currentPoint}");
                    currentPoint++;
                }
            }
            return result;
        }

        private List<string> GetDiagonalPoints()
        {
            var result = new List<string>();
            var xDirection = X1 > X2 ? Direction.Left : Direction.Right;
            var yDirection = Y1 < Y2 ? Direction.Down : Direction.Up;
            var currentX = X1;
            var currentY = Y1;
            while(currentX != X2)
            {
                result.Add($"{currentX},{currentY}");
                if(xDirection == Direction.Left)
                {
                    currentX--;
                }
                else
                {
                    currentX++;
                }
                if(yDirection == Direction.Down)
                {
                    currentY++;
                }
                else
                {
                    currentY--;
                }
            }
            result.Add($"{currentX},{currentY}");
            return result;
        }

        public LineDirection LineDirection => X1 == X2 ? LineDirection.Vertical : Y1 == Y2 ? LineDirection.Horizontal : LineDirection.Diagonal;

    }

    public enum LineDirection
    {
        Horizontal,
        Vertical,
        Diagonal
    }

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }


}
