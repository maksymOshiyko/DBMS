using DBMSCore.Exceptions;
using DBMSCore.Models;
using DBMSCore.Models.Columns;

namespace DBMSCore.Services;

public static class ColumnFactory
{
    public static Column GetColumnInstance(string name, string type)
    {
        string normalizedType = type.ToUpper();
        
        return normalizedType switch
        {
            "CHAR" => new CharColumn(name),
            "INTEGER" => new IntegerColumn(name),
            "PNG" => new PngColumn(name),
            "REAL" => new RealColumn(name),
            "REAL_INTERVAL" => new RealIntervalColumn(name),
            "STRING" => new StringColumn(name),
            _ => throw new BadRequestException("Invalid column type")
        };
    }
}