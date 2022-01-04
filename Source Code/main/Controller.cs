using System;
using System.Data.Odbc;
using TeamHobby.HobbyProjectGenerator.Archive;
using TeamHobby.HobbyProjectGenerator.DataAccessLayer;
using TeamHobby.HobbyProjectGenerator.UserManagement;

namespace TeamHobby.HobbyProjectGenerator.Main
{
    public class Controller
    {

        public static void Main(string[] args)
        {
            // Console customization
            // Change the look of the console
            Console.Title = "HobbyProjectGenerator";
            // Change console text color
            Console.ForegroundColor = ConsoleColor.Green;
            // Change terminal height
            Console.WindowHeight = 40;

            // Creating the Factory class
            // Logger log = new Logger();
            //Logger.PrintTest();

            // Loop login terminal
            bool mainMenu = true;

            // Loop Admin login
            while (mainMenu is true)
            {
                // Create credentials object
                GetCredentials credentials = new GetCredentials();

                // Get DB credentials ***Both values currently hard coded, will update to user input later on.***
                string dbUserName = "root";
                string dbPassword = "Teamhobby";

                // Admin Sign in
                Console.WriteLine("\nPlease Enter Admin Credentials " +
                    "or enter 0 to exit the machine\n");
                string? username = credentials.GetUserName();
                //int menuExit = Convert.ToInt32(username);
                // Exit Infinite Menu
                if (username == "0")
                {
                    break;
                }
                string? password = credentials.GetPassword();

                // Get time of login attempt
                DateTime TimeStamp = DateTime.UtcNow;

                // String for checking query return type
                string dbType = "sql";
                // Creating the Factory class
                RDSFactory dbFactory = new RDSFactory();

                // Testing Data Access Layer
                string dbInfo = "DRIVER={MariaDB ODBC 3.1 Driver};" +
                    "TCPIP=1;" +
                    "SERVER=localhost;" +
                    "DATABASE=hobby;" +
                    $"UID={dbUserName};" +
                    $"PASSWORD={dbPassword};" +
                    "OPTION=3";
                IDataSource<string> datasource = dbFactory.getDataSource(dbType, dbInfo);

                // While loop to keep this running forever with UserManagement
                // Testing archive Manager
                //while (true)
                //{
                string currentDate = DateTime.Now.ToString("dd");
                string currentTime = DateTime.Now.ToString("T");
                //Console.WriteLine("Current date: {0}, Current Time: {1}", currentDate, currentTime);
                if (String.Equals(currentDate, "01") && String.Equals(currentTime, "00:00:00 AM"))
                {
                    ArchiveManager archive = new ArchiveManager(datasource);
                    archive.Controller();
                }
                //}

                // Create manager class from UserManagement
                SystemAccountManager manager = new SystemAccountManager();

                // Create UserAccount class
                UserAccount user = new UserAccount(username, password, TimeStamp);
                // Call user object and wait for return string
                string isLogin = manager.CreateUserRecord(user, datasource);

                // If login is not incorrect and user is returning to login menu
                if (isLogin != "Access Denied: Unauthorized\n")
                {
                    Console.WriteLine("Returning to login...\n");
                    Console.WriteLine("-------------------------------------\n");
                }
                // If login is incorrect
                else
                {
                    Console.WriteLine("******Access Denied: Unauthorized******");
                }

            }
        }
    }
}



