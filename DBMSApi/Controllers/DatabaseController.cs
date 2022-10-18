using DBMSCore.Interfaces;
using DBMSCore.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DBMSApi.Controllers;

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
    [HttpPost("create")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Database))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public ActionResult<Database> CreateDatabase(string name)
    {
        return _dbms.CreateDatabase(name);
    }

    /// <summary>
    /// Get database
    /// </summary>
    /// <response code="200">Returns a database</response>
    /// <response code="400">Bad request. Database is not created</response>
    [HttpGet("get")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Database))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public ActionResult<Database> GetDatabase()
    {
        return _dbms.GetDatabase();
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
    [HttpPost("save")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public ActionResult SaveDatabase(string path)
    {
        _dbms.SaveDatabase(path);
        return Ok();
    }

    /// <summary>
    /// Upload database from file
    /// </summary>
    /// <param name="path">Path to file with database</param>
    /// <response code="200">Returns uploaded database</response>
    /// <response code="400">Bad request. Something went wrong when uploading database</response>
    [HttpPost("upload")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public ActionResult<Database> UploadDatabase(string path)
    {
        _dbms.UploadDatabase(path);
        return Ok();
    }
}