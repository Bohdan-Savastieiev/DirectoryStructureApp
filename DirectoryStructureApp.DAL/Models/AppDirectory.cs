using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryStructureApp.DAL.Models;

public class AppDirectory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentDirectoryId { get; set; }
    public AppDirectory? ParentDirectory { get; set; }
    public List<AppDirectory> SubDirectories { get; set; }

}