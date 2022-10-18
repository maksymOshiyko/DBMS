namespace DBMSCore.Models.Columns;

public class IntegerColumn : Column
{
    public IntegerColumn(string name) : base(name, "INTEGER") { }

    public override bool Validate(string value)
    {
        return int.TryParse(value, out int _);
    }
}