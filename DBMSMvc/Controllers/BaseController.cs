using DBMSMvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DBMSMvc.Controllers;

[GlobalExceptionFilter]
public class BaseController : Controller
{
}