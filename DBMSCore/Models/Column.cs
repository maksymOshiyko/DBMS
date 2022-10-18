namespace DBMSCore.Models;

public abstract class Column
{
    protected Column(string name, string type)
    {
        Name = name;
        Type = type;
    }
    
    public string Name { get; }
    public string Type { get; }

    public abstract bool Validate(string value);
}