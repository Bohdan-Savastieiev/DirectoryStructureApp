using DirectoryStructureApp.DAL.Models;

namespace DirectoryStructureApp.DAL.Interfaces;

public interface IDirectoryRepository
{
    Task<IEnumerable<DirectoryEntity>> GetRootDirectoriesAsync();
    Task<IEnumerable<DirectoryEntity>> GetRootDirectoriesWithAllSubsAsync();
    Task<DirectoryEntity?> GetDirectoryByIdAsync(int id);
    Task<DirectoryEntity?> GetDirectoryByIdWithAllSubsAsync(int id);
    Task AddDirectoryAsync(DirectoryEntity directory);
    Task DeleteDirectoryAsync(int id);
    Task SaveChangesAsync();
}