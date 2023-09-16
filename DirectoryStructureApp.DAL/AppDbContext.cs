using DirectoryStructureApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DirectoryStructureApp.DAL;

public class AppDbContext: DbContext
{
    public DbSet<DirectoryEntity> Directories { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Database/myapp.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DirectoryEntity>()
            .Property(d => d.Name)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<DirectoryEntity>()
            .HasIndex(d => new { d.ParentDirectoryId, d.Name })
            .IsUnique();

        modelBuilder.Entity<DirectoryEntity>()
            .HasMany(pd => pd.SubDirectories)
            .WithOne(sd => sd.ParentDirectory)
            .HasForeignKey(sd => sd.ParentDirectoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}