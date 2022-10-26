using DBMSApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DBMSApi.Controllers;

[ApiController]
[ApiExceptionFilter]
public class ApiControllerBase : ControllerBase
{
}