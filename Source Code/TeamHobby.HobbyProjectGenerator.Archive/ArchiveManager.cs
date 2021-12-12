
using TeamHobby.HobbyProjectGenerator.DataAccess;

namespace TeamHobby.HobbyProjectGenerator.Archive
{
    public class ArchiveManager
    {
        private IDataSource<string> _conn;

        // Constructor to take in the connection. 
        public ArchiveManager(IDataSource<string> dataSource) {
            _conn = dataSource;
        }


        // Create the folder where the compress file will be stored
        public bool CreateArchiveFolder(){
            //string curPath = Directory.GetCurrentDirectory();
            string curPath = @"C:\";
            string folderName = "\\HobbyArchive";

            // Find the root drive of the program
            string drive = Path.GetPathRoot(path: curPath);
            // Check if Drive exists or not
            if (!Directory.Exists(drive))
            {
                Console.WriteLine("Drive {0} not found", drive);
                return false;
            }

            string archiveFolder = curPath + folderName;
            //Console.WriteLine("Creating a new folder at: {0}", archiveFolder);
            if (!Directory.Exists(archiveFolder))
            {
                Console.WriteLine("Creating a new folder at: {0}", archiveFolder);
                Directory.CreateDirectory(archiveFolder);
            }
            return true;
        }


        // Return the name of the new output file with Path
        public string CreateOutFileName() {
            try {
                // Creating a file name:
                string path = @"C:\HobbyArchive";
                //string path = Directory.GetCurrentDirectory() + "\\HobbyArchive";
                Console.WriteLine("The current directory is {0}", path);
                string date = DateTime.Now.ToString("M_d_yyyy");
                string fileName = date + "_archive.csv";
                string filePath = System.IO.Path.Combine(path, fileName);

                Console.WriteLine("Date: {0}", date);
                Console.WriteLine("Filepath: {0}", filePath);            
                return filePath;
            }
            catch
            {
                Console.WriteLine("ArchiveCon: Creating a file name failed. ");
                return "";
            }
        }

        // Method overload to take in a specific path
        public string CreateOutFileName(string archivePath)
        {
            try
            {
                //string path = archivePath + "\\HobbyArchive";
                Console.WriteLine("The current directory is {0}", archivePath);
                //string date = DateTime.Now.ToString("M_d_yyyy_H:mm:ss");
                string date = DateTime.Now.ToString("M_d_yyyy");
                string fileName = date + "_archive.csv";
                string filePath = Path.Combine(archivePath, fileName);

                Console.WriteLine("Date: {0}", date);
                Console.WriteLine("Filepath: {0}", filePath);
                return filePath;
            }
            catch
            {
                Console.WriteLine("ArchiveCon: Creating a file name failed. ");
                return "";
            }
        }

        // put everything toget here
        public bool Controller(){

            SqlDAO sqlDS = null;
            if (_conn.GetType() == typeof(SqlDAO))
            {
                sqlDS = (SqlDAO)_conn;

            }
            // TODO: Remember to add try catch block here to make sure it is completed.

            //Creating the folder Archive
            Console.WriteLine("Creating a new folder ...");
            CreateArchiveFolder();
            Console.WriteLine("Creating a new folder completed ...");
            Console.WriteLine("----------------");
            Console.WriteLine("");

            // Creating a file name:
            string filePath = @"C:\HobbyArchive";
            //Console.WriteLine("Creating file name ... ");
            //string curPath = archive.CreateOutFileName();
            Console.WriteLine("Creating Output file name ...");
            string curPath = CreateOutFileName();
            Console.WriteLine("Output file name created ...");
            Console.WriteLine("----------------");
            Console.WriteLine("");

            //string pathForward = @"C:\Users\Chunchunmaru\Documents\csulbFall2021\HobbyProject\Source Code\main\bin\Debug\net6.0\HobbyArchive";
            //string pathTemp = "C:/Temp/oldlogs10.txt";
            //string pathTempBack = @"C:\Temp\oldlogs10.txt";

            //Output SQL to a text file
            Console.WriteLine("Copying to a text file ...");
            sqlDS.CopyToFile(curPath);
            Console.WriteLine("Copying completed ...");
            Console.WriteLine("----------------");
            Console.WriteLine("");

            // Compress the file
            Console.WriteLine("Copressing the text file ...");
            sqlDS.CompressFile(curPath);
            Console.WriteLine("Copression completed ...");
            Console.WriteLine("----------------");
            Console.WriteLine("");

            //Remove output file
            Console.WriteLine("Removing the text file ...");
            sqlDS.RemoveOutputFile(curPath);
            Console.WriteLine("Text File removal completed ...");
            Console.WriteLine("----------------");
            Console.WriteLine("");

            // Remove entries fromt the database
            Console.WriteLine("Removing the text file ...");
            sqlDS.RemoveEntries();
            Console.WriteLine("Entries removal completed ...");
            Console.WriteLine("----------------");
            Console.WriteLine("");

            // Testing date time patters
            //string date = DateTime.Now.ToString("d");
            //Console.WriteLine(date);
            //Console.WriteLine(DateTime.Now.ToString("M_d_yyyy_H:mm:ss"));

            return true;

        }





    }
}