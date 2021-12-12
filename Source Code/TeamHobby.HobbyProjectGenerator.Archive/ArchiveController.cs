
using TeamHobby.HobbyProjectGenerator.DataAccess;

namespace TeamHobby.HobbyProjectGenerator.Archive
{
    public class ArchiveController
    {
        private IDataSource<string> _conn;

        // Constructor to take in the connection. 
        public ArchiveController(IDataSource<string> dataSource) {
            _conn = dataSource;
        }


        // Create the folder where the compress file will be stored
        public bool CreateArchiveFolder(){
            //string curPath = Directory.GetCurrentDirectory();
            string curPath = @"C:\";

            // Find the root drive of the program
            string drive = Path.GetPathRoot(path: curPath);
            // Check if Drive exists or not
            if (!Directory.Exists(drive))
            {
                Console.WriteLine("Drive {0} not found", drive);
                return false;
            }

            string archiveFolder = curPath + "\\HobbyArchive";
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
                string path = Directory.GetCurrentDirectory() + "\\HobbyArchive";
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
                string date = DateTime.Now.ToString("M_d_yyyy");
                string fileName = date + "_archive.csv";
                string filePath = System.IO.Path.Combine(archivePath, fileName);

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
            
            // Create the folder for Archiving
            CreateArchiveFolder();

            // Create the file name for output
            CreateOutFileName();
            return true;
        }





    }
}