using DBMSCore.Dtos;

namespace DBMSMvc.Models;

public class EditRowViewModel
{
    public TableDto Table { get; set; }
    public int RowIndex { get; set; }
}