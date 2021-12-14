using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamHobby.HobbyProjectGenerator.Archive;
using TeamHobby.HobbyProjectGenerator.DataAccess;

namespace TeamHobby.HobbyProjectGenerator.ArchiveTests
{
    public class ArchivingTests
    {
        //private string dbInfo = "DRIVER={MariaDB ODBC 3.1 Driver};" +
        //                  "SERVER=localhost;" +
        //                  "DATABASE=hobby;" +
        //                  "UID=root;" +
        //                  "PASSWORD=Teamhobby;" +
        //                  "OPTION=3";


        // [TestMethod]
        public void IsArchiveFolderCreated(IDataSource<string> sqlDAO)
        {
            // Arrange 
            //SqlDAO sqlDAO = new SqlDAO(dbInfo);
            ArchiveManager archiveManager = new ArchiveManager(sqlDAO);
            string folderPath = @"C:/HobbyArchive";
            bool expectedVal = true;

            // Act 
            archiveManager.CreateArchiveFolder();


            // Assert
            bool actualVal = Directory.Exists(folderPath);
            if (expectedVal == actualVal)
            {
                Console.WriteLine("Expected: {0}, Actual: {1}. Folder created correctly", expectedVal, actualVal);
                try
                {
                    Directory.Delete(folderPath, true);
                }
                catch {
                    Console.WriteLine("Folder failed to be deleted");
                }
            }
            else
            {
                Console.WriteLine("Expected: {0}, Actual: {1}. Folder created incorrectly", expectedVal, actualVal);
            }
        }

        // [TestMethod]
        public void IsFileNameCreated(IDataSource<string> sqlDAO)
        {
            // Arrange
            string foldPath = @"C:\HobbyArchive";
            string fileName = DateTime.Now.ToString("M_d_yyyy") + "_archive.csv";
            string expectedFilePath = System.IO.Path.Combine(foldPath, fileName);
            ArchiveManager archiveManager = new ArchiveManager(sqlDAO);

            // Act
            string actualFilePath = archiveManager.CreateOutFileName();

            // Assert
            bool result = String.Equals(actualFilePath, expectedFilePath);
            if (result)
            {
                Console.WriteLine("Expected: {0}, Actual: {1}. File name created correctly", expectedFilePath, actualFilePath);
            }
            else
            {
                Console.WriteLine("Expected: {0}, Actual: {1}. File name created incorrectly", expectedFilePath, actualFilePath);
            }

        }

        // [Test Method]
        public void IsCSVFileExist(IDataSource<string> sqlDAO)
        {
            // Arrange
            ArchiveManager archiveManager = new ArchiveManager(sqlDAO);
            SqlDAO sqlDS = null;
            string folderPath = @"C:/HobbyArchive";

            try
            {
                bool folderRes = archiveManager.CreateArchiveFolder();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Folder creation for copying failed");
            }
            string filePath = archiveManager.CreateOutFileName();
            bool expectedVal = true;

            if (sqlDAO.GetType() == typeof(SqlDAO))
            {
                sqlDS = (SqlDAO)sqlDAO;
            }

            // Act
            bool actualVal = false;
            if (sqlDS != null)
            {
                try
                {
                    actualVal = sqlDS.CopyToFile(filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Copying to a file failed !!");
                    Console.WriteLine(ex.Message);
                }
            }
            // Assert 
            bool result = File.Exists(filePath);
            if (result)
            {
                Console.WriteLine("Expected: {0}, Actual: {1}. CSV file created correctly", expectedVal, actualVal);
                // Clean up the folder use to test. 
                try
                {
                    Directory.Delete(folderPath, true);
                }
                catch
                {
                    Console.WriteLine("Folder failed to be deleted");
                }
            }
            else
            {
                Console.WriteLine("Expected: {0}, Actual: {1}. CSV file created incorrectly", expectedVal, actualVal);
            }

        }

        // [Test Method]
        public void IsProcessFlowCompleted(IDataSource<string> sqlDAO)
        {
            // Arrange 
            ArchiveManager archive = new ArchiveManager(sqlDAO);

            // Act
            // Testing archive Manager
            int i = -10;
            while (i < 20)
            {
                DateTime date1 = new DateTime(2021, 12, 1, 0, 0, 0);
                //Console.WriteLine("Testing first of month: {0}", date1.ToString("dd"));
                //string currentDate = DateTime.Now.ToString("dd");
                //string currentTime = DateTime.Now.ToString("T");
                string currentTime = "00:00:00 AM";
                string currentDate = i.ToString();

                if (i == 1)
                {
                    currentDate = date1.ToString("dd");
                    //ArchiveManager archive = new ArchiveManager(sqlDAO);
                    //archive.Controller();
                }

                //Console.WriteLine("Current date: {0}, Current Time: {1}", currentDate, currentTime);
                Console.WriteLine("Current date: {0}, Current Time: {1}", currentDate, currentTime);
                if (String.Equals(currentDate, "01") && String.Equals(currentTime, "00:00:00 AM"))
                {
                    Console.WriteLine("Archiving process Start");
                    //ArchiveManager archive = new ArchiveManager(sqlDAO);
                    archive.Controller();
                }

                i++;
            }

        }

        public void IsCleaningUpCompleted(IDataSource<string> sqlDAO)
        {
            //Arrange
            ArchiveManager archive = new ArchiveManager(sqlDAO);
            bool actualVal = false;
            bool expectedVal = false;

            // Act
            actualVal = archive.Controller();

            // Assert
            //bool result = !(actualVal && expectedVal);
            if (expectedVal == actualVal)
            {
                Console.WriteLine("Expected: {0}, Actual: {1}. Error handle correctly", expectedVal, actualVal);
            }
            else
            {
                Console.WriteLine("Expected: {0}, Actual: {1}. Error handle incorrectly", expectedVal, actualVal);
            }

        }

        public void RemoveEntriesTest(IDataSource<string> sqlDAO)
        {
            Console.WriteLine("TESTING - REMOVE ENTRIES"); 

            // Arrange
            ArchiveManager archiveManager = new ArchiveManager(sqlDAO);
            SqlDAO sqlDS = null;

            if (sqlDAO.GetType() == typeof(SqlDAO))
            {
                sqlDS = (SqlDAO)sqlDAO;
                sqlDS.GetConnection().Open();
                
                // Act
                Console.WriteLine("\nInserting into archive...");
                sqlDS.WriteData("INSERT into log(LtimeStamp, LvName, catname, userop, logmessage) values " +
                "('2021-08-07 23:00:00', 'Info', 'View', 'create some projects', 'new account created')," +
                "('2021-06-04 23:00:00', 'Info', 'Business', 'create some projects', 'new projects made')," +
                "('2021-07-02 23:00:00', 'Info', 'View', 'log out', 'log out successful')," +
                "('2021-09-03 23:00:00', 'Info', 'Business', 'log in', 'log in successfully')," +
                "('2021-10-20 23:00:00', 'Info', 'View', 'search for projects', 'result return')," +
                "('2021-09-03 23:00:00', 'Info', 'Business', 'log in', 'log in successfully');");

                OdbcDataReader odbcObj = (OdbcDataReader)sqlDS.ReadData("SELECT * FROM log WHERE DATEDIFF(CURRENT_TIMESTAMP, log.LtimeStamp) > 30;");

                int count = 0;
                while (odbcObj.Read()) { ++count; }
                Console.WriteLine("Number of entries > 30 days old: " + count);

                Console.WriteLine("\nRemoving entries from archive...");
                sqlDS.RemoveEntries();

                odbcObj = (OdbcDataReader)sqlDS.ReadData("SELECT * FROM log WHERE DATEDIFF(CURRENT_TIMESTAMP, log.LtimeStamp) > 30;");

                count = 0;
                while (odbcObj.Read()) { ++count; }
                Console.WriteLine("Number of entries > 30 days old: " + count);

                // Assert
                bool expectedVal = true;
                bool actualVal = !Convert.ToBoolean(count);
                
                if (expectedVal == actualVal)
                {
                    Console.WriteLine("\nExpected: {0}, Actual: {0}. Entries removed correctly.", expectedVal, actualVal);
                }
                else
                {
                    Console.WriteLine("\nExpected: {0}, Actual: {1}. Entries not removed.", expectedVal, actualVal);
                }

                sqlDS.GetConnection().Close();
            }

            return;
        }

        public void RemoveOutputFileTest(IDataSource<string> sqlDAO)
        {
            Console.WriteLine("TESTING - REMOVE OUTPUT FILE\n");
            
            // Arrange
            ArchiveManager archiveManager = new ArchiveManager(sqlDAO);
            SqlDAO sqlDS = null;

            if (sqlDAO.GetType() == typeof(SqlDAO))
            {
                sqlDS = (SqlDAO)sqlDAO;
            }

            // Act
            string filepath = archiveManager.CreateOutFileName();
            Console.WriteLine("\nRemoving file...");
            sqlDS.RemoveOutputFile(filepath);
            
            // Assert 
            bool expectedVal = false;
            bool actualVal = File.Exists(filepath);
            Console.WriteLine("Checking if file exists: " + actualVal);

            if (expectedVal == actualVal)
            {
                Console.WriteLine("\nExpected: {0}, Actual: {0}. Output file removed correctly.", expectedVal, actualVal);
            }
            else 
            {
                Console.WriteLine("\nExpected: {0}, Actual: {1}. Output file NOT removed.", expectedVal, actualVal);
            }

            return;
        }

        public static void Main(string[] args)
        {
            string dbInfo = "DRIVER={MariaDB ODBC 3.1 Driver};" +
                          "SERVER=localhost;" +
                          "DATABASE=hobby;" +
                          "UID=root;" +
                          "PASSWORD=Teamhobby;" +
                          "OPTION=3";
            SqlDAO sqlDAO = new SqlDAO(dbInfo);
            //ArchiveManager archiveManager = new ArchiveManager(sqlDAO);

            // Testing folder creation
            ArchivingTests test = new ArchivingTests();
            test.IsArchiveFolderCreated(sqlDAO);
            Console.WriteLine("-----------------");
            Console.WriteLine("");

            // Testing file creation
            test.IsFileNameCreated(sqlDAO);
            Console.WriteLine("-----------------");
            Console.WriteLine("");


            // Testing file creation
            test.IsCSVFileExist(sqlDAO);
            Console.WriteLine("-----------------");
            Console.WriteLine("");


            // Testing clean up sequence
            //test.IsCleaningUpCompleted(sqlDAO);
            //Console.WriteLine("-----------------");
            //Console.WriteLine("");

            // Testing remove entries
            test.RemoveEntriesTest(sqlDAO);
            Console.WriteLine("-----------------");
            Console.WriteLine("");

            // Testing remove output file
            test.RemoveOutputFileTest(sqlDAO);
            Console.WriteLine("-----------------");
            Console.WriteLine("");

        }

    }

}
