using DirectoryStructureApp.BLL.DTOs;

namespace DirectoryStructureApp.BLL.Interfaces;

public interface IDirectoryService
{
    Task<IEnumerable<DirectoryDto>> GetDirectoriesWithoutParentAsync();
}
