# DirectoryStructureApp

## DAL (Data Access Layer)
- AppDbContext: Database context using Entity Framework Core. Defines DbSet for directories and database configuration.
- DirectoryEntity: Directory model for the database.
- IDirectoryRepository and DirectoryRepository: Repository for working with directories in the database and the interface for it.

## DTOs (Data Transfer Objects)
- DirectoryDto: The main object for representing a directory. Contains information about the directory, such as ID, name, parent directory ID, and a list of subdirectories.
- DirectorySerializationDto: Object for directory serialization.

## BLL (Business Logic Layer)
- IDirectoryService and DirectoryService: An interface defining the main methods for working with directories, such as obtaining root directories, adding, and deleting directories.
- IFileSystemService and FileSystemService: Interface for working with the file system, including importing directories from a path or file and exporting directories to a file.

## Web (MVC)
- Exports folder: All export files with serialized directories are located here.
- Database folder: Sqlite database is located here.
- DirectoryController: A controller with all main functionality such as:
  - Browsing Directories;
  - Directories Deletion;
  - Import Directories from Path;
  - Import Directories from File;
  - Export Directories to File.

