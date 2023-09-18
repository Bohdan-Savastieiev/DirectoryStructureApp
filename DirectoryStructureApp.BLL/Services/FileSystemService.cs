using AutoMapper;
using DirectoryStructureApp.BLL.DTOs;
using DirectoryStructureApp.BLL.Interfaces;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace DirectoryStructureApp.BLL.Services;

public class FileSystemService : IFileSystemService
{
    private readonly IMapper _mapper;
    private readonly string _exportPath;
    private readonly string _importPath;
    public FileSystemService(IMapper mapper, IHostEnvironment env)
    {
        _mapper = mapper;
        _exportPath = Path.Combine(env.ContentRootPath, "Files/Exports");
        _importPath = Path.Combine(env.ContentRootPath, "Files/Imports");
        CreateNecessaryFolders();
    }

    public void ExportDirectoriesToFile(IEnumerable<DirectoryDto> directories)
    {
        var filePath = GetNewFilePath(FileCommunicationType.Export);
        var directoriesForSerialization = _mapper.Map<List<DirectorySerializationDto>>(directories);
        var json = JsonConvert.SerializeObject(directoriesForSerialization, Formatting.Indented);
        File.WriteAllText(filePath, json);
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
        else
        {
            throw new DirectoryNotFoundException(nameof(path));
        }
        return directories;
    }

    public async Task<List<DirectoryDto>> ImportDirectoriesFromFileAsync(IFormFile file)
    {
        var filePath = GetNewFilePath(FileCommunicationType.Import);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var serializedDirectories = File.ReadAllText(filePath);
        return DeserializeAndMapDirectoriesFromFile(serializedDirectories);
    }
    private List<DirectoryDto> DeserializeAndMapDirectoriesFromFile(string serializedDirectories)
    {
        var directories = new List<DirectoryDto>();
        var deserializedDirectories = JsonConvert.DeserializeObject<List<DirectorySerializationDto>>(serializedDirectories);
        if (deserializedDirectories != null)
        {
            var mappedDirectories = _mapper.Map<List<DirectoryDto>>(deserializedDirectories);
            directories.AddRange(mappedDirectories);
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
    private string GetNewFilePath(FileCommunicationType communicationType)
    {
        var path = communicationType == FileCommunicationType.Export
                        ? _exportPath
                        : _importPath;
        string type = communicationType.ToString();
        var existingFiles = Directory.GetFiles(path).ToList();
        int nextNumber = 1;
        while (existingFiles.Contains(Path.Combine(path, $"Directory{type}{nextNumber}.json")))
        {
            nextNumber++;
        }

        return Path.Combine(path, $"Directory{type}{nextNumber}.json");
    }

    private void CreateNecessaryFolders()
    {
        if (!Directory.Exists(_exportPath))
        {
            Directory.CreateDirectory(_exportPath);
        }
        if (!Directory.Exists(_importPath))
        {
            Directory.CreateDirectory(_importPath);
        }
    }

    private enum FileCommunicationType
    {
        Export,
        Import
    }
}
