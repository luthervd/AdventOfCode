namespace TwentyOne.Day5
{
    public class PointArgs
    {
        private List<LineArgs> _diagonal = new List<LineArgs>();
        
        private List<LineArgs> _straight  = new List<LineArgs>();

        public void AddLineArg(LineArgs lineArgs)
        {
            if (lineArgs.LineDirection == LineDirection.Horizontal || lineArgs.LineDirection == LineDirection.Vertical)
            {
                _straight.Add(lineArgs);
            }
            else
            {
                _diagonal.Add(lineArgs);
            }
        }

        public IEnumerable<LineArgs> GetLines(bool straightOnly = true)
        {
            if (straightOnly)
            {
                return _straight;
            }
            else
            {
                var result =  new List<LineArgs>(_straight);
                result.AddRange(_diagonal);
                return result;
            }
        }

        //public int XSize { get; set; }

        //public int YSize { get; set; }
    }
}
