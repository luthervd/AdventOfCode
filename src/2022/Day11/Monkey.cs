namespace TwentyTwo;

public class Monkey
{
    
    private string[] _operationArgs;
    private (int divisibleTest, int onTrue, int onFalse) _throwConditions;
    public Monkey(IEnumerable<long> items, string[] operationArgs, (int divisibleTest, int onTrue, int onFalse) throwConditions)
    {
        Items = new Queue<long>(items);
        _operationArgs = operationArgs;
        _throwConditions = throwConditions;
    }

    public (long item, int throwTo)? ThrowNext(long commonDivisor)
    {
        if(Items.Count == 0)
        {
            return null;
        }
        else
        {
            InspectionCount++;
            var left = Items.Dequeue();
            var right = _operationArgs[2] == "old" ? left : long.Parse(_operationArgs[2]);
            left = _operationArgs[1] == "*" ? left*right : left+right;
            left = commonDivisor == 0 ? left / 3 : left % commonDivisor;
            return left % _throwConditions.divisibleTest == 0 ? (left, _throwConditions.onTrue) : (left, _throwConditions.onFalse);
        }

    }

    public Queue<long> Items { get; private set; }

    public long InspectionCount { get; private set; }

    public long Divisor => _throwConditions.divisibleTest;
}
