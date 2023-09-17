using DirectoryStructureApp.BLL.DTOs;
using DirectoryStructureApp.BLL.Interfaces;
using DirectoryStructureApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DirectoryStructureApp.Web.Controllers;

public class DirectoryViewController : Controller
{
    private readonly IDirectoryService _directoryService;

    public DirectoryViewController(IDirectoryService directoryService)
    {
        _directoryService = directoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int? directoryId)
    {
        var directories = new List<DirectoryDto>();
        if (directoryId == null)
        {
            var rootDirectories = await _directoryService.GetDirectoriesWithoutParentAsync();
            directories.AddRange(rootDirectories);
        }
        else
        {
            var directory = await _directoryService.GetDirectoryByIdAsync((int)directoryId);
            if (directory != null)
            {
                directories.Add(directory);
            }
        }

        return View(directories);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}