using core;
using System.Drawing;

namespace TwentyTwo;

public class Day14 : IPuzzle<Day14Args, Day14Results>
{
    private int LowestPoint = 0;
    private int FarthestPoint = 0;
    private int NearestPoint = 0;
    private bool FallingToInfinity = false;
  
    public Day14Args LoadArgs()
    {
        var args = new Day14Args();
        var lines = File.ReadAllLines("Day14/Day14Data.txt");
        foreach (var line in lines)
        {
            var points = new List<(Point From, Point To)>();
            var lineArgs = line.Split("->");
            for (var i = 0; i < lineArgs.Length - 1; i++)
            {
                var from = ToPoint(lineArgs[i].Split(","));
                var to = ToPoint(lineArgs[i + 1].Split(","));
                points.Add((from, to));
            }
            args.Points.Add(points);
        }
        return args;
    }

    public Day14Results Run(Day14Args input)
    {
        var part1 = Part1(input);
        var part2 = Part2(input);
        
        return new Day14Results
        {
            Part1 = part1,
            Part2 = part2
        };
    }


    private int Part1(Day14Args input)
    {
        var points = SetObstacles(input);
        int numberOfSandPieces = 0;
        while (!FallingToInfinity)
        {
            var sand = new Sand(500);
            numberOfSandPieces++;
            while (!sand.IsSettled)
            {
                sand.Move(points);
                if (sand.Point.Y > LowestPoint)
                {
                    FallingToInfinity = true;
                    break;
                }
            }
            if (!FallingToInfinity)
            {
                points.Add(sand.Point);
            }

        }
        return numberOfSandPieces - 1;
    }

    private int Part2(Day14Args input)
    {
        var points = SetObstacles(input);
        var start = new Point(500, 0);
        var bottomFloor = LowestPoint + 2;
        var (floorFrom, floorTo) = SetFloor(points);
        int numberOfSandPieces = 0;
        var isBlockingTop = false;
        while (!isBlockingTop)
        {
            var sand = new Sand(500);
            numberOfSandPieces++;
            while (!sand.IsSettled)
            {
                sand.Move(points);
            }
            if(sand.Point == start)
            {
                isBlockingTop = true;
            }
            else if(sand.Point.Y == bottomFloor-1)
            {
                (floorFrom,floorTo) = AddToFloor(points, bottomFloor, floorFrom, floorTo);
               
            }
            if(sand.Point.X < floorFrom || sand.Point.X > floorTo)
            {
                throw new ArgumentException("Out of bounds");
            }
            var added = points.Add(sand.Point);
            if (!added)
            {
                throw new ArgumentException("Out of bounds");
            }
        }
        return numberOfSandPieces;
    }


    private (int left, int right) SetFloor(HashSet<Point> points)
    {
        var bottomFloorY = LowestPoint + 2;
        var floorFrom = NearestPoint - 200;
        var floorTo = FarthestPoint + 200;
        var floorStart = new Point(floorFrom, bottomFloorY);
        var floorEnd = new Point(floorTo, bottomFloorY);
        var floor = new Obstacle(new[] { (floorStart, floorEnd) });
        foreach (var point in floor.Points)
        {
            points.Add(point);
        }
        return (floorFrom, floorTo);
    }

    private (int left, int right) AddToFloor(HashSet<Point> points, int y, int left, int right)
    {
        var leftStart = left - 100;
        var rightEnd = right + 100;
        var l = new Point(leftStart, y);
        var r = new Point(right, y);
        var leftObstacle = new Obstacle(new List<(Point, Point)> { (new Point(leftStart, y), new Point(left, y)) });
        var rightObstacle = new Obstacle(new List<(Point, Point)> { (new Point(right, y), new Point(rightEnd, y)) });
        foreach(var point in leftObstacle.Points)
        {
            points.Add(point);
        }
        foreach (var point in rightObstacle.Points)
        {
            points.Add(point);
        }
        return (leftStart, rightEnd);
    }

    private HashSet<Point> SetObstacles(Day14Args args)
    {
        var obstacles = new List<Obstacle>();
        foreach (var arg in args.Points)
        {
            var obstacle = new Obstacle(arg);
            if (obstacle.LowestPoint > LowestPoint)
            {
                LowestPoint = obstacle.LowestPoint;
            }
            if (obstacle.FarthestPoint > FarthestPoint)
            {
                FarthestPoint = obstacle.FarthestPoint;
            }
            if (NearestPoint == 0)
            {
                NearestPoint = obstacle.NearestPoint;
            }
            else if (obstacle.NearestPoint < NearestPoint)
            {
                NearestPoint = obstacle.NearestPoint;
            }
            obstacles.Add(obstacle);
            
        }
        return obstacles.SelectMany(x => x.Points).ToHashSet();
    }



    private Point ToPoint(string[] points) => new Point(int.Parse(points[0]), int.Parse(points[1]));


}

