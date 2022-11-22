using DBMSCore.Exceptions;
using DBMSCore.Services;

namespace DBMSCore.Models;

public class Table
{
    public Table(string name)
    {
        Name = name;
    }
    
    public string Name { get; }
    public List<Column> Columns { get; init; } = new();
    public List<Row> Rows { get; init; } = new();

    public Row AddRow(List<string> values)
    {
        ValidateRowValues(values);
        HandlePngColumn(values);

        Row row = new(values);
        Rows.Add(row);

        return row;
    }

    public void DeleteRow(int rowIndex)
    {
        if (Rows.ElementAtOrDefault(rowIndex) is null) throw new NotFoundException("Row is not found");

        Rows.Remove(Rows[rowIndex]);
    }

    public Row EditRow(int rowIndex, List<string> newValues)
    {
        if (Rows.ElementAtOrDefault(rowIndex) is null) throw new NotFoundException("Row is not found");

        ValidateRowValues(newValues);
        HandlePngColumn(newValues);

        Row rowToEdit = Rows[rowIndex];
        for (int i = 0; i < newValues.Count; i++)
        {
            rowToEdit.Values[i] = newValues[i];
        }

        return rowToEdit;
    }

    private void ValidateRowValues(List<string> values)
    {
        if (values is null || values.Count == 0) throw new BadRequestException("Row should have at least 1 value"); 
        
        if (values.Count > Columns.Count) 
            throw new BadRequestException("Row length should not be more than table`s column length");

        for (int i = 0; i < values.Count; i++)
        {
            var value = values[i];
            var column = Columns[i];
            bool isValid = column.Validate(value);

            if (!isValid) throw new BadRequestException($"Value \"{value}\" is not assignable to {column.Type} type");
        }
    }
    
    private void HandlePngColumn(List<string> values)
    {
        for (int i = 0; i < values.Count; i++)
        {
            if (Columns[i].Type == "PNG")
            {
                string pngPath = values[i];
                
                try
                {
                    byte[] bytes = File.ReadAllBytes(pngPath);
                    string bytesAsBase64 = Convert.ToBase64String(bytes);

                    values[i] = bytesAsBase64;
                }
                catch (Exception)
                {
                    throw new BadRequestException($"Failed to read file {pngPath}");
                }
            };
        }
    }

    public Column AddColumn(string name, string type)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type)) 
            throw new BadRequestException("Column should have name and type");

        if (Columns.Any(x => x.Name == name)) throw new BadRequestException("Column name already exists");
        
        var column = ColumnFactory.GetColumnInstance(name, type);
        Columns.Add(column);

        return column;
    }

    public void DeleteColumn(int columnIndex)
    {
        if (Columns.ElementAtOrDefault(columnIndex) is null) throw new NotFoundException("Column is not found");
        
        foreach (var row in Rows)
        {
            if (row.Values.ElementAtOrDefault(columnIndex) is not null)
            {
                row.Values.RemoveAt(columnIndex);
            }
        }
        
        Columns.Remove(Columns[columnIndex]);
    }

    public Table SortByColumn(int columnIndex)
    {
        if (Columns.ElementAtOrDefault(columnIndex) is null) throw new NotFoundException("Column is not found");

        return new Table(Name)
        {
            Columns = Columns,
            Rows = Rows.OrderBy(x => x.Values.ElementAtOrDefault(columnIndex)).ToList()
        };
    }

    public byte[] DownloadPng(int columnIndex, int rowIndex)
    {
        var column = Columns.ElementAtOrDefault(columnIndex);
        if (column is null) throw new NotFoundException("Column is not found");
        if (column.Type != "PNG") throw new BadRequestException("Column type is not PNG");
        
        if (Rows.ElementAtOrDefault(rowIndex) is null) throw new NotFoundException("Row is not found");
        if (Rows[rowIndex].Values.ElementAtOrDefault(columnIndex) is null)
            throw new BadRequestException("Row value is empty");
        
        string base64Png = Rows[rowIndex].Values[columnIndex];
        return Convert.FromBase64String(base64Png);
    }
}