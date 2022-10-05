namespace DBMSCore.Models.ColumnTypes;

public class RealIntervalColumn : Column
{
    private readonly char _dilimiter = '_';
    
    public RealIntervalColumn(string name) : base(name) { }

    public override bool Validate(string value)
    {
        if (!value.Contains(_dilimiter)) return false;
        
        var split = value.Split(_dilimiter);
        return double.TryParse(split[0], out double _) && double.TryParse(split[1], out double _);
    }
}