using DirectoryStructureApp.BLL.DTOs;

namespace DirectoryStructureApp.BLL.Interfaces;

public interface IFileSystemService
{
    List<DirectoryDto> ImportDirectoriesFromPath(string path);
    List<DirectoryDto> ImportDirectoriesFromFile(string filePath);
    void ExportDirectoriesToFile(IEnumerable<DirectoryDto> directories);
}
