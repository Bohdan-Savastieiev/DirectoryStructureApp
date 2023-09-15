using DirectoryStructureApp.DAL.Models;

namespace DirectoryStructureApp.DAL.Interfaces;

public interface IDirectoryRepository
{
    Task<IEnumerable<AppDirectory>> GetAllDirectoriesAsync();
    Task<AppDirectory> GetDirectoryByIdAsync(int id);
    Task<IEnumerable<AppDirectory>> GetSubDirectoriesAsync(int parentId);
    Task AddDirectoryAsync(AppDirectory directory);
    Task UpdateDirectoryAsync(AppDirectory directory);
    Task DeleteDirectoryAsync(int id);
    Task SaveChangesAsync();
}