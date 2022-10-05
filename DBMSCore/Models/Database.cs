namespace DBMSCore.Models;

public class Database
{
    public Database(string name)
    {
        Name = name;
    }
    
    public string Name { get; set; }
    public List<Table> Tables { get; } = new();

    public Table AddTable(string name)
    {
        Tables.Add(new Table(name));
    }

    public void DeleteTable(int tableIndex)
    {
        
    }
}