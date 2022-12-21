namespace TwentyTwo;

public class Packet : IComparable<Packet>
{
    
    public Packet()
    {
        Values = new List<Packet>();
    }

    public Packet(int value)
    {
        Value = value;
    }

    public int? Value { get; }

    public List<Packet> Values { get; }

    public int CompareTo(Packet? other)
    {
        if (Value.HasValue && other.Value.HasValue)
        {
            return Value.Value.CompareTo(other.Value.Value);
        }
        else if (!Value.HasValue && !other.Value.HasValue)
        {
            for (var i = 0; i < Values.Count && i < other.Values.Count; i++)
            {
                var comparison = Values[i].CompareTo(other.Values[i]);
                if (comparison != 0)
                {
                    return comparison;
                }
            }
            return Values.Count.CompareTo(other.Values.Count);
        }
        else
        {
            Packet packet = new Packet();
            if (Value.HasValue)
            {
                packet.Values.Add(new Packet(Value.Value));
                return packet.CompareTo(other);
            }
            else
            {
                packet.Values.Add(new Packet(other.Value.Value));
                return CompareTo(packet);
            }
        }
    }
}
