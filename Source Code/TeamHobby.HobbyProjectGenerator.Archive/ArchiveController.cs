namespace TeamHobby.HobbyProjectGenerator.Archive
{
    public class ArchiveController
    {
        private IDataSource<string> _conn;

        // Constructor to take in the connection. 
        public ArchiveController(IDataSource<string> dataSource) {
            _conn = dataSource;
        }

        public IDataSource<string> GetDataSource(){
            return _conn;
        }

        // Create the folder where the compress file will be stored
        public bool CreateArchiveFolder(){
            return true;
        }

        // Return the name of the new output file with Path
        public string CreateOutFileName(){
            return "hello";
        }

        // put everything toget here
        public bool Controller(){
            return true;
        }





    }
}