namespace DBMSCore.Dtos;

public class TableDto
{
    public int Index { get; set; }
    public string Name { get; set; }
    public List<ColumnDto> Columns { get; set; } = new();
    public List<RowDto> Rows { get; set; } = new();
}