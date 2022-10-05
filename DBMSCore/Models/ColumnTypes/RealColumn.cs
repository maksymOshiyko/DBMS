namespace DBMSCore.Models.ColumnTypes;

public class RealColumn : Column
{
    public RealColumn(string name) : base(name) { }

    public override bool Validate(string value)
    {
        return double.TryParse(value, out double _);
    }
}