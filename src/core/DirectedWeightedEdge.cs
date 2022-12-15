namespace core
{
    public class DirectedWeightedEdge<TNode>
    {
        public DirectedWeightedEdge(TNode from, TNode to, long weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }

        public TNode From { get; init; }

        public TNode To { get; init; }

        public long Weight { get; set; }
    }
}
