using DBMSApi.Models.Hateoas;
using DBMSCore.Interfaces;
using DBMSCore.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DBMSApi.Controllers;

[Route("api/database")]
public class DatabaseController : ApiControllerBase
{
    private readonly IDbms _dbms;

    public DatabaseController(IDbms dbms)
    {
        _dbms = dbms;
    }

    /// <summary>
    /// Create database
    /// </summary>
    /// <param name="name">Name of database</param>
    /// <response code="200">Returns a database</response>
    /// <response code="400">Bad request. Database should have name</response>
    [HttpPost(Name = nameof(CreateDatabase))]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Database))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public ActionResult<HateoasResponse<Database>> CreateDatabase(string name)
    {
        var database = _dbms.CreateDatabase(name);
        return new HateoasResponse<Database>
        {
            Data = database,
            Links = new []
            {
                new Link { Method = "POST", Rel = "self", Href = Url.Link(nameof(CreateDatabase), null) },
                new Link { Method = "GET", Rel = "get_database", Href = Url.Link(nameof(GetDatabase), null) },
                new Link { Method = "POST", Rel = "save_database", Href = Url.Link(nameof(SaveDatabase), null) },
                new Link { Method = "POST", Rel = "upload_database", Href = Url.Link(nameof(UploadDatabase), null) },
                new Link { Method = "POST", Rel = "create_table", Href = Url.ActionLink("AddTable", "Tables") }
            }
        };
    }

    /// <summary>
    /// Get database
    /// </summary>
    /// <response code="200">Returns a database</response>
    /// <response code="400">Bad request. Database is not created</response>
    [HttpGet(Name = nameof(GetDatabase))]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Database))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public ActionResult<HateoasResponse<Database>> GetDatabase()
    {
        var database = _dbms.GetDatabase();
        return new HateoasResponse<Database>
        {
            Data = database,
            Links = new []
            {
                new Link { Method = "POST", Rel = "create_database", Href = Url.Link(nameof(CreateDatabase), null) },
                new Link { Method = "GET", Rel = "self", Href = Url.Link(nameof(GetDatabase), null) },
                new Link { Method = "POST", Rel = "save_database", Href = Url.Link(nameof(SaveDatabase), null) },
                new Link { Method = "POST", Rel = "upload_database", Href = Url.Link(nameof(UploadDatabase), null) },
                new Link { Method = "POST", Rel = "create_table", Href = Url.ActionLink("AddTable", "Tables") }
            }
        };
    }

    /// <summary>
    /// Save database to a specific path
    /// </summary>
    /// <param name="path">Path to file where database should be saved</param>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request.
    ///     Database is not created
    ///     OR Something went wrong when saving database
    /// </response>
    [HttpPost("save", Name = nameof(SaveDatabase))]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public ActionResult<HateoasResponse> SaveDatabase(string path)
    {
        _dbms.SaveDatabase(path);
        return new HateoasResponse
        {
            Links = new []
            {
                new Link { Method = "POST", Rel = "create_database", Href = Url.Link(nameof(CreateDatabase), null) },
                new Link { Method = "GET", Rel = "get_database", Href = Url.Link(nameof(GetDatabase), null) },
                new Link { Method = "POST", Rel = "self", Href = Url.Link(nameof(SaveDatabase), null) },
                new Link { Method = "POST", Rel = "upload_database", Href = Url.Link(nameof(UploadDatabase), null) }
            }
        };
    }

    /// <summary>
    /// Upload database from file
    /// </summary>
    /// <param name="path">Path to file with database</param>
    /// <response code="200">Returns uploaded database</response>
    /// <response code="400">Bad request. Something went wrong when uploading database</response>
    [HttpPost("upload", Name = nameof(UploadDatabase))]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public ActionResult<HateoasResponse<Database>> UploadDatabase(string path)
    {
        var database = _dbms.UploadDatabase(path);
        return new HateoasResponse<Database>
        {
            Data = database,
            Links = new []
            {
                new Link { Method = "POST", Rel = "create_database", Href = Url.Link(nameof(CreateDatabase), null) },
                new Link { Method = "GET", Rel = "get_database", Href = Url.Link(nameof(GetDatabase), null) },
                new Link { Method = "POST", Rel = "save_database", Href = Url.Link(nameof(SaveDatabase), null) },
                new Link { Method = "POST", Rel = "self", Href = Url.Link(nameof(UploadDatabase), null) },
                new Link { Method = "POST", Rel = "create_table", Href = Url.ActionLink("AddTable", "Tables") }
            }
        };
    }
}