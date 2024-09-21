using System.IO;
using System.IO.Enumeration;

internal class Program
{
    private static bool DirectoriesInPath(string Path){
        string[] DirPaths = Directory.GetDirectories(Path);
        return DirPaths.Length > 0;
    }

    private static bool FilesInPath(string Path){
        string[] FilePaths = Directory.GetFiles(Path);
        return FilePaths.Length > 0;
    }

    private static void RerunInFolder(string Prefix, string? NewPrefix, string InsertedPath){
        // Checks for directories inside the folder
        if(DirectoriesInPath(InsertedPath)){
            // Rerun Program
            string DirName = Path.GetFileName(InsertedPath);
            Console.WriteLine($"Operating inside folder: {DirName}");
            ChangePrefixes(Prefix, NewPrefix, InsertedPath);
            Console.WriteLine($"Finished folder {DirName}");
        } else if (FilesInPath(InsertedPath)){
            // Modifies only files
            ChangeFilenamesPrefixes(Prefix, NewPrefix, InsertedPath);
        }
    }

    private static void ChangeDirectoriesPrefixes(string Prefix, string? NewPrefix, string InsertedPath){

        string[] DirPaths = Directory.GetDirectories(InsertedPath);
        string DirName, NewDirName, NewDirPath;

        if(DirPaths.Length == 0){
            Console.WriteLine("No directory found in the path.");
            return;
        }

        foreach (string DirPath in DirPaths) {
            DirName = Path.GetFileName(DirPath);

            // In case the present directory doesnt have the prefix matching
            if(!DirName.Contains(Prefix))
                continue;

            // Resolving new name and path
            NewDirName = DirName.Replace(Prefix,NewPrefix);
            NewDirPath = Path.GetDirectoryName(DirPath) + @"\" + NewDirName;

            // Renames the directory
            Directory.Move(DirPath, NewDirPath);

            // Message to command line
            Console.WriteLine($"DIRECTORY: Renamed {DirName} to {NewDirName}.");

            // Checks if the program needs to be rerun inside this folder (Has more directories inside of it or files)
            RerunInFolder(Prefix, NewPrefix, NewDirPath);
        }
    }

    private static void ChangeFilenamesPrefixes(string Prefix, string? NewPrefix, string InsertedPath){

        string[] FilePaths = Directory.GetFiles(InsertedPath);
        string FileName, NewFileName, NewFilePath;
        int c = 0; // Counts the amount of prefix matches 

        foreach(string FilePath in FilePaths) {

            // In case the filename doesnt have the prefix matching
            FileName = Path.GetFileName(FilePath);
            if(!FileName.Contains(Prefix))
                continue;

            // Resolving path and new filename
            NewFileName = FileName.Replace(Prefix,NewPrefix);
            NewFilePath = Path.GetDirectoryName(FilePath) + @"\" + NewFileName;

            // Renames the file
            File.Move(FilePath, NewFilePath);

            // Message to command line
            Console.WriteLine($"FILE: Renamed file {FileName} to {NewFileName}.");
            c++;

        }
    }

    private static void ChangePrefixes(string? Prefix, string? NewPrefix, string? InsertedPath){
        // Checks if prefix was inserted
        if(String.IsNullOrEmpty(Prefix)){
            Console.WriteLine("Prefix not inserted. Try again.");
            return;
        }
        // Checks if the path was inserted
        if(String.IsNullOrEmpty(InsertedPath)){
            Console.WriteLine("Path not inserted. Try again.");
            return;
        }

        // Renaming prefix in directories
        if(DirectoriesInPath(InsertedPath))
            ChangeDirectoriesPrefixes(Prefix, NewPrefix, InsertedPath);

        // Renaming prefixes of the files in the inserted directory
        if(FilesInPath(InsertedPath))
            ChangeFilenamesPrefixes(Prefix, NewPrefix, InsertedPath);
    }

    private static void Main(string[] args) {
        Console.WriteLine("Program Started.");

        // Will leave it nullable, we validate it further
        // And show error message
        string? Prefix, NewPrefix, Path;

        Console.Write("Insert file prefix: ");
        Prefix = Console.ReadLine();

        Console.Write("Insert new file prefix: ");
        NewPrefix = Console.ReadLine();

        Console.Write("Insert path of file or directory: ");
        Path = Console.ReadLine();

        ChangePrefixes(Prefix, NewPrefix, Path);

        Console.WriteLine("Program Finished.");
    }
}