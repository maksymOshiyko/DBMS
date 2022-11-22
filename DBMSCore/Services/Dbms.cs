using DBMSCore.Dtos;
using DBMSCore.Exceptions;
using DBMSCore.Interfaces;
using DBMSCore.Models;
using Newtonsoft.Json;

namespace DBMSCore.Services;

public class Dbms : IDbms
{
    private Database? _database;

    public Database CreateDatabase(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new BadRequestException("Database should have name");

        _database = new Database(name);
        return _database;
    }

    public Database GetDatabase()
    {
        if (_database is null) throw new BadRequestException("Database is not created");
        return _database;
    }

    public void SaveDatabase(string path)
    {
        if (_database is null) throw new BadRequestException("Database is not created");

        try
        {
            string json = JsonConvert.SerializeObject(_database, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            using StreamWriter writer = new(path, new FileStreamOptions
            {
                Mode = FileMode.OpenOrCreate,
                Access = FileAccess.Write,
            });
            writer.Write(json);
        }
        catch (Exception)
        {
            throw new BadRequestException("Something went wrong when saving database");
        }
    }

    public Database UploadDatabase(string path)
    {
        try
        {
            using StreamReader streamReader = new(path, new FileStreamOptions
            {
                Mode = FileMode.Open,
                Access = FileAccess.Read
            });
            string json = streamReader.ReadToEnd();

            var database = JsonConvert.DeserializeObject<Database>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            _database = database ?? throw new Exception();
            return _database;
        }
        catch (Exception)
        {
            throw new BadRequestException("Something went wrong when uploading database");
        }
    }

    public TableDto AddTable(string name)
    {
        if (_database is null) throw new BadRequestException("Database is not created");
        var table = _database.AddTable(name);
        return new TableDto
        {
            Index = _database.Tables.IndexOf(table),
            Name = table.Name,
            Columns = table.Columns.Select((c, i) => new ColumnDto {Index = i, Name = c.Name, Type = c.Type}).ToList(),
            Rows = table.Rows.Select((r, i) => new RowDto {Index = i, Values = r.Values}).ToList(),
        };
    }

    public void DeleteTable(int tableIndex)
    {
        if (_database is null) throw new BadRequestException("Database is not created");
        _database.DeleteTable(tableIndex);
    }

    public RowDto AddRow(int tableIndex, List<string> values)
    {
        CheckTableExists(tableIndex);
        var row = _database!.Tables[tableIndex].AddRow(values);
        return new RowDto
        {
            Index = _database.Tables[tableIndex].Rows.IndexOf(row),
            Values = row.Values
        };
    }

    public void DeleteRow(int tableIndex, int rowIndex)
    {
        CheckTableExists(tableIndex);
        _database!.Tables[tableIndex].DeleteRow(rowIndex);
    }

    public RowDto EditRow(int tableIndex, int rowIndex, List<string> newValues)
    {
        CheckTableExists(tableIndex);
        var row = _database!.Tables[tableIndex].EditRow(rowIndex, newValues);
        return new RowDto
        {
            Index = rowIndex,
            Values = row.Values
        };
    }

    public ColumnDto AddColumn(int tableIndex, string name, string type)
    {
        CheckTableExists(tableIndex);
        var column = _database!.Tables[tableIndex].AddColumn(name, type);
        return new ColumnDto
        {
            Index = _database.Tables[tableIndex].Columns.IndexOf(column),
            Name = column.Name,
            Type = column.Type
        };
    }

    public void DeleteColumn(int tableIndex, int columnIndex)
    {
        CheckTableExists(tableIndex);
        _database!.Tables[tableIndex].DeleteColumn(columnIndex);
    }

    public TableDto SortByColumn(int tableIndex, int sortColumnIndex)
    {
        CheckTableExists(tableIndex);
        var table = _database!.Tables[tableIndex].SortByColumn(sortColumnIndex);
        return new TableDto
        {
            Index = tableIndex,
            Name = table.Name,
            Columns = table.Columns.Select((c, i) => new ColumnDto {Index = i, Name = c.Name, Type = c.Type}).ToList(),
            Rows = table.Rows.Select((r, i) => new RowDto {Index = i, Values = r.Values}).ToList(),
        };
    }

    public byte[] DownloadPng(int tableIndex, int columnIndex, int rowIndex)
    {
        CheckTableExists(tableIndex);
        return _database!.Tables[tableIndex].DownloadPng(columnIndex, rowIndex);
    }

    public TableDto GetTable(int tableIndex)
    {
        CheckTableExists(tableIndex);
        var table = _database!.Tables[tableIndex];
        return new TableDto
        {
            Index = tableIndex,
            Name = table.Name,
            Columns = table.Columns.Select((c, i) => new ColumnDto {Index = i, Name = c.Name, Type = c.Type}).ToList(),
            Rows = table.Rows.Select((r, i) => new RowDto {Index = i, Values = r.Values}).ToList(),
        };
    }

    public bool IsDatabaseCreated()
    {
        return _database is not null;
    }


    private void CheckTableExists(int tableIndex)
    {
        if (_database is null) throw new BadRequestException("Database is not created");
        if (_database.Tables.ElementAtOrDefault(tableIndex) is null)
            throw new NotFoundException("Table is not found");
    }
}