using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamHobby.HobbyProjectGenerator.DataAccess;
using TeamHobby.HobbyProjectGenerator.UserManagement;

namespace main
{
    public class GenerateOpFile
    {
        public void GenerateOPFile()
        //public static void Main(string[] args)
        {
            try
            {
                // Get the current directory.
                string path = Directory.GetCurrentDirectory();
                // Open file stream
                using StreamWriter file = new(path + "\\BulkOps\\Bulk.txt", append: true);
                // Show location of the file being written
                Console.WriteLine(path + "\\BulkOps\\Bulk.txt");
                
                // List choices of operations
                List<string> ops = new List<string> {"Create Account", "Edit Account",
                           "Delete Account", "Disable Account", "Enable Account"};
                // Create Account service object
                AccountService serviceTest = new AccountService();

                // Find database and connect to it 
                string dbType = "sql";
                RDSFactory dbFactory = new RDSFactory();

                // Testing Data Access Layer
                string dbInfo = "DRIVER={MariaDB ODBC 3.1 Driver};" +
                    "TCPIP=1;" +
                    "SERVER=localhost;" +
                    "DATABASE=hobby;" +
                    "UID=root;" +
                    "PASSWORD=Teamhobby;" +
                    "OPTION=3";
                IDataSource<string> datasource = dbFactory.getDataSource(dbType, dbInfo);

                // Create a file
                // Initialize value for random operation
                Random random = new Random();
                int index = 0;
                for (int i = 0; i < 10000; i++)
                {
                    // Choose random operation
                    index = random.Next(ops.Count);
                    // Create next dummy data
                    UserAccount testAcc = new UserAccount("newUser" + $"{i}", "4567", $"email{i}@aol.com", "regular", DateTime.Now);
                    // Write to file
                    // Write Operation: for now it is only create accounts to avoid errors
                    file.WriteLine($"Operation: {ops[0]}");
                    // Write credentials in the format of: (UserName, Password, Role, IsActive, CreatedBy, CreatedDate, Email)
                    file.WriteLine($"Username: {testAcc.username}\nPassword: {testAcc.password}\nRole: {testAcc.role}\nEmail: {testAcc.email}\n");
                }
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading file");
                Console.WriteLine(e.Message);
            }
        }
    }
}
