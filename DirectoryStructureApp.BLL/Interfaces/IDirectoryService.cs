using DirectoryStructureApp.BLL.DTOs;

namespace DirectoryStructureApp.BLL.Interfaces;

public interface IDirectoryService
{
    Task<IEnumerable<DirectoryDto>> GetRootDirectoriesAsync();
    Task<IEnumerable<DirectoryDto>> GetRootDirectoriesWithAllSubsAsync();
    Task<DirectoryDto?> GetDirectoryByIdAsync(int id);
    Task<DirectoryDto?> GetDirectoryByIdWithAllSubsAsync(int id);
    Task AddDirectoryAsync(DirectoryDto directoryDto);
    Task AddDirectoriesAsync(List<DirectoryDto> directories);
    Task DeleteDirectoryAsync(int id);
}
