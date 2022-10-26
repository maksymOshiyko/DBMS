using DBMSApi.Models.Hateoas;
using DBMSCore.Dtos;
using DBMSCore.Interfaces;
using DBMSCore.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DBMSApi.Controllers;

public class RowsController : ApiControllerBase
{
    private readonly IDbms _dbms;

    public RowsController(IDbms dbms)
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
    [HttpPost("api/tables/{tableIndex}/rows", Name = nameof(AddRow))]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Row))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public ActionResult<HateoasResponse<RowDto>> AddRow(int tableIndex, List<string> values)
    {
        var row = _dbms.AddRow(tableIndex, values);
        return new HateoasResponse<RowDto>
        {
            Data = row, 
            Links = new Link[]
            {
                new() { Method = "POST", Rel = "self", Href = Url.Link(nameof(AddRow), 
                    new { tableIndex }) },
                new() { Method = "DELETE", Rel = "delete_row", Href = Url.Link(nameof(DeleteRow), 
                    new { tableIndex, rowIndex = row.Index }) },
                new() { Method = "PUT", Rel = "edit_row", Href = Url.Link(nameof(EditRow), 
                    new { tableIndex, rowIndex = row.Index }) },
            }
        };
    }

    /// <summary>
    /// Add row to table
    /// </summary>
    /// <param name="tableIndex">Table index</param>
    /// <param name="rowIndex">Row index</param>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request.Database is not created</response>
    /// <response code="404">Not found. Row is not found</response>
    [HttpDelete("api/tables/{tableIndex}/rows/{rowIndex}", Name = nameof(DeleteRow))]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public ActionResult<HateoasResponse> DeleteRow(int tableIndex, int rowIndex)
    {
        _dbms.DeleteRow(tableIndex, rowIndex);
        return new HateoasResponse
        {
            Links = new Link[]
            {
                new() { Method = "POST", Rel = "add_row", Href = Url.Link(nameof(AddRow), 
                    new { tableIndex }) },
                new() { Method = "DELETE", Rel = "self", Href = Url.Link(nameof(DeleteRow), 
                    new { tableIndex, rowIndex }) }
            }
        };
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
    [HttpPut("api/tables/{tableIndex}/rows/{rowIndex}", Name = nameof(EditRow))]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Row))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public ActionResult<HateoasResponse<RowDto>> EditRow(int tableIndex, int rowIndex, List<string> values)
    {
        var row = _dbms.EditRow(tableIndex, rowIndex, values);
        return new HateoasResponse<RowDto>
        {
            Data = row, 
            Links = new Link[]
            {
                new() { Method = "PUT", Rel = "self", Href = Url.Link(nameof(EditRow), 
                    new { tableIndex, rowIndex = row.Index }) },
                new() { Method = "DELETE", Rel = "delete_row", Href = Url.Link(nameof(DeleteRow), 
                    new { tableIndex, rowIndex = row.Index }) }
            }
        };
    }

    /// <summary>
    /// Download png
    /// </summary>
    /// <param name="tableIndex">Table index</param>
    /// <param name="columnIndex">Column index</param>
    /// <param name="rowIndex">Row index</param>
    /// <response code="200">Returns file</response>
    /// <response code="400">Bad request.
    ///     Database is not created
    ///     OR Row value is empty
    ///     OR Column type is not PNG
    /// </response>
    /// <response code="404">Not found.
    ///     Row is not found
    ///     OR Table is not found
    ///     OR Column is not found
    /// </response>
    [HttpGet("api/tables/{tableIndex}/columns/{columnIndex}/rows/{rowIndex}/download-png", Name = nameof(DownloadPng))]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(FileResult))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public FileResult DownloadPng(int tableIndex, int columnIndex, int rowIndex)
    {
        return File(_dbms.DownloadPng(tableIndex, columnIndex, rowIndex), "image/png", Guid.NewGuid().ToString());
    }
}