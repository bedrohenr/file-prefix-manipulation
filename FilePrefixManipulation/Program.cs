using System.IO;
using System.IO.Enumeration;

internal class Program
{
    private static void Main(string[] args) {
        Console.WriteLine("Started.");

        string InsertedPath = "D:\\bkp\\Music\\MEmu Music";
        string[] FilePaths = Directory.GetFiles(InsertedPath);
        string[] DirPaths = Directory.GetDirectories(InsertedPath);

        Console.WriteLine("Directories: ");
        foreach (string DirName in DirPaths)
        {
            Console.WriteLine(DirName);
        }
        Console.WriteLine("End of directories.");

        Console.WriteLine("");

        Console.WriteLine("Files: ");
        foreach(string FileName in FilePaths) {
            Console.WriteLine(FileName);
        }
        Console.WriteLine("End of files.");
        
    }
}