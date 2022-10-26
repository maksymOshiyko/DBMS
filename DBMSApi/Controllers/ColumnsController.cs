using DBMSApi.Models.Hateoas;
using DBMSCore.Dtos;
using DBMSCore.Interfaces;
using DBMSCore.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DBMSApi.Controllers;

public class ColumnsController : ApiControllerBase
{
    private readonly IDbms _dbms;

    public ColumnsController(IDbms dbms)
    {
        _dbms = dbms;
    }

    /// <summary>
    /// Add column to table
    /// </summary>
    /// <param name="tableIndex">Table index</param>
    /// <param name="name">Column name</param>
    /// <param name="type">Column type(CHAR, INTEGER, PNG, REAL, REAL_INTERVAL, STRING)</param>
    /// <response code="200">Returns added column</response>
    /// <response code="400">Bad request.
    ///     Database is not created
    ///     OR Column name already exists
    ///     OR Column should have name and type
    ///     OR Invalid column type
    /// </response>
    [HttpPost("api/tables/{tableIndex}/columns", Name = nameof(AddColumn))]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Column))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public ActionResult<HateoasResponse<ColumnDto>> AddColumn(int tableIndex, string name, string type)
    {
        var column = _dbms.AddColumn(tableIndex, name, type);
        return new HateoasResponse<ColumnDto>
        {
           Data = column,
           Links = new Link[]
           {
               new() { Method = "POST", Rel = "self", Href = Url.Link(nameof(AddColumn),
                   new { tableIndex }) },
               new() { Method = "DELETE", Rel = "delete_column", Href = Url.Link(nameof(DeleteColumn), 
                   new { tableIndex, columnIndex = column.Index }) },
           }
        };
    }

    /// <summary>
    /// Add column to table
    /// </summary>
    /// <param name="tableIndex">Table index</param>
    /// <param name="columnIndex">Column index</param>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request. Database is not created</response>
    /// <response code="404">Not found. Column is not found</response>
    [HttpDelete("api/tables/{tableIndex}/columns/{columnIndex}", Name = nameof(DeleteColumn))]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public ActionResult<HateoasResponse> DeleteColumn(int tableIndex, int columnIndex)
    {
        _dbms.DeleteColumn(tableIndex, columnIndex);
        return new HateoasResponse
        {
            Links = new Link[]
            {
                new() { Method = "POST", Rel = "add_column", Href = Url.Link(nameof(AddColumn),
                    new { tableIndex }) },
                new() { Method = "DELETE", Rel = "self", Href = Url.Link(nameof(DeleteColumn), 
                    new { tableIndex, columnIndex }) },
            }
        };
    }
}