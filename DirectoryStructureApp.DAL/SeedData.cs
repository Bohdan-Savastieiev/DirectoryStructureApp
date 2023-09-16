using DirectoryStructureApp.DAL.Models;
using System;

namespace DirectoryStructureApp.DAL;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Directories.Any())
        {
            var rootDirectory = new DirectoryEntity { Name = "Creating Digital Images" };
            context.Directories.Add(rootDirectory);
            context.SaveChanges();

            var resources = new DirectoryEntity { Name = "Resources", ParentDirectoryId = rootDirectory.Id };
            var evidence = new DirectoryEntity { Name = "Evidence", ParentDirectoryId = rootDirectory.Id };
            var graphicProducts = new DirectoryEntity { Name = "Graphic Products", ParentDirectoryId = rootDirectory.Id };

            context.Directories.AddRange(resources, evidence, graphicProducts);
            context.SaveChanges();

            var primarySources = new DirectoryEntity { Name = "Primary Sources", ParentDirectoryId = resources.Id };
            var secondarySources = new DirectoryEntity { Name = "Secondary Sources", ParentDirectoryId = resources.Id };

            context.Directories.AddRange(primarySources, secondarySources);
            context.SaveChanges();
        }
    }
}