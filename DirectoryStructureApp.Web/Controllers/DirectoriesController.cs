using DirectoryStructureApp.BLL.Interfaces;
using DirectoryStructureApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DirectoryStructureApp.Web.Controllers;

public class DirectoriesController : Controller
{
    private readonly IDirectoryService _directoryService;

    public DirectoriesController(IDirectoryService directoryService)
    {
        _directoryService = directoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var directories = await _directoryService.GetDirectoriesWithoutParentAsync();
        return View(directories);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}