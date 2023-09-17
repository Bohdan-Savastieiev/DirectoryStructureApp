using AutoMapper;
using DirectoryStructureApp.BLL.DTOs;
using DirectoryStructureApp.BLL.Interfaces;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;

namespace DirectoryStructureApp.BLL.Services;

public class FileSystemService : IFileSystemService
{
    private readonly IMapper _mapper;

    private readonly string _baseExportPath;
    public FileSystemService(IMapper mapper, IHostEnvironment env)
    {
        _mapper = mapper;
        _baseExportPath = Path.Combine(env.ContentRootPath, "Exports");
    }

    public void ExportDirectoriesToFile(IEnumerable<DirectoryDto> directories)
    {
        var filePath = GetNewExportFilePath();
        var directoriesForSerialization = _mapper.Map<List<DirectorySerializationDto>>(directories);
        var json = JsonConvert.SerializeObject(directoriesForSerialization, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public List<DirectoryDto> ImportDirectoriesFromFile(string filePath)
    {
        var directories = new List<DirectoryDto>();
        if (!File.Exists(filePath))
        {
            return directories;
        }

        var json = File.ReadAllText(filePath);
        var deserializedDirectories = JsonConvert.DeserializeObject<List<DirectorySerializationDto>>(json);
        if (deserializedDirectories != null)
        {
            var mappedDirectories = _mapper.Map<List<DirectoryDto>>(deserializedDirectories);
            directories.AddRange(mappedDirectories);
        }

        return directories;
    }

    public List<DirectoryDto> ImportDirectoriesFromPath(string path)
    {
        var directories = new List<DirectoryDto>();
        if (Directory.Exists(path))
        {
            var rootDirectory = new DirectoryInfo(path);
            var rootDto = new DirectoryDto
            {
                Name = rootDirectory.Name
            };
            directories.Add(rootDto);
            PopulateSubDirectories(rootDto, rootDirectory);
        }
        return directories;
    }

    private void PopulateSubDirectories(DirectoryDto parentDto, DirectoryInfo parentDirectoryInfo)
    {
        DirectoryInfo[] subDirectories;
        try
        {
            subDirectories = parentDirectoryInfo.GetDirectories();
        }
        catch (UnauthorizedAccessException)
        {
            return;
        }

        foreach (var directory in subDirectories)
        {
            var directoryDto = new DirectoryDto
            {
                Name = directory.Name,
                ParentDirectory = parentDto
            };
            parentDto.SubDirectories.Add(directoryDto);
            PopulateSubDirectories(directoryDto, directory);
        }
    }
    private string GetNewExportFilePath()
    {
        var existingFiles = Directory.GetFiles(_baseExportPath).ToList();
        int nextNumber = 1;
        while (existingFiles.Contains(Path.Combine(_baseExportPath, $"DirectoryExport{nextNumber}.json")))
        {
            nextNumber++;
        }

        return Path.Combine(_baseExportPath, $"DirectoryExport{nextNumber}.json");
    }
}
