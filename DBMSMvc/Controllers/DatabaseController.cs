using DBMSCore.Interfaces;
using DBMSMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBMSMvc.Controllers;

public class DatabaseController : BaseController
{
    private readonly IDbms _dbms;

    public DatabaseController(IDbms dbms)
    {
        _dbms = dbms;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View(_dbms.GetDatabase());
    }
    
    [HttpGet]
    public IActionResult CreateDatabase()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult CreateDatabase(string name)
    {
        _dbms.CreateDatabase(name);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult SaveDatabase()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult SaveDatabase(string path)
    {
        _dbms.SaveDatabase(path);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult UploadDatabase()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult UploadDatabase(string path)
    {
        _dbms.UploadDatabase(path);
        return RedirectToAction("Index");
    }
}