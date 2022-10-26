using DBMSApi.Models.Hateoas;
using DBMSCore.Dtos;
using DBMSCore.Interfaces;
using DBMSCore.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DBMSApi.Controllers;

[Route("api/tables")]
public class TablesController : ApiControllerBase
{
    private readonly IDbms _dbms;

    public TablesController(IDbms dbms)
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
    [HttpPost(Name = nameof(AddTable))]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Table))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public ActionResult<HateoasResponse<TableDto>> AddTable(string name)
    {
        var table = _dbms.AddTable(name);
        var sortTableLinks = GetSortTableLinks(table);
        var links = new List<Link>
        {
            new() { Method = "POST", Rel = "self", Href = Url.Link(nameof(AddTable), null) },
            new() { Method = "DELETE", Rel = "delete_table", Href = Url.Link(nameof(DeleteTable), 
                new {tableIndex = table.Index}) },
            new() { Method = "GET", Rel = "get_table", Href = Url.Link(nameof(GetTable), 
                new {tableIndex = table.Index}) },
        };
        links.AddRange(sortTableLinks);
        
        return new HateoasResponse<TableDto>
        {
            Data = table,
            Links = links
        };
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
    [HttpDelete("{tableIndex}", Name = nameof(DeleteTable))]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public ActionResult<HateoasResponse> DeleteTable(int tableIndex)
    {
        _dbms.DeleteTable(tableIndex);
        return new HateoasResponse
        {
            Links = new Link[]
            {
                new() { Method = "DELETE", Rel = "self", Href = Url.Link(nameof(DeleteTable), 
                    new { tableIndex } ) },
                new() { Method = "POST", Rel = "create_table", Href = Url.Link(nameof(AddTable), null) },
            }
        };
    }

    /// <summary>
    /// Sort table by specified column 
    /// </summary>
    /// <param name="tableIndex">Table index</param>
    /// <param name="sortColumnIndex">Sorting column index</param>
    /// <response code="200">Returns sorted table</response>
    /// <response code="400">Bad request.Database is not created</response>
    /// <response code="404">Not found. Column is not found</response> 
    [HttpGet("sort/{tableIndex}/column/{sortColumnIndex}", Name = nameof(SortByColumn))]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Table))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public ActionResult<HateoasResponse<TableDto>> SortByColumn(int tableIndex, int sortColumnIndex)
    {
        var table = _dbms.SortByColumn(tableIndex, sortColumnIndex);
        var sortTableLinks = GetSortTableLinks(table, sortColumnIndex);
        var links = new List<Link>
        {
            new() { Method = "DELETE", Rel = "delete_table", Href = Url.Link(nameof(DeleteTable), 
                new {tableIndex = table.Index}) },
            new() { Method = "GET", Rel = "get_table", Href = Url.Link(nameof(GetTable), 
                new {tableIndex = table.Index}) }
        };
        links.AddRange(sortTableLinks);
        
        return new HateoasResponse<TableDto>
        {
            Data = table,
            Links = links
        };
    }
    
    
    /// <summary>
    /// Get table from database
    /// </summary>
    /// <param name="tableIndex">Table index</param>
    /// <response code="200">Returns table</response>
    /// <response code="400">Bad request.Database is not created</response>
    /// <response code="404">Not found. Table is not found</response> 
    [HttpGet("{tableIndex}", Name = nameof(GetTable))]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Table))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public ActionResult<HateoasResponse<TableDto>> GetTable(int tableIndex)
    {
        var table = _dbms.GetTable(tableIndex);
        var sortTableLinks = GetSortTableLinks(table);
        var links = new List<Link>
        {
            new() { Method = "GET", Rel = "self", Href = Url.Link(nameof(GetTable), 
                new {tableIndex = table.Index}) },
            new() { Method = "DELETE", Rel = "delete_table", Href = Url.Link(nameof(DeleteTable), 
                new {tableIndex = table.Index}) },
        };
        links.AddRange(sortTableLinks);
        
        return new HateoasResponse<TableDto>
        {
            Data = table,
            Links = links
        };
    }

    private List<Link> GetSortTableLinks(TableDto table, int? sortColumnIndex = null)
    {
        var sortTableLinks = new List<Link>();
        foreach (var column in table.Columns)
        {
            string rel = "sort_table";

            if (sortColumnIndex.HasValue && sortColumnIndex.Value == column.Index)
            {
                rel = "self";
            }

            sortTableLinks.Add(new Link
            {
                Method = "GET",
                Rel = rel,
                Href = Url.Link(nameof(SortByColumn), new { tableIndex = table.Index,
                    sortColumnIndex = column.Index }) 
            });
        }

        return sortTableLinks;
    }
}