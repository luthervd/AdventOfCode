﻿using core;

namespace TwentyTwo;

public class Day8 : IPuzzle<Day8Args, Day8Result>
{
    public Day8Args LoadArgs()
    {
        var lines = File.ReadAllLines("Day8/Day8Data.txt");
        var rowCount = lines.Length;
        var colCount = lines[0].Length;
        var args = new Day8Args(rowCount, colCount);
        for (var i = 0; i < rowCount; i++)
        {
            var line = lines[i];
            for (var y = 0; y < colCount; y++)
            {
                var tree = int.Parse(line[y].ToString());
                args.Grid[i, y] = tree;
            }
        }
        return args;
    }

    public Day8Result Run(Day8Args input)
    {
        {
            var edgeCount = (input.ColCount * 2) + ((input.RowCount - 2) * 2);
            var visibleCount = 0;
            var scenicScores = new List<int>();
            for (var i = 1; i < input.RowCount - 1; i++)
            {
                for (var y = 1; y < input.ColCount - 1; y++)
                {
                    var toCheck = input.Grid[i, y];
                    bool upClear = true, downClear = true, leftClear = true, rightClear = true;
                    int upDistance = 0, downDistance = 0, leftDistance = 0, rightDistance = 0;

                    //Sweep up
                    for (var up = i - 1; up >= 0; up--)
                    {
                        var next = input.Grid[up, y];
                        upClear = toCheck > next;
                        upDistance++;
                        if (!upClear)
                        {
                            break;
                        }
                        
                    }
                    
                    //Sweep down
                    for (var down = i + 1; down < input.RowCount; down++)
                    {
                        var next = input.Grid[down, y];
                        downClear = toCheck > next;
                        downDistance++;
                        if (!downClear)
                        {
                            break;
                        }
                        
                    }

                    //Sweep left
                    for (var left = y - 1; left >= 0; left--)
                    {
                        var next = input.Grid[i, left];
                        leftClear = toCheck > next;
                        leftDistance++;
                        if (!leftClear)
                        {
                            break;
                        }
                        
                    }

                    //Sweep right
                    for (var right = y + 1; right < input.ColCount; right++)
                    {
                        var next = input.Grid[i, right];
                        rightClear = toCheck > next;
                        rightDistance++;
                        if (!rightClear)
                        {
                            break;
                        }
                        
                    }

                    //Check visibility
                    if (leftClear || rightClear || upClear || downClear)
                    {
                        visibleCount++;
                    }
                    var scenicScore = upDistance * leftDistance * downDistance * rightDistance;
                    scenicScores.Add(scenicScore);
                }
                
            }

            return new Day8Result
            {
                Part1 = edgeCount + visibleCount,
                Part2 = scenicScores.Max()
            };
        }
    }
}
