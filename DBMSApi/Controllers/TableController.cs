using DBMSCore.Interfaces;
using DBMSCore.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DBMSApi.Controllers;

public class TableController : ApiControllerBase
{
    private readonly IDbms _dbms;

    public TableController(IDbms dbms)
    {
        _dbms = dbms;
    }

    /// <summary>
    /// Add table to database
    /// </summary>
    /// <param name="name">Name of table</param>
    /// <response code="200">Returns added table</response>
    /// <response code="400">Bad request.
    ///     Database is not created
    ///     OR Table name already exists
    /// </response>
    [HttpPost("add")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Table))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public ActionResult<Table> AddTable(string name)
    {
        return _dbms.AddTable(name);
    }

    /// <summary>
    /// Delete table from database
    /// </summary>
    /// <param name="tableIndex">Table index</param>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request.
    ///     Database is not created
    ///     OR Something went wrong when uploading database
    /// </response>
    /// <response code="404">Not found. Table is not found</response> 
    [HttpDelete("delete")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public ActionResult DeleteTable(int tableIndex)
    {
        _dbms.DeleteTable(tableIndex);
        return Ok();
    }

    /// <summary>
    /// Sort table by specified column 
    /// </summary>
    /// <param name="tableIndex">Table index</param>
    /// <param name="sortColumnIndex">Sorting column index</param>
    /// <response code="200">Returns sorted table</response>
    /// <response code="400">Bad request.Database is not created</response>
    /// <response code="404">Not found. Column is not found</response> 
    [HttpGet("sort")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Table))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public ActionResult<Table> SortByColumn(int tableIndex, int sortColumnIndex)
    {
        return _dbms.SortByColumn(tableIndex, sortColumnIndex);
    }
}