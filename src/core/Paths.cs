namespace core
{
    public static class Paths
    {

        public static (long Distance, IEnumerable<T> Paths) Dijkstra<T>(EdgeWeightedDiGraph<T> graph, T start, T end) where T : notnull
        {
            var edges = new Dictionary<T, DirectedWeightedEdge<T>>();
            var queue = new PriorityQueue<T, long>();
            queue.Enqueue(start, 0);
            var distanceFromStart = new Dictionary<T, long> { [start] = 0 };

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current.Equals(end)) { break; }
                foreach (var next in graph.Neighbours(current))
                {
                    var newCost = distanceFromStart[current] + next.Weight;
                    if (!distanceFromStart.TryGetValue(next.To, out long otherCost) || newCost < otherCost)
                    {
                        distanceFromStart[next.To] = newCost;
                        edges[next.To] = next;
                        queue.Enqueue(next.To, newCost);
                    }

                }
            }
            var distance  = distanceFromStart.GetValueOrDefault(end, long.MaxValue);
            var path = new Stack<T>();
            if (!edges.ContainsKey(end)) return (distance, path);
            else
            {
                var edge = edges[end];
                path.Push(edge.To);
                while(edge != null)
                {
                    edge = edges.ContainsKey(edge.From) ? edges[edge.From] : null;
                    if (edge != null)
                    {
                        path.Push(edge.To);
                    }
                }

            }
            return (distance, path);
        }

        public static (long Distance, IEnumerable<T> Paths) AStar<T>(EdgeWeightedDiGraph<T> graph, T start, T end, Func<T, T, long> heurisitc) where T : notnull
        {
            var edges = new Dictionary<T, DirectedWeightedEdge<T>>();
            var queue = new PriorityQueue<T, long>();
            queue.Enqueue(start, 0);

            var distanceFromStart = new Dictionary<T, long> { [start] = 0 };

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current.Equals(end))
                {
                    break;
                }

                foreach (var next in graph.Neighbours(current))
                {
                    var newCost = distanceFromStart[current] + heurisitc.Invoke(current, next.To);
                    if (!distanceFromStart.TryGetValue(next.To, out long otherCost) || newCost < otherCost)
                    {
                        distanceFromStart[next.To] = newCost;
                        edges[next.To] = next;
                        var h = heurisitc.Invoke(next.To, end);
                        var priority = newCost + h;
                        queue.Enqueue(next.To, priority);
                    }
                }
            }
            var distance = distanceFromStart.GetValueOrDefault(end, long.MaxValue);
            var path = new Stack<T>();
            if (!edges.ContainsKey(end)) return (distance, path);
            else
            {
                var edge = edges[end];
                path.Push(edge.To);
                while (edge != null)
                {
                    edge = edges.ContainsKey(edge.From) ? edges[edge.From] : null;
                    if(edge != null)
                    {
                        path.Push(edge.To);
                    }
                   
                }

            }
            return (distance, path);
        }

       
    }
}
