namespace DBMSCore.Models.ColumnTypes;

public class CharColumn : Column
{
    public CharColumn(string name) : base(name) { }

    public override bool Validate(string value)
    {
        return char.TryParse(value, out char _);
    }
}