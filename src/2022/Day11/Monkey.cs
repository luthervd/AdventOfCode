namespace TwentyTwo;

public class Monkey
{
    private Queue<long> _items;
    private string[] _operationArgs;
    private (int divisibleTest, int onTrue, int onFalse) _throwConditions;
    public Monkey(IEnumerable<long> items, string[] operationArgs, (int divisibleTest, int onTrue, int onFalse) throwConditions)
    {
        _items = new Queue<long>(items);
        _operationArgs = operationArgs;
        _throwConditions = throwConditions;
    }

    public (long item, int throwTo)? ThrowNext(long commonDivisor)
    {
        if(_items.Count == 0)
        {
            return null;
        }
        else
        {
            InspectionCount++;
            var left = _items.Dequeue();
            var right = _operationArgs[2] == "old" ? left : long.Parse(_operationArgs[2]);
            left = _operationArgs[1] == "*" ? left*right : left+right;
            if (commonDivisor == 0)
            {
                left = left / 3;
            }
            else
            {
                left = (left % commonDivisor);
            }
            if(left % _throwConditions.divisibleTest == 0)
            {
                return (left, _throwConditions.onTrue);
            }
            return (left, _throwConditions.onFalse);
        }

    }

    public long InspectionCount { get; private set; }

    public long Divisor => _throwConditions.divisibleTest;

    public void Receive(long item)
    {
        _items.Enqueue(item);
    }

    public bool HasNext()
    {
        return _items.Any();
    }
}
