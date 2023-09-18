using DirectoryStructureApp.BLL.DTOs;
using DirectoryStructureApp.BLL.Interfaces;
using DirectoryStructureApp.Web.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DirectoryStructureApp.Web.Controllers;

public class DirectoriesController : Controller
{
    private readonly IDirectoryService _directoryService;
    private readonly IFileSystemService _fileSystemImportService;
    public DirectoriesController(
        IDirectoryService directoryService, 
        IFileSystemService fileSystemImportService)
    {
        _directoryService = directoryService;
        _fileSystemImportService = fileSystemImportService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> View(int? directoryId)
    {
        var directories = new List<DirectoryDto>();
        if (directoryId == null)
        {
            var rootDirectories = await _directoryService.GetRootDirectoriesAsync();
            directories.AddRange(rootDirectories);
        }
        else
        {
            var directory = await _directoryService.GetDirectoryByIdAsync((int)directoryId);
            if (directory != null)
            {
                directories.Add(directory);
            }
            else
            {
                return RedirectToAction("View");
            }
        }

        return View(directories);
    }

    [HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ConfirmDelete(int directoryId)
    {
        var directory = await _directoryService.GetDirectoryByIdAsync(directoryId);
        if (directory == null)
        {
            return RedirectToAction("View");
        }

        return View(directory);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int directoryId)
    {
        var directoryToDelete = await _directoryService.GetDirectoryByIdAsync(directoryId);
        if (directoryToDelete == null)
        {
            return View();
        }

        var parentId = directoryToDelete.ParentDirectoryId;
        await _directoryService.DeleteDirectoryAsync(directoryId);
        if (parentId != null)
        {
            return RedirectToAction("View", new { directoryId = parentId });
        }

        return RedirectToAction("View");
    }

    [HttpGet]
    public IActionResult Import()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ImportPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return View();
        }

        try
        {
            var importDirectories = _fileSystemImportService.ImportDirectoriesFromPath(path);
            await _directoryService.AddDirectoriesAsync(importDirectories);
            return RedirectToAction("Index");
        }
        catch (DirectoryNotFoundException)
        {
            ModelState.AddModelError("", "Directory has not been found by the path provided or the path provided is not valid.");
            return View("Import");
        }
    }

    [HttpPost]
    public async Task<IActionResult> ImportFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return Content("File not selected");

        try
        {
            var importDirectories = await _fileSystemImportService.ImportDirectoriesFromFileAsync(file);
            await _directoryService.AddDirectoriesAsync(importDirectories);
            return RedirectToAction("Index");
        }
        catch(JsonException ex)
        {
            ModelState.AddModelError("", "Mistake in file deserialization: " + ex.Message);
            return View("Import");
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError("", "Mistake in file deserialization: " + ex.Message);
            return View("Import");
        }
    }


    [HttpPost]
    public async Task<IActionResult> ExportFile(int? directoryId)
    {
        var directories = new List<DirectoryDto>();
        if (directoryId == null)
        {
            var rootDirectories = await _directoryService.GetRootDirectoriesWithAllSubsAsync();
            directories.AddRange(rootDirectories);
        }
        else
        {
            var directory = await _directoryService.GetDirectoryByIdWithAllSubsAsync((int)directoryId);
            if (directory == null)
            {
                return RedirectToAction("Error");
            }
            directories.Add(directory);
        }

        _fileSystemImportService.ExportDirectoriesToFile(directories);

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}