using DirectoryStructureApp.BLL.DTOs;
using Microsoft.AspNetCore.Http;

namespace DirectoryStructureApp.BLL.Interfaces;

public interface IFileSystemService
{
    List<DirectoryDto> ImportDirectoriesFromPath(string path);
    Task<List<DirectoryDto>> ImportDirectoriesFromFileAsync(IFormFile file);
    void ExportDirectoriesToFile(IEnumerable<DirectoryDto> directories);
}
