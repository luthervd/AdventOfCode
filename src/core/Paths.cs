namespace core
{
    public static class Paths
    {

        public static long Dijkstra<T>(EdgeWeightedDiGraph<T> graph, T start, T end) where T : notnull
        {
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
                        queue.Enqueue(next.To, newCost);
                    }

                }
            }
            return distanceFromStart.GetValueOrDefault(end, long.MaxValue);
        }

        public static long AStar<T>(EdgeWeightedDiGraph<T> graph, T start, T end, Func<T, T, long> heurisitc) where T : notnull
        {
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
                        var h = heurisitc.Invoke(next.To, end);
                        var priority = newCost + h;
                        queue.Enqueue(next.To, priority);
                    }
                }
            }
            return distanceFromStart.GetValueOrDefault(end, long.MaxValue);
        }

       
    }
}
