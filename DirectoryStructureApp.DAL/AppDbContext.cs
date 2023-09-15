using DirectoryStructureApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DirectoryStructureApp.DAL;

public class AppDbContext: DbContext
{
    public DbSet<AppDirectory> Directories { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

}