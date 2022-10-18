namespace DBMSCore.Models.Columns;

public class RealColumn : Column
{
    public RealColumn(string name) : base(name, "REAL") { }

    public override bool Validate(string value)
    {
        return double.TryParse(value, out double _);
    }
}