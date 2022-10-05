namespace DBMSCore.Models.ColumnTypes;

public class PictureColumn : Column
{
    public PictureColumn(string name) : base(name) { }

    public override bool Validate(string value)
    {
        throw new NotImplementedException();
    }
}