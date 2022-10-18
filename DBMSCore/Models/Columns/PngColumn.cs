namespace DBMSCore.Models.Columns;

public class PngColumn : Column
{
    public PngColumn(string name) : base(name, "PNG")
    {
    }

    public override bool Validate(string value)
    {
        return value.EndsWith(".png");
    }
}