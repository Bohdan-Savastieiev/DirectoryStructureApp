using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryStructureApp.BLL.DTOs
{
    public class DirectorySerializationDto
    {
        public string Name { get; set; } = null!;
        public List<DirectorySerializationDto> SubDirectories { get; set; } = new List<DirectorySerializationDto>();

    }
}