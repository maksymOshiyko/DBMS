namespace DBMSCore.Models.Columns;

public class CharColumn : Column
{
    public CharColumn(string name) : base(name, "CHAR") { }

    public override bool Validate(string value)
    {
        return char.TryParse(value, out char _);
    }
}