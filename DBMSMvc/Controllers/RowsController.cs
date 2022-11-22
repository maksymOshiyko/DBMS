using System.Text;
using DBMSCore.Dtos;
using DBMSCore.Interfaces;
using DBMSMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBMSMvc.Controllers;

public class RowsController : BaseController
{
    private readonly IDbms _dbms;

    public RowsController(IDbms dbms)
    {
        _dbms = dbms;
    }

    [HttpGet]
    public IActionResult AddRow(int tableIndex)
    {
        var table = _dbms.GetTable(tableIndex);
        return View(table);
    }
    
    [HttpPost]
    public IActionResult AddRow([FromBody] AddRowDto dto)
    {
        _dbms.AddRow(dto.TableIndex, dto.Values);
        return RedirectToAction("Index", "Tables", new {tableIndex = dto.TableIndex});
    }

    [HttpGet]
    public IActionResult DeleteRow(int tableIndex, int rowIndex)
    {
        _dbms.DeleteRow(tableIndex, rowIndex);
        return RedirectToAction("Index", "Tables", new {tableIndex});
    }
    
    [HttpGet]
    public IActionResult EditRow(int tableIndex, int rowIndex)
    {
        var table = _dbms.GetTable(tableIndex);
        EditRowViewModel model = new()
        {
            Table = table,
            RowIndex = rowIndex
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult EditRow([FromBody] EditRowDto dto)
    {
        _dbms.EditRow(dto.TableIndex, dto.RowIndex, dto.Values);
        return RedirectToAction("Index", "Tables", new {tableIndex = dto.TableIndex});
    }

    [HttpGet]
    public IActionResult Png(int tableIndex, int columnIndex, int rowIndex)
    {
        var bytes = _dbms.DownloadPng(tableIndex, columnIndex, rowIndex);
        string base64 = Convert.ToBase64String(bytes);
        return View("Png", base64);
    }
}