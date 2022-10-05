namespace DBMSCore.Models.ColumnTypes;

public class StringColumn : Column
{
    public StringColumn(string name) : base(name) { }

    public override bool Validate(string value)
    {
        return true;
    }
}