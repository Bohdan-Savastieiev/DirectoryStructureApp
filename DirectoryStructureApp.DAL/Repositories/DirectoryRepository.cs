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

    public async Task<IEnumerable<DirectoryEntity>> GetRootDirectoriesAsync()
    {
        return await _context.Directories
            .Include(d => d.SubDirectories)
            .Where(d => d.ParentDirectoryId == null)
            .ToListAsync();
    }
    public async Task<IEnumerable<DirectoryEntity>> GetRootDirectoriesWithAllSubsAsync()
    {
        var directories = await GetRootDirectoriesAsync();
        foreach(var directory in directories)
        {
            await GetAllSubDirectoriesAsync(directory);
        }

        return directories;
    }

    public async Task<DirectoryEntity?> GetDirectoryByIdAsync(int id)
    {
        return await _context.Directories
            .Include(d => d.SubDirectories)
            .FirstOrDefaultAsync(d => d.Id == id);
    }
    public async Task<DirectoryEntity?> GetDirectoryByIdWithAllSubsAsync(int id)
    {
        var directory = await GetDirectoryByIdAsync(id);
        await GetAllSubDirectoriesAsync(directory);

        return directory;
    }

    public async Task AddDirectoryAsync(DirectoryEntity directory)
    {
        await _context.Directories.AddAsync(directory);
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

    private async Task GetAllSubDirectoriesAsync(DirectoryEntity? directory)
    {
        if (directory != null && directory.SubDirectories != null)
        {
            foreach (var subDirectory in directory.SubDirectories)
            {
                await LoadAllSubDirectoriesAsync(subDirectory);
            }
        }
    }

    private async Task LoadAllSubDirectoriesAsync(DirectoryEntity directory)
    {
        var subDirectories = await _context.Directories
            .Include(d => d.SubDirectories)
            .Where(d => d.ParentDirectoryId == directory.Id)
            .ToListAsync();

        if (directory.SubDirectories != null)
        {
            foreach (var subDirectory in subDirectories)
            {
                directory.SubDirectories.Add(subDirectory);
            }
        }

        foreach (var subDirectory in subDirectories)
        {
            await LoadAllSubDirectoriesAsync(subDirectory);
        }
    }
}