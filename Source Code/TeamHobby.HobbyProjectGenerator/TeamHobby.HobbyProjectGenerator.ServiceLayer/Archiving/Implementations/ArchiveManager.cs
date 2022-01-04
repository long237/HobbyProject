
using TeamHobby.HobbyProjectGenerator.DataAccessLayer;

namespace TeamHobby.HobbyProjectGenerator.ServiceLayer
{
    public class ArchiveManager
    {
        private IDataSource<string> _conn;

        // Constructor to take in the connection. 
        public ArchiveManager(IDataSource<string> dataSource) {
            _conn = dataSource;
        }

        //public IDataSource<string> GetConnection()
        //{
        //    return _conn;
        //}

        // Create the folder where the compress file will be stored
        public bool CreateArchiveFolder(){
            //string curPath = Directory.GetCurrentDirectory();
            string curPath = @"C:\";
            string folderName = "\\HobbyArchive";

            // Find the root drive of the program
            string drive = Path.GetPathRoot(path: curPath);
            // Check if Drive exists or not
            try {
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
                    //return true;
                }
            }
            catch (Exception ex){
                // If the creating a folder failed, return false
                return false;
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

        // put everything toget here
        public bool Controller(){

            SqlDAO sqlDS = null;
            if (_conn.GetType() == typeof(SqlDAO))
            {
                sqlDS = (SqlDAO)_conn;
            }

            // Check to make sure that Sql Data source connection is not null
            if (sqlDS == null){
                return false;
            }
            // TODO: Remember to add try catch block here to make sure it is completed.

            //Creating the folder Archive
            Console.WriteLine("Creating a new folder ...");
            CreateArchiveFolder();
            Console.WriteLine("Creating a new folder completed ...");
            Console.WriteLine("----------------");
            Console.WriteLine("");

            // Creating a file name:
            //string filePath = @"C:\HobbyArchive";
            //Console.WriteLine("Creating file name ... ");
            //string curPath = archive.CreateOutFileName();
            Console.WriteLine("Creating Output file name ...");
            string outPath = CreateOutFileName();
            Console.WriteLine("Output file name created ...");
            Console.WriteLine("----------------");
            Console.WriteLine("");

            //string pathForward = @"C:\Users\Chunchunmaru\Documents\csulbFall2021\HobbyProject\Source Code\main\bin\Debug\net6.0\HobbyArchive";
            //string pathTemp = "C:/Temp/oldlogs10.txt";
            //string pathTempBack = @"C:\Temp\oldlogs10.txt";

            // Try catch block to delete abort the compressing process
            try{
                //Output SQL to a text file
                Console.WriteLine("Copying to a text file ...");
                sqlDS.CopyToFile(outPath);
                Console.WriteLine("Copying completed ...");
                Console.WriteLine("----------------");
                Console.WriteLine("");

                // Compress the file
                Console.WriteLine("Copressing the text file ...");
                sqlDS.CompressFile(outPath);
                Console.WriteLine("Copression completed ...");
                Console.WriteLine("----------------");
                Console.WriteLine("");

                //Remove output file
                Console.WriteLine("Removing the text file ...");
                sqlDS.RemoveOutputFile(outPath);
                Console.WriteLine("Text File removal completed ...");
                Console.WriteLine("----------------");
                Console.WriteLine("");

                // Remove entries fromt the database
                Console.WriteLine("Removing the text file ...");
                sqlDS.RemoveEntries();
                Console.WriteLine("Entries removal completed ...");
                Console.WriteLine("----------------");
                Console.WriteLine("");
                
                // Return true if compressing sequence is successful
                return true;
            }
            catch (Exception e){
                // Abort the compressing sequence and clean up uneccesary files. 
                Console.WriteLine("Archiving Sequenced failed. Cleaning up resources.");

                // Delete the text output file if any of the process failed
                if (File.Exists(outPath)){
                    File.Delete(outPath);
                }

                // Delete the compressed file if Removing entries and removing text file output failed
                string compFile = outPath + ".gz";
                if (File.Exists(compFile)){
                    File.Delete(compFile);
                }

                return false;
                
            }

            // Testing date time patters
            //string date = DateTime.Now.ToString("d");
            //Console.WriteLine(date);
            //Console.WriteLine(DateTime.Now.ToString("M_d_yyyy_H:mm:ss"));
        }

    }
}