namespace core
{
    public class DirectedWeightedEdge<TNode, TWeight> where TWeight : IComparable<TWeight>
    {
        public DirectedWeightedEdge(TNode from, TNode to, TWeight weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }

        public TNode From { get; init; }

        public TNode To { get; init; }

        public TWeight Weight { get; set; }
    }
}
