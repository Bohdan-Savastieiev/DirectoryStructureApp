using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryStructureApp.DAL.Models;

public class DirectoryEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? ParentDirectoryId { get; set; }
    public DirectoryEntity? ParentDirectory { get; set; }
    public ICollection<DirectoryEntity>? SubDirectories { get; set; }

}