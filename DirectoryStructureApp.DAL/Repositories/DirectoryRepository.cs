using DirectoryStructureApp.DAL.Interfaces;
using DirectoryStructureApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace DirectoryStructureApp.DAL.Repositories;

public class DirectoryRepository : IDirectoryRepository
{
    private readonly AppDbContext _context;
    public DirectoryRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<DirectoryEntity>> GetAllDirectoriesAsync()
    {
        return await _context.Directories.ToListAsync();
    }

    public async Task<IEnumerable<DirectoryEntity>> GetDirectoriesWithoutParentAsync()
    {
        return await _context.Directories
            .Where(d => d.ParentDirectoryId == null)
            .ToListAsync();
    }

    public async Task<DirectoryEntity?> GetDirectoryByIdAsync(int id)
    {
        return await _context.Directories.FindAsync(id);
    }

    public async Task InsertDirectoryAsync(DirectoryEntity directory)
    {
        await _context.Directories.AddAsync(directory);
    }

    public void UpdateDirectory(DirectoryEntity directory)
    {
        _context.Entry(directory).State = EntityState.Modified;
    }

    public async Task DeleteDirectoryAsync(int id)
    {
        var directory = await _context.Directories.FindAsync(id);
        if (directory != null)
        {
            _context.Directories.Remove(directory);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}