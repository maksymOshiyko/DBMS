using DBMSCore.Interfaces;
using DBMSCore.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DBMSApi.Controllers;

public class RowController : ApiControllerBase
{
    private readonly IDbms _dbms;

    public RowController(IDbms dbms)
    {
        _dbms = dbms;
    }

    /// <summary>
    /// Add row to table
    /// </summary>
    /// <param name="tableIndex">Table index</param>
    /// <param name="values">Row values:
    ///     CHAR - a; INTEGER - 1; PNG - path to file; REAL - 0,1; REAL_INTERVAL - 0,1_0,2; STRING - example
    /// </param>
    /// <response code="200">Returns added row</response>
    /// <response code="400">Bad request.
    ///     Database is not created
    ///     OR Row should have at least 1 value
    ///     OR Row length should not be more than table`s column length
    ///     OR "value" is not assignable to "column_type" type
    /// </response>
    [HttpPost("add")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Row))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public ActionResult<Row> AddRow(int tableIndex, List<string> values)
    {
        return _dbms.AddRow(tableIndex, values);
    }

    /// <summary>
    /// Add row to table
    /// </summary>
    /// <param name="tableIndex">Table index</param>
    /// <param name="rowIndex">Row index</param>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request.Database is not created</response>
    /// <response code="404">Not found. Row is not found</response>
    [HttpDelete("delete")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public ActionResult DeleteRow(int tableIndex, int rowIndex)
    {
        _dbms.DeleteRow(tableIndex, rowIndex);
        return Ok();
    }

    /// <summary>
    /// Edit table row
    /// </summary>
    /// <param name="tableIndex">Table index</param>
    /// <param name="rowIndex">Row index</param>
    /// <param name="values">Row values:
    ///     CHAR - a; INTEGER - 1; PNG - path to file; REAL - 0,1; REAL_INTERVAL - 0,1_0,2; STRING - example
    /// </param>
    /// <response code="200">Returns edited row</response>
    /// <response code="400">Bad request.
    ///     Database is not created
    ///     OR "value" is not assignable to "column_type" type
    /// </response>
    /// <response code="404">Not found. Row is not found</response>
    [HttpPut("edit")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Row))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public ActionResult<Row> EditRow(int tableIndex, int rowIndex, List<string> values)
    {
        return _dbms.EditRow(tableIndex, rowIndex, values);
    }

    [HttpGet("download-png")]
    public FileResult DownloadPng(int tableIndex, int columnIndex, int rowIndex)
    {
        return File(_dbms.DownloadPng(tableIndex, columnIndex, rowIndex), "image/png", Guid.NewGuid().ToString());
    }
}