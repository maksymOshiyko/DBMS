using DBMSCore.Interfaces;
using DBMSCore.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DBMSApi.Controllers;

public class ColumnController : ApiControllerBase
{
    private readonly IDbms _dbms;

    public ColumnController(IDbms dbms)
    {
        _dbms = dbms;
    }

    /// <summary>
    /// Add column to table
    /// </summary>
    /// <param name="tableIndex">Table index</param>
    /// <param name="name">Column name</param>
    /// <param name="type">Column type</param>
    /// <response code="200">Returns added column</response>
    /// <response code="400">Bad request.
    ///     Database is not created
    ///     OR Column name already exists
    ///     OR Column should have name and type
    ///     OR Invalid column type
    /// </response>
    [HttpPost("add")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Column))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public ActionResult<Column> AddColumn(int tableIndex, string name, string type)
    {
        return _dbms.AddColumn(tableIndex, name, type);
    }

    /// <summary>
    /// Add column to table
    /// </summary>
    /// <param name="tableIndex">Table index</param>
    /// <param name="columnIndex">Column index</param>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request. Database is not created</response>
    /// <response code="404">Not found. Column is not found</response>
    [HttpDelete("delete")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public ActionResult DeleteColumn(int tableIndex, int columnIndex)
    {
        _dbms.DeleteColumn(tableIndex, columnIndex);
        return Ok();
    }
}