using core;
namespace TwentyTwo;

public class Day7 : IPuzzle<Day7Args, Day7Results>
{
    private VirtualDirectory? _directoryPointer;
   
    private List<VirtualDirectory> _cache = new List<VirtualDirectory>();
    
    private const long TotalDiskSize = 70_000_000;
    
    private const long RequiredSize =  30_000_000;
    
    public Day7Args LoadArgs()
    {
        var content = File.ReadAllLines("Day7/Day7Data.txt");
        return new Day7Args
        {
            Args = content
        };
    }

    public Day7Results Run(Day7Args input)
    {
        var results = new Day7Results();
        foreach(var arg in input.Args)
        {
            if (arg.StartsWith("$"))
            {
                HandleCommand(arg);
            }
            else
            {
                HandleContent(arg);
            }
        }
        //Part 1
        var toDelete = _cache.Where(x => x.GetSize() <= 100000);
        var deleteTotal = toDelete.Sum(x => x.GetSize());
        results.Part1 = deleteTotal;
        //Part 2 
        var root = _directoryPointer;
        while(root?.Parent != null)
        {
            root = root.Parent;
        }

        var freeSpace = TotalDiskSize - root?.GetSize();
        var stillRequired = RequiredSize - freeSpace;

        var bestCandidate = _cache
                            .Where(x => x.GetSize() >= stillRequired)
                            .OrderBy(x => x.GetSize())
                            .FirstOrDefault();
        results.Part2 = bestCandidate != null ? bestCandidate.GetSize() : -1;
        return results;

    }

    private void HandleCommand(string command)
    {
        var args = command.Split(" ");
        switch (args[1])
        {
            case "cd":
                HandleCd(args[2]);
                break;
        }
    }

    private void HandleCd(string option)
    {
        if(option == "/")
        {
            var root = new VirtualDirectory(option);
            _directoryPointer = root;
            _cache.Add(root);
        }
        else if(option == "..")
        {
            _directoryPointer = _directoryPointer?.Parent;
        }
        else
        {
            _directoryPointer = _directoryPointer?.MoveTo(option);
        }
    }

    private void HandleContent(string arg)
    {
        var contentArgs = arg.Split(" ");
        if (contentArgs[0] == "dir")
        {
            var child = _directoryPointer?.AddChild(contentArgs[1]);
            if(child != null)
            {
                _cache.Add(child);
            }
        }
        else
        {
            var fileInfo = new VirtualFileInfo(contentArgs[1], long.Parse(contentArgs[0]));
            _directoryPointer?.Files.Add(fileInfo);
        }
    }
}
