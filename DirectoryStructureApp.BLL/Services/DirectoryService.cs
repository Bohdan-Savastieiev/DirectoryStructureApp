using AutoMapper;
using DirectoryStructureApp.BLL.DTOs;
using DirectoryStructureApp.BLL.Interfaces;
using DirectoryStructureApp.DAL.Interfaces;
using DirectoryStructureApp.DAL.Models;
using FluentValidation;
using System.Reflection.PortableExecutable;

namespace DirectoryStructureApp.BLL.Services;

public class DirectoryService : IDirectoryService
{
    private readonly IDirectoryRepository _directoryRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<DirectoryDto> _validator;

    public DirectoryService(
        IDirectoryRepository directoryRepository,
        IMapper mapper,
        IValidator<DirectoryDto> validator)
    {
        _directoryRepository = directoryRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<IEnumerable<DirectoryDto>> GetAllDirectoriesAsync()
    {
        var directoryEntities = await _directoryRepository.GetAllDirectoriesAsync();
        var directoryDtos = _mapper.Map<IEnumerable<DirectoryDto>>(directoryEntities);
        return directoryDtos;
    }
    public async Task<IEnumerable<DirectoryDto>> GetDirectoriesWithoutParentAsync()
    {
        var directoryEntities = await _directoryRepository.GetDirectoriesWithoutParentAsync();
        var directoryDtos = _mapper.Map<IEnumerable<DirectoryDto>>(directoryEntities);
        return directoryDtos;
    }
    public async Task<IEnumerable<DirectoryDto>> GetSubDirectoriesAsync(int parentDirectoryId)
    {
        var directoryEntities = await _directoryRepository.GetSubDirectoriesAsync(parentDirectoryId);
        var directoryDtos = _mapper.Map<IEnumerable<DirectoryDto>>(directoryEntities);
        return directoryDtos;
    }

    public async Task<DirectoryDto?> GetDirectoryByIdAsync(int id)
    {
        var directoryEntity = await _directoryRepository.GetDirectoryByIdAsync(id);
        var directoryDto = _mapper.Map<DirectoryDto>(directoryEntity);
        return directoryDto;
    }

    public async Task AddDirectoriesAsync(List<DirectoryDto> directories)
    {
        foreach (var directory in directories)
        {
            await AddDirectoryAsync(directory);
        }
    }
    public async Task AddDirectoryAsync(DirectoryDto directoryDto)
    {
        var validationResult = await _validator.ValidateAsync(directoryDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var directoryEntity = _mapper.Map<DirectoryEntity>(directoryDto);

        await _directoryRepository.AddDirectoryAsync(directoryEntity);
        await _directoryRepository.SaveChangesAsync();
    }

    public async Task UpdateDirectoryAsync(DirectoryDto directoryDto)
    {
        var validationResult = await _validator.ValidateAsync(directoryDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var directoryEntity = _mapper.Map<DirectoryEntity>(directoryDto);

        _directoryRepository.UpdateDirectory(directoryEntity);
        await _directoryRepository.SaveChangesAsync();
    }

    public async Task DeleteDirectoryAsync(int id)
    {
        await _directoryRepository.DeleteDirectoryAsync(id);
        await _directoryRepository.SaveChangesAsync();
    }
}
