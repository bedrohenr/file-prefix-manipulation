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

        Console.WriteLine("Renaming directories");
        foreach (string DirPath in DirPaths) {
            // In case the directory doesnt have the prefix matching
            DirName = Path.GetFileName(DirPath);
            if(!DirName.Contains(Prefix))
                continue;

            NewDirName = DirName.Replace(Prefix,NewPrefix);
            NewDirPath = Path.GetDirectoryName(DirPath) + @"\" + NewDirName;

            Directory.Move(DirPath, NewDirPath);
            Console.WriteLine($"Renamed folder {DirName} to {NewDirName}.");

            // Checks if the program needs to be rerun inside this folder
            RerunInFolder(Prefix, NewPrefix, NewDirPath);
        }
        Console.WriteLine("End of directories.");
        
    }

    private static void ChangeFilenamesPrefixes(string Prefix, string? NewPrefix, string InsertedPath){

        string[] FilePaths = Directory.GetFiles(InsertedPath);
        string FileName, NewFileName, NewFilePath;
        int c = 0;

        Console.WriteLine("Renaming files");
        foreach(string FilePath in FilePaths) {

            // In case the filename doesnt have the prefix matching
            FileName = Path.GetFileName(FilePath);
            if(!FileName.Contains(Prefix))
                continue;

            NewFileName = FileName.Replace(Prefix,NewPrefix);
            NewFilePath = Path.GetDirectoryName(FilePath) + @"\" + NewFileName;
            File.Move(FilePath, NewFilePath);
            Console.WriteLine($"Renamed file {FileName} to {NewFileName}.");
            c++;

        }
        if(c == 0)
            Console.WriteLine("No prefix matches found.");
        else
            Console.WriteLine($"{c} files renamed.");

        Console.WriteLine("End of files.");
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

        // Strings
        string FileName, NewFileName, NewFilePath;
        string[] FilePaths = Directory.GetFiles(InsertedPath);
        string[] DirPaths = Directory.GetDirectories(InsertedPath);

        Console.WriteLine("Directories: ");
        foreach (string DirPath in DirPaths) {
            Console.WriteLine(DirPath);
            FileName = Path.GetFileName(DirPath);
            NewFileName = FileName.Replace(Prefix,"");
            NewFilePath = Path.GetDirectoryName(DirPath) + @"\" + NewFileName;
            Directory.Move(DirPath, NewFilePath);
            ChangeFilesPrefix(Prefix, NewPrefix, InsertedPath); // Renaming inside the directory
            Console.WriteLine($"Renamed {FileName} to {NewFileName}.");
        }
        Console.WriteLine("End of directories.");

        Console.WriteLine("");

        Console.WriteLine("Files: ");
        foreach(string FilePath in FilePaths) {
            FileName = Path.GetFileName(FilePath);
            if(FileName.Contains(Prefix)){
                NewFileName = FileName.Replace(Prefix,"");
                NewFilePath = Path.GetDirectoryName(FilePath) + @"\" + NewFileName;
                File.Move(FilePath, NewFilePath);
                Console.WriteLine($"Renamed {FileName} to {NewFileName}.");
            }
        }
        Console.WriteLine("End of files.");
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