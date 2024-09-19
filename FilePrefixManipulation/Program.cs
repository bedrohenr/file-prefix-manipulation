using System.IO;
using System.IO.Enumeration;

internal class Program
{
    private static void Main(string[] args) {
        Console.WriteLine("Started.");

        string prefix = "[SPOTIFY-DOWNLOADER.COM] ";
        string InsertedPath = "D:\\bkp\\Music\\MEmu Music";
        string[] FilePaths = Directory.GetFiles(InsertedPath);
        string[] DirPaths = Directory.GetDirectories(InsertedPath);
        string? FileName, NewFileName, NewFilePath;

        // TODO
        // Make recurrent calls inside every folder?
        Console.WriteLine("Directories: ");
        foreach (string DirPath in DirPaths) {
            Console.WriteLine(DirPath);
            // Get only dir name
            // Rename 
        }
        Console.WriteLine("End of directories.");

        Console.WriteLine("");

        Console.WriteLine("Files: ");
        foreach(string FilePath in FilePaths) {
            FileName = Path.GetFileName(FilePath);
            if(FileName.Contains(prefix)){
                NewFileName = FileName.Replace(prefix,"");
                NewFilePath = Path.GetDirectoryName(FilePath) + @"\" + NewFileName;
                File.Move(FilePath, NewFilePath);
                Console.WriteLine($"Renamed {FileName} to {NewFileName}.");
            }
            // Get only path name
            // Rename
        }
        Console.WriteLine("End of files.");
        
    }
}