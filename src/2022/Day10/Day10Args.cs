using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyTwo;

public enum Instruction
{
    None,
    AddX
}

public class Day10Args
{
    public List<(Instruction, int)> Values { get; set; }
}