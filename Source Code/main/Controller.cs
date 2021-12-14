using System;
using System.Data.Odbc;
using TeamHobby.HobbyProjectGenerator.Archive;
using TeamHobby.HobbyProjectGenerator.DataAccess;
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
                // Admin Sign in
                GetCredentials credentials = new GetCredentials();
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
                    "UID=root;" +
                    "PASSWORD=Teamhobby;" +
                    "OPTION=3";
                IDataSource<string> datasource = dbFactory.getDataSource(dbType, dbInfo);
                // Create manager class from UserManagement
                SystemAccountManager manager = new SystemAccountManager();

                // Create UserAccount class
                UserAccount user = new UserAccount(username, password, TimeStamp);

                string isLogin = manager.CreateUserRecord(user, datasource);


                if (isLogin != "Access Denied: Unauthorized\n")
                {
                    Console.WriteLine("Returning to login...\n");
                    Console.WriteLine("-------------------------------------\n");
                }
                else
                {
                    Console.WriteLine("******Access Denied: Unauthorized******");
                }
                
            }

            /*  // While loop to keep this running forever with UserManagement
              // Testing archive Manager
              //while (true)
              //{
              string currentDate = DateTime.Now.ToString("dd");
              string currentTime = DateTime.Now.ToString("T");
              Console.WriteLine("Current date: {0}, Current Time: {1}", currentDate, currentTime);
              if (String.Equals(currentDate, "1") && String.Equals(currentTime, "00:00:00 AM"))
              {
                  ArchiveManager archive = new ArchiveManager(datasource);
                  archive.Controller();
              }
              //}*/



            //Creating the folder Archive
            //Console.WriteLine("Creating a new folder ...");
            //archive.CreateArchiveFolder();
            //Console.WriteLine("");

            //// Creating a file name:
            //string filePath = @"C:\HobbyArchive";
            ////Console.WriteLine("Creating file name ... ");
            ////string curPath = archive.CreateOutFileName();
            //string curPath = archive.CreateOutFileName(filePath);

            ////string pathForward = @"C:\Users\Chunchunmaru\Documents\csulbFall2021\HobbyProject\Source Code\main\bin\Debug\net6.0\HobbyArchive";
            ////string pathTemp = "C:/Temp/oldlogs10.txt";
            ////string pathTempBack = @"C:\Temp\oldlogs10.txt";
            //Console.WriteLine("----------------");

            ////Output SQL to a text file
            //sqlDS.CopyToFile(curPath);

            //// Compress the file
            //sqlDS.CompressFile(curPath);

            ////Remove output file
            //sqlDS.RemoveOutputFile(curPath);

            //// Remove entries fromt the database
            //sqlDS.RemoveEntries();




            // 2.Inserting Data into the database:
            //string sqlWrite = "INSERT into log(lvname, catname, userop, logmessage) values " +
            //    "('Info', 'View', 'Testing DAL stuffs', 'new DAL method tested');";

            //Console.WriteLine("Writing to the database... ");
            //datasource.WriteData(sqlWrite);
            //Console.WriteLine("Writing completed. ");

            // 3. Removing from a database
            //string sqlRemove = "DELETE from log where logID = 28;";
            //Console.WriteLine("Writing to the database... ");
            //datasource.WriteData(sqlRemove);
            //Console.WriteLine("Writing completed. ");
        }
    }
}
