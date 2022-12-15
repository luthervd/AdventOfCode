namespace core
{
    public class EdgeWeightedDiGraph<TNode,TWeight> where TWeight : IComparable<TWeight>
    {
        private Dictionary<TNode,LinkedList<DirectedWeightedEdge<TNode, TWeight>>> _adjacent;

        public EdgeWeightedDiGraph()
        {
            
            _adjacent = new Dictionary<TNode,LinkedList<DirectedWeightedEdge<TNode, TWeight>>>();
        }

        public int Vertices { get; init; }

        public void AddEdge(DirectedWeightedEdge<TNode,TWeight> edge)
        {
            if (!_adjacent.ContainsKey(edge.From))
            {
                _adjacent[edge.From] = new LinkedList<DirectedWeightedEdge<TNode, TWeight>>();
            }
            _adjacent[edge.From].AddLast(edge);
        }
    }
}
