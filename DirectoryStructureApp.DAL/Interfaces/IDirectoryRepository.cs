using DirectoryStructureApp.DAL.Models;

namespace DirectoryStructureApp.DAL.Interfaces;

public interface IDirectoryRepository
{
    Task<IEnumerable<DirectoryEntity>> GetAllDirectoriesAsync();
    Task<IEnumerable<DirectoryEntity>> GetDirectoriesWithoutParentAsync();
    Task<IEnumerable<DirectoryEntity>> GetSubDirectoriesAsync(int parentDirectoryId);
    Task<DirectoryEntity?> GetDirectoryByIdAsync(int id);
    Task AddDirectoryAsync(DirectoryEntity directory);
    void UpdateDirectory(DirectoryEntity directory);
    Task DeleteDirectoryAsync(int id);
    Task SaveChangesAsync();
}