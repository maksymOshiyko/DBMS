using DBMSApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DBMSApi.Controllers;

[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public class ApiControllerBase : ControllerBase
{
}