namespace DBMSCore.Models.ColumnTypes;

public class IntegerColumn : Column
{
    public IntegerColumn(string name) : base(name) { }

    public override bool Validate(string value)
    {
        return int.TryParse(value, out int _);
    }
}