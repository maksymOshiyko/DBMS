namespace DBMSCore.Models;

public class Table
{
    public Table(string name)
    {
        Name = name;
    }

    public string Name { get; }
    public List<Column> Columns { get; }
    public List<Row> Rows { get; }

    public Row AddRow(List<string> values)
    {
        
    }

    public void DeleteRow(int rowIndex)
    {
        
    }

    public Row EditRow(int rowIndex, List<string> newValues)
    {
        
    }

    public Column AddColumn(string name, string type)
    {
        
    }

    public void DeleteColumn(int columnIndex)
    {
        
    }

    public Column RenameColumn(int columnIndex, string newName)
    {
        
    }

    public Table SortByColumn(int columnIndex)
    {
        
    }
}