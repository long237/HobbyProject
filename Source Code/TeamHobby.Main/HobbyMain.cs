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

        //Console.WriteLine("Starting file compression: ");
        //Compress(fileInfo);
        //Console.WriteLine("Ending compression");
        //SqlConnection myconn = new SqlConnection();


        //IDataSource dataSource = new SQLSource();

        SQLSource sqlSource = new SQLSource();
        bool res = sqlSource.CompressFile(fileName);
        //bool res = CompressFile(fileName);
        Console.WriteLine(res);


    }


    public static void Compress(FileInfo fi)
    {
        // Get the stream of the source file.
        using (FileStream inFile = fi.OpenRead())
        {
            // Prevent compressing hidden and 
            // already compressed files.
            if ((File.GetAttributes(fi.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fi.Extension != ".gz")
            {
                // Create the compressed file.
                using (FileStream outFile =
                            File.Create(fi.FullName + ".gz"))
                {
                    using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                    {
                        // Copy the source file into 
                        // the compression stream.
                        inFile.CopyTo(Compress);

                        Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                            fi.Name, fi.Length.ToString(), outFile.Length.ToString());
                    }
                }
            }
        }
    }
}
