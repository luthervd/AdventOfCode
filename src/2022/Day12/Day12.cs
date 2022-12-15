using core;
using System.Drawing;

namespace TwentyTwo;

public class Day12 : IPuzzle<Day12Args, Day12Results>
{
    public Day12Args LoadArgs()
    {
        var lines = File.ReadAllLines("Day12/Day12Data.txt");
        var args = new Day12Args(lines);
        return args;
    }

    public Day12Results Run(Day12Args input)
    {
        var graph = new EdgeWeightedDiGraph<Point>((d) => d.Weight <= 1);
        graph.AddEdges(input.PathGrid.Nodes.SelectMany(x => ToWeightedEdge(x.Key, x.Value, input.PathGrid)));
        return new Day12Results
        {
            Part1 = Paths.AStar(graph, input.PathGrid.Start.Item1, input.PathGrid.End.Item1, Manhattan),
            Part2 = input.PathGrid.Nodes.Where(x => x.Value == 'a').Select(x => Paths.AStar(graph, x.Key, input.PathGrid.End.Item1, Manhattan)).Min()
        };
    }

    private long Manhattan(Point left, Point right) => Math.Abs(left.X - right.X) + Math.Abs(left.Y - right.Y);

    private List<DirectedWeightedEdge<Point>> ToWeightedEdge(Point node, char nodeValue, PathGrid<char> grid)
    {
        var result = new List<DirectedWeightedEdge<Point>>();
        var paths = new List<Point> { new Point(node.X, node.Y+1), new Point(node.X+1, node.Y), new Point(node.X, node.Y - 1), new Point(node.X - 1, node.Y) };
        foreach(var path in paths)
        {
            if (grid.Nodes.ContainsKey(path))
            {
                var item = grid.Nodes[path] == 'S' ? 'a' : grid.Nodes[path] == 'E' ? 'z' : grid.Nodes[path];
                nodeValue = nodeValue == 'S' ? 'a' : nodeValue == 'E' ? 'z' : nodeValue;
                var weight = item - nodeValue;
                if(weight < 1)
                {
                    weight = 1;
                }
                var edge = new DirectedWeightedEdge<Point>(node, path, weight);
                result.Add(edge);
            }
        }
        return result;
    }
}
