using System.Text;

namespace TwentyOne.Day4
{
    public class Board
    {
        private Tile[] _tiles = new Tile[25];
        
        private int _index = 0;
        private (int, bool)[][] _debug = new (int, bool)[5][];
        public Board(IList<string> args)
        {
            Build(args);
        }

        private void Build(IList<string> args)
        {
            var rowIndex = 1;
            var colIndex = 1;
            foreach (var row in args)
            {
                _debug[rowIndex - 1] = new (int,bool)[5];  
                var cols = row.Split(" ");
                foreach (var col in cols)
                {
                    if (!string.IsNullOrEmpty(col))
                    {
                        _debug[rowIndex-1][colIndex-1] = (int.Parse(col),false);
                        Add(int.Parse(col), rowIndex, colIndex);
                        colIndex++;
                    }
                }
                rowIndex++;
                colIndex = 1;
            }
        }

        private void Add(int number, int row, int col)
        {
            var tile = new Tile(number, row, col);
            _tiles[_index] = tile;
            _index++;
        }

        public bool CheckNumber(int number)
        {
            var tile = _tiles.FirstOrDefault(x => x.Number == number);
            if(tile != null)
            {
                tile.Marked = true;
                _debug[tile.Row-1][tile.Col-1].Item2 = true;
            }
            return BoardComplete();
        }

        public int GetResult(int lastNumber)
        {
            var result = _tiles.Where(x => !x.Marked).Sum(x => x.Number);
            return result * lastNumber;
        }

        public bool Finished = false;

        public bool BoardComplete()
        {
            var index = 1;
            while(index <= 5)
            {
                var matched = Check(index);
                if (matched)
                {
                    Finished = true;
                    return true;
                }
                index++;
            }
            
            return false;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            for(var i = 0; i < 5; i++)
            {
                var row = _debug[i];
                foreach(var col in row)
                {
                    if (col.Item2)
                    {
                        result.Append("X ");
                    }
                    else
                    {
                        result.Append($"{col.Item1} ");
                    }
                    
                }
                result.Append(Environment.NewLine);

            }
            return result.ToString();
        }

        private bool Check(int index)
        {
            var row = _tiles.Where(x => x.Row == index).All(x => x.Marked);
            var col = _tiles.Where(x => x.Col == index).All(x => x.Marked);
            return row || col;
        }

        private class Tile
        {
            public Tile(int number, int row, int col)
            {
                Number = number;
                Row = row;
                Col = col;
                Marked = false;
            }

            public int Number { get; }

            public int Row { get;  }

            public int Col { get;  }

            public bool Marked { get; set; }
        }
    }
}
