using core;
using System.ComponentModel.DataAnnotations;
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
        var graph = new EdgeWeightedDiGraph<Point, int>();
        foreach(var point in input.PathGrid.Nodes)
        {
            var weightedEdges = ToWeightedEdge(point.Key,point.Value, input.PathGrid);
            foreach(var edge in weightedEdges)
            {
                graph.AddEdge(edge);
            }
        }
        //TODO add edges
        //TODO calculate A* path
        return new Day12Results();
    }

    private List<DirectedWeightedEdge<Point,int>> ToWeightedEdge(Point node, char nodeValue, PathGrid<char> grid)
    {
        var result = new List<DirectedWeightedEdge<Point,int>>();
        var paths = new List<Point> { new Point(node.X, node.Y+1), new Point(node.X+1, node.Y), new Point(node.X, node.Y - 1), new Point(node.X - 1, node.Y) };
        foreach(var path in paths)
        {
            if (grid.Nodes.ContainsKey(path))
            {
                var item = grid.Nodes[path];
                var weight = nodeValue == 'S' || nodeValue == 'E' ? 1 : item - nodeValue;
                if(weight == 0 || weight == 1)
                {
                    var edge = new DirectedWeightedEdge<Point, int>(node, path, weight);
                    result.Add(edge);
                }
                
            }
        }
        return result;

    }
}
