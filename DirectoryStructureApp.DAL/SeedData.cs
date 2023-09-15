using DirectoryStructureApp.DAL.Models;
using System;

namespace DirectoryStructureApp.DAL;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Directories.Any())
        {
            var rootDirectory = new AppDirectory { Name = "Creating Digital Images" };
            context.Directories.Add(rootDirectory);
            context.SaveChanges();

            var resources = new AppDirectory { Name = "Resources", ParentDirectoryId = rootDirectory.Id };
            var evidence = new AppDirectory { Name = "Evidence", ParentDirectoryId = rootDirectory.Id };
            var graphicProducts = new AppDirectory { Name = "Graphic Products", ParentDirectoryId = rootDirectory.Id };

            context.Directories.AddRange(resources, evidence, graphicProducts);
            context.SaveChanges();

            var primarySources = new AppDirectory { Name = "Primary Sources", ParentDirectoryId = resources.Id };
            var secondarySources = new AppDirectory { Name = "Secondary Sources", ParentDirectoryId = resources.Id };

            context.Directories.AddRange(primarySources, secondarySources);
            context.SaveChanges();
        }
    }
}