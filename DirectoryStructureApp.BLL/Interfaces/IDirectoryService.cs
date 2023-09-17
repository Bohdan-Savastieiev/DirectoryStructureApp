using DirectoryStructureApp.BLL.DTOs;

namespace DirectoryStructureApp.BLL.Interfaces;

public interface IDirectoryService
{
    Task<IEnumerable<DirectoryDto>> GetDirectoriesWithoutParentAsync();
    Task<IEnumerable<DirectoryDto>> GetSubDirectoriesAsync(int parentDirectoryId);
    Task<DirectoryDto?> GetDirectoryByIdAsync(int id);
    Task AddDirectoryAsync(DirectoryDto directoryDto);
}
