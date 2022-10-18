using DBMSCore.Exceptions;

namespace DBMSCore.Models;

public class Database
{
    public Database(string name)
    {
        Name = name;
    }
    
    public string Name { get; }
    public List<Table> Tables { get; } = new();

    public Table AddTable(string name)
    {
        if (Tables.Any(x => x.Name == name)) throw new BadRequestException("Table name already exists");
        
        var table = new Table(name);
        Tables.Add(table);
        return table;
    }

    public void DeleteTable(int tableIndex)
    {
        if (Tables.ElementAtOrDefault(tableIndex) is null) throw new NotFoundException("Table is not found");

        Tables.Remove(Tables[tableIndex]);
    }
}