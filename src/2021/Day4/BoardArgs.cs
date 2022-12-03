using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne.Day4
{
    public class BoardArgs
    {
        public List<List<string>> Boards { get; set; } = new List<List<string>>();

        public List<string> Balls { get; set; } = new List<string>();

        public int BoardSize => 5;
    }
}
