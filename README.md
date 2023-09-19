# DirectoryStructureApp

## DAL (Data Access Layer)
- AppDbContext: Database context using Entity Framework Core. Defines DbSet for directories and database configuration.
- DirectoryEntity: Directory model for the database.
- IDirectoryRepository and DirectoryRepository: Repository for working with directories in the database and the interface for it.

## DTOs (Data Transfer Objects)
- DirectoryDto: The main object for representing a directory. Contains information about the directory, such as ID, name, parent directory ID, and a list of subdirectories.
- DirectorySerializationDto: Object for directory serialization.

## BLL (Business Logic Layer)
- IDirectoryService and DirectoryService: An interface and the service for working with directories, such as obtaining root directories, adding, and deleting directories.
- IFileSystemService and FileSystemService: An interface and the service for working with the file system, including importing directories from a path or file and exporting directories to a file.

## Web (MVC)
- Files folder: All export, import files and the Sqlite database are located here in the corresponding folders.
- DirectoryController: A controller with all main functionality such as:
  - Browsing Directories;
  - Directories Deletion;
  - Import Directories from Path;
  - Import Directories from File;
  - Export Directories to File.

