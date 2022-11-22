using DBMSCore.Dtos;
using DBMSCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DBMSMvc.Controllers;

public class TablesController : BaseController
{
    private readonly IDbms _dbms;

    public TablesController(IDbms dbms)
    {
        _dbms = dbms;
    }

    [HttpGet]
    public IActionResult Index(int tableIndex)
    {
        var table = _dbms.GetTable(tableIndex);
        return View(table);
    }

    [HttpGet]
    public IActionResult AddTable()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult AddTable(string name)
    {
        var table = _dbms.AddTable(name);
        return RedirectToAction("Index", new { tableIndex = table.Index });
    }

    [HttpGet]
    public IActionResult DeleteTable(int tableIndex)
    {
        _dbms.DeleteTable(tableIndex);
        return RedirectToAction("Index", "Database");
    }

    [HttpGet]
    public IActionResult SortByColumn(int tableIndex, int? sortColumnIndex)
    {
        return Json(sortColumnIndex.HasValue
            ? _dbms.SortByColumn(tableIndex, sortColumnIndex.Value) 
            : _dbms.GetTable(tableIndex));
    }
}