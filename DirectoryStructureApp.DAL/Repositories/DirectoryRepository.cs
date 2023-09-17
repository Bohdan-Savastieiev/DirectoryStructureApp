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
        return await _context.Directories
            .Include(d => d.SubDirectories)
            .ToListAsync();
    }

    public async Task<IEnumerable<DirectoryEntity>> GetDirectoriesWithoutParentAsync()
    {
        return await _context.Directories
            .Include(d => d.SubDirectories)
            .Where(d => d.ParentDirectoryId == null)
            .ToListAsync();
    }
    public async Task<IEnumerable<DirectoryEntity>> GetSubDirectoriesAsync(int directoryParentId)
    {
        return await _context.Directories
            .Include(d => d.SubDirectories)
            .Where(d => d.ParentDirectoryId == directoryParentId)
            .ToListAsync();
    }

    public async Task<DirectoryEntity?> GetDirectoryByIdAsync(int id)
    {
        return await _context.Directories
            .Include(d => d.SubDirectories)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task AddDirectoryAsync(DirectoryEntity directory)
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