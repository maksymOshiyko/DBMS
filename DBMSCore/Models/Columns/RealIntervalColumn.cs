namespace DBMSCore.Models.Columns;

public class RealIntervalColumn : Column
{
    private readonly char _delimiter = '_';
    
    public RealIntervalColumn(string name) : base(name, "REAL_INTERVAL") { }

    public override bool Validate(string value)
    {
        if (!value.Contains(_delimiter)) return false;
        
        var split = value.Split(_delimiter);
        return double.TryParse(split[0], out double lhs) && double.TryParse(split[1], out double rhs)
            && lhs <= rhs;
    }
}