using System.Drawing;

namespace core
{
    public class PathGrid<T> where T : IEquatable<T>
    {
        private Point? _start;
        private Point? _end;
        private T _startValue;
        private T _endValue;

        public PathGrid(int rows, int cols, Func<int, int, T> nodeSelector, T start, T end)
        {
            _startValue = start;
            _endValue = end;
            Nodes = new Dictionary<Point, T>();
            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < cols; c++)
                {
                    var point = new Point(c, r);
                    var item = nodeSelector.Invoke(c, r);
                    if (item.Equals(start))
                    {
                        if (_start != null)
                        {
                            throw new ArgumentException("Start value must be unique");
                        }
                        _start = point;
                    }
                    else if (item.Equals(end))
                    {
                        if (_end != null)
                        {
                            throw new ArgumentException("End value must be unique");
                        }
                        _end = point;
                    }
                    Nodes[point] = item;
                }
            }
        }

        public Dictionary<Point, T> Nodes { get; init; }

        public (Point, T) Start
        {
            get
            {
                if(_start == null)
                {
                    throw new ArgumentException("No start point");
                }
                return (_start.Value, _startValue);
            }
        }

        public (Point,T) End
        {
            get
            {
                if (_end == null)
                {
                    throw new ArgumentException("No end point");
                }
                return (_end.Value, _endValue);
            }
        }


    }
}
