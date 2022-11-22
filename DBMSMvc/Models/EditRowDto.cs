namespace DBMSMvc.Models;

public class EditRowDto
{
    public List<string> Values { get; set; }
    public int TableIndex { get; set; }
    public int RowIndex { get; set; }
}