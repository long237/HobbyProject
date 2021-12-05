// See https://aka.ms/new-console-template for more information
using System.Data.SqlClient;
using System.IO.Compression;
using TeamHobby.HobbyProjectGenerator.Archive;

public class HobbyMain
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");

        // Testing Compressing a file
        string fileName = @"C:\Users\Chunchunmaru\Documents\csulbFall2021\HobbyProject\Source Code\TeamHobby.Main\archiving2.txt";
        //string fileName = @"arhiving2.txt";
        FileInfo fileInfo = new FileInfo(fileName);

        Console.WriteLine("File Name: {0}", fileInfo.FullName);

        SQLSource sqlSource = new SQLSource();
        bool res = sqlSource.CompressFile(fileName);
        //bool res = CompressFile(fileName);
        Console.WriteLine(res);

        Console.WriteLine(CreateFileName());

    }
    
    public static string CreateFileName()
    {
        return DateTime.Now.ToString() + "archive.txt";
    }
}
