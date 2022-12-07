namespace TwentyTwo;

public class VirtualDirectory
{
    public VirtualDirectory(string name)
    {
        Name = name;
        Children = new Dictionary<string,VirtualDirectory>();
        Files = new List<VirtualFileInfo>();
    }

    public VirtualDirectory(string name, VirtualDirectory parent) : this(name)
    {
        Parent = parent;
    }

    public string Name { get; init; }

    public Dictionary<string,VirtualDirectory> Children { get; init; }

    public VirtualDirectory? Parent { get; init; }

    public List<VirtualFileInfo> Files { get; init; }

    public VirtualDirectory AddChild(string name)
    {
        var next = new VirtualDirectory(name, this);
        Children[name] = next;
        return next;
    }

    public VirtualDirectory MoveTo(string name)
    {
        return Children[name];
    }

    public long GetSize()
    {
        var totalSize = Files.Sum(x => x.FileSize);
        foreach(var child in Children.Values)
        {
            totalSize += child.GetSize();
        }
        return totalSize;
    }
}

public record VirtualFileInfo(string Name, long FileSize);