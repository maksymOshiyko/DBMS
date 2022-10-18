namespace DBMSCore.Models.Columns;

public class StringColumn : Column
{
    public StringColumn(string name) : base(name, "STRING") { }

    public override bool Validate(string value)
    {
        return true;
    }
}