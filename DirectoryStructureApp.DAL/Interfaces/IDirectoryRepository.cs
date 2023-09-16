using DirectoryStructureApp.DAL.Models;

namespace DirectoryStructureApp.DAL.Interfaces;

public interface IDirectoryRepository
{
    Task<IEnumerable<DirectoryEntity>> GetAllDirectoriesAsync();
    Task<IEnumerable<DirectoryEntity>> GetDirectoriesWithoutParentAsync();
    Task<DirectoryEntity?> GetDirectoryByIdAsync(int id);
    Task InsertDirectoryAsync(DirectoryEntity directory);
    void UpdateDirectory(DirectoryEntity directory);
    Task DeleteDirectoryAsync(int id);
    Task SaveChangesAsync();
}