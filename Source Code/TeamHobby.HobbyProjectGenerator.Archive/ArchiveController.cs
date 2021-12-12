
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
            string curPath = Directory.GetCurrentDirectory();
            //string temp = @
            string archiveFolder = curPath + "\\HobbyArhive";
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
                string path = Directory.GetCurrentDirectory();
                Console.WriteLine("The current directory is {0}", path);
                return path;
            }
            catch
            {
                Console.WriteLine("ArchiveCon: Creating a file name failed. ");
                return "";
            }
        }

        // put everything toget here
        public bool Controller(){
            return true;
        }





    }
}