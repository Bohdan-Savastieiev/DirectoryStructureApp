using AutoMapper;
using DirectoryStructureApp.BLL.DTOs;
using DirectoryStructureApp.DAL.Models;

namespace DirectoryStructureApp.BLL.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DirectoryEntity, DirectoryDto>();
        CreateMap<DirectoryDto, DirectoryEntity>();
    }
}