using DirectoryStructureApp.DAL.Interfaces;
using DirectoryStructureApp.DAL.Models;

namespace DirectoryStructureApp.DAL.Repositories;

public class DirectoryRepository : IDirectoryRepository
{
    private readonly AppDbContext _context;
    public DirectoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task AddDirectoryAsync(AppDirectory directory)
    {
        throw new NotImplementedException();
    }

    public Task DeleteDirectoryAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AppDirectory>> GetAllDirectoriesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<AppDirectory> GetDirectoryByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AppDirectory>> GetSubDirectoriesAsync(int parentId)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateDirectoryAsync(AppDirectory directory)
    {
        throw new NotImplementedException();
    }
}