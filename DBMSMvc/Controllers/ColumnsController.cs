using DBMSCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DBMSMvc.Controllers;

public class ColumnsController : BaseController
{
    private readonly IDbms _dbms;

    public ColumnsController(IDbms dbms)
    {
        _dbms = dbms;
    }
    
    [HttpGet]
    public IActionResult AddColumn(int tableIndex)
    {
        var table = _dbms.GetTable(tableIndex);
        return View(table);
    }

    [HttpPost]
    public IActionResult AddColumn(int tableIndex, string name, string type)
    {
        _dbms.AddColumn(tableIndex, name, type);
        return RedirectToAction("Index", "Tables", new {tableIndex});
    }

    [HttpGet]
    public IActionResult DeleteColumn(int tableIndex, int columnIndex)
    {
        _dbms.DeleteColumn(tableIndex, columnIndex);
        return RedirectToAction("Index", "Tables", new {tableIndex});
    }
}