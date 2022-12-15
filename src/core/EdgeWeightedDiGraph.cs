using System.Runtime.InteropServices;

namespace core
{
    public class EdgeWeightedDiGraph<TNode> where TNode : notnull
    {
        private Dictionary<TNode,LinkedList<DirectedWeightedEdge<TNode>>> _adjacent;
        private Func<DirectedWeightedEdge<TNode>, bool> _neighbourSelector;

        public EdgeWeightedDiGraph(Func<DirectedWeightedEdge<TNode>,bool> neighbourSelector)
        {
            
            _adjacent = new Dictionary<TNode,LinkedList<DirectedWeightedEdge<TNode>>>();
            _neighbourSelector = neighbourSelector;
        }

        public int Vertices => _adjacent.Count;

        public void AddEdge(DirectedWeightedEdge<TNode> edge)
        {
            if (!_adjacent.ContainsKey(edge.From))
            {
                _adjacent[edge.From] = new LinkedList<DirectedWeightedEdge<TNode>>();
            }
            _adjacent[edge.From].AddLast(edge);
        }

        public void AddEdges(IEnumerable<DirectedWeightedEdge<TNode>> edges)
        {
            foreach(var edge in edges)
            {
                AddEdge(edge);
            }
        }

        public IEnumerable<DirectedWeightedEdge<TNode>> Neighbours(TNode node)
        {
            if (_adjacent.ContainsKey(node))
            {
                foreach(var n in _adjacent[node])
                {
                    if (_neighbourSelector.Invoke(n))
                    {
                        yield return n;
                    }
                }
            }
        }
    }
}
