using DBMSMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBMSMvc.Controllers;

public class HomeController : BaseController
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Error(string message)
    {
        var model = new ErrorViewModel() {Error = message};
        return View(model);
    }
}