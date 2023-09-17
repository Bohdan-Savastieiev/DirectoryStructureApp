namespace DirectoryStructureApp.BLL.DTOs;

public class DirectoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? ParentDirectoryId { get; set; }
    public DirectoryDto? ParentDirectory { get; set; }
    public List<DirectoryDto> SubDirectories { get; set; } = new List<DirectoryDto>();
}