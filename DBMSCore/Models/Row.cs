namespace DBMSCore.Models;

public class Row
{
    public Row(List<string> values)
    {
        Values = values;
    }
    
    public List<string> Values { get; } 
}