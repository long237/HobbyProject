using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamHobby.HobbyProjectGenerator.DataAccess;
using TeamHobby.HobbyProjectGenerator.UserManagement;

namespace main
{
    internal class FileExtract
    {
        public static void Main(string[] args)
        {
            string[] lines = { "First line", "Second line", "Third line" };
            using StreamWriter file = new ("BulkRequest.txt");

            var serviceTest = new AccountService();
            string dbType = "sql";
            RDSFactory dbFactory = new RDSFactory();


            // Testing Data Access Layer
            string dbInfo = "DRIVER={MariaDB ODBC 3.1 Driver};" +
                "TCPIP=1;" +
                "SERVER=localhost;" +
                "DATABASE=hobby;" +
                "UID=root;" +
                "PASSWORD=hobby;" +
                "OPTION=3";
            IDataSource<string> datasource = dbFactory.getDataSource(dbType, dbInfo);

            // Create a file
            for (int i = 0; i < 10000; i++)
            {
                var TestAcc = new UserAccount("newUser" + $"{i}", "4567", $"email{i}@a.com", "regular", DateTime.Now);
                file.Write(serviceTest.CreateUserRecord(TestAcc, "Rifat", datasource));
            }
        }
    }
}
