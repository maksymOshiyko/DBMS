namespace DBMSCore.Models;

public abstract class Column
{
    public string Name { get; }

    protected Column(string name)
    {
        Name = name;
    }

    public abstract bool Validate(string value);
}