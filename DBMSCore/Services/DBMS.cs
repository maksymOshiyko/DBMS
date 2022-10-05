using DBMSCore.Interfaces;
using DBMSCore.Models;

namespace DBMSCore.Services;

public class DBMS : IDBMS
{
    private Database _database;
    
    public Database CreateDatabase(string name)
    {
        _database = new Database(name);
        return _database;
    }

    public Database GetDatabase()
    {
        return _database;
    }

    public byte[] DownloadDatabase()
    {
        throw new NotImplementedException();
    }

    public void SaveDatabase(string path)
    {
        throw new NotImplementedException();
    }

    public Database UploadDatabase(byte[] file)
    {
        throw new NotImplementedException();
    }

    public Table AddTable(string name)
    {
        return _database.AddTable(name);
    }

    public void DeleteTable(int tableIndex)
    {
        _database.DeleteTable(tableIndex);
    }

    public Row AddRow(int tableIndex, List<string> values)
    {
        return _database.Tables[tableIndex].AddRow(values);
    }

    public void DeleteRow(int tableIndex, int rowIndex)
    {
        _database.Tables[tableIndex].DeleteRow(rowIndex);
    }

    public Row EditRow(int tableIndex, int rowIndex, List<string> newValues)
    {
        return _database.Tables[tableIndex].EditRow(rowIndex, newValues);
    }

    public Column AddColumn(int tableIndex, string name, string type)
    {
        return _database.Tables[tableIndex].AddColumn(name, type);
    }

    public void DeleteColumn(int tableIndex, int columnIndex)
    {
        _database.Tables[tableIndex].DeleteColumn(columnIndex);
    }

    public Table SortByColumn(int tableIndex, int sortColumnIndex)
    {
        return _database.Tables[tableIndex].SortByColumn(sortColumnIndex);
    }
}