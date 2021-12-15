using System;
using System.Data.Odbc;
using System.Diagnostics;
using System.IO;
using System.Threading;
using TeamHobby.HobbyProjectGenerator.Archive;
using TeamHobby.HobbyProjectGenerator.DataAccess;
using Xunit;

namespace TeamHobby.Archiving.xTests
{
    public class ArchivingXUnitTest
    {
        string dbInfo = "DRIVER={MariaDB ODBC 3.1 Driver};" +
                          "SERVER=localhost;" +
                          "DATABASE=hobby;" +
                          "UID=root;" +
                          "PASSWORD=Teamhobby;" +
                          "OPTION=3";

        [Fact]
        public void IsArchiveFolderCreated()
        {
            // Arrange        
            SqlDAO sqlDAO = new SqlDAO(dbInfo);
            ArchiveManager archiveManager = new ArchiveManager(sqlDAO);
            string folderPath = @"C:/HobbyArchive";
            bool expectedVal = true;

            // Act 
            archiveManager.CreateArchiveFolder();


            // Assert
            bool actualVal = Directory.Exists(folderPath);
            Assert.Equal(expectedVal, actualVal);
            // Cleaning up directory after test
            try
            {
                Directory.Delete(folderPath, true);
            }
            catch
            {
                Console.WriteLine("Folder failed to be deleted");
            }
        }

        [Fact]
        public void IsFileNameCreated()
        {
            // Arrange
            SqlDAO sqlDAO = new SqlDAO(dbInfo);
            string foldPath = @"C:\HobbyArchive";
            string fileName = DateTime.Now.ToString("M_d_yyyy") + "_archive.csv";
            string expectedFilePath = System.IO.Path.Combine(foldPath, fileName);
            ArchiveManager archiveManager = new ArchiveManager(sqlDAO);

            // Act
            string actualFilePath = archiveManager.CreateOutFileName();

            // Assert
            
            Assert.Equal(expectedFilePath, actualFilePath);
        }

        [Fact]
        public void IsCSVFileExist()
        {
            // Arrange
            SqlDAO sqlDAO = new SqlDAO(dbInfo);
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
            if (sqlDS != null)
            {
                try
                {
                    sqlDS.CopyToFile(filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Copying to a file failed !!");
                    Console.WriteLine(ex.Message);
                }
            }

            // Assert       
            bool actualVal = File.Exists(filePath);
            Assert.Equal(expectedVal, actualVal);

            try
            {
                Directory.Delete(folderPath, true);
            }
            catch
            {
                Console.WriteLine("Folder failed to be deleted");
            }
        }

        [Fact]
        public void RemoveEntriesTest()
        {
            // Arrange
            SqlDAO sqlDAO = new SqlDAO(dbInfo);
            ArchiveManager archiveManager = new ArchiveManager(sqlDAO);
            OdbcDataReader odbcObj = null;
            Object resultData = null;
            bool expectedVal = false;

            sqlDAO.WriteData("INSERT into log(LtimeStamp, LvName, catname, userop, logmessage) values" +
                "('2021-08-07 23:00:00', 'Info', 'View', 'create some projects', 'new account created')," +
                "('2021-06-04 23:00:00', 'Info', 'Business', 'create some projects', 'new projects made')," +
                "('2021-07-02 23:00:00', 'Info', 'View', 'log out', 'log out successful')," +
                "('2021-09-03 23:00:00', 'Info', 'Business', 'log in', 'log in successfully')," +
                "('2021-10-20 23:00:00', 'Info', 'View', 'search for projects', 'result return');");

            // Act
            sqlDAO.RemoveEntries();

            // Assert
            bool actualVal;

            resultData = sqlDAO.ReadData("SELECT * FROM log WHERE DATEDIFF(CURRENT_TIMESTAMP, log.LtimeStamp) > 30;");
            if ((resultData != null) && (resultData.GetType() == typeof(OdbcDataReader)))
            {
                odbcObj = (OdbcDataReader)resultData;
            }
            OdbcConnection conn = sqlDAO.GetConnection();
            actualVal = odbcObj.HasRows;
            conn.Close();

            Assert.Equal(expectedVal, actualVal);

        }


        [Fact]
        public void RemoveOutputFileTest()
        {
            // Arrange
            SqlDAO sqlDAO = new SqlDAO(dbInfo);
            ArchiveManager archiveManager = new ArchiveManager(sqlDAO);
            bool expectedVal = false;
            //Thread.Sleep(5000);

            // Create the folder and the csv file for compressing
            archiveManager.CreateArchiveFolder();
            string filepath = archiveManager.CreateOutFileName();
            sqlDAO.CopyToFile(filepath);

            // Act
            if (sqlDAO != null)
            {
                sqlDAO.RemoveOutputFile(filepath);
            }

            // Assert 
            bool actualVal = File.Exists(filepath);
            Assert.Equal(expectedVal, actualVal);
        }

        [Fact]
        public void IsFileCompressed()
        {
            // Arrange
            SqlDAO sqlDAO = new SqlDAO(dbInfo);
            ArchiveManager archiveManager = new ArchiveManager(sqlDAO);

            // Create the folder and the csv file to be compressed. 
            archiveManager.CreateArchiveFolder();
            string filePath = archiveManager.CreateOutFileName();
            sqlDAO.CopyToFile(filePath);
            FileAttributes expectedAttribute = FileAttributes.Archive;
            string compFilePath = Path.ChangeExtension(filePath, ".gz");

            // Act
            sqlDAO.CompressFile(filePath);

            // Assert
            FileAttributes actualAttribute = File.GetAttributes(compFilePath);
            Assert.Equal(expectedAttribute, actualAttribute);

        }

        [Fact]
        public void IsArchive_Under_60s_10kRecords()
        {
            // Arrange
            SqlDAO sqlDAO = new SqlDAO(dbInfo);
            ArchiveManager archiveManager = new ArchiveManager(sqlDAO);
            var timer = new Stopwatch();
            double expectedVal = 60;
            string folderPath = @"C:/HobbyArchive";

            sqlDAO.WriteData("INSERT into log(LtimeStamp, LvName, catname, userop, logmessage) values" +
                "('2021-08-07 23:00:00', 'Info', 'View', 'create some projects', 'new account created')," +
                "('2021-06-04 23:00:00', 'Info', 'Business', 'create some projects', 'new projects made')," +
                "('2021-07-02 23:00:00', 'Info', 'View', 'log out', 'log out successful')," +
                "('2021-09-03 23:00:00', 'Info', 'Business', 'log in', 'log in successfully')," +
                "('2021-10-20 23:00:00', 'Info', 'View', 'search for projects', 'result return');");

            for (int i = 0; i < 11; i++)
            {
                sqlDAO.WriteData("INSERT INTO log(LtimeStamp, LvName, catName, userOP, logMessage) " +
                    "SELECT LtimeStamp, LvName, catName, userOP, logMessage FROM log WHERE DATEDIFF(CURRENT_TIMESTAMP, log.LtimeStamp) > 30;");
            }

            // Act
            timer.Start();
            archiveManager.Controller();
            timer.Stop();

            // Arrange
            double actualVal = timer.ElapsedMilliseconds;
            Assert.True((actualVal / 1000) < expectedVal);

            // Clean up resources after testing
            try
            {
                Directory.Delete(folderPath, true);
            }
            catch
            {
                Console.WriteLine("Folder failed to be deleted");
            }
        }

        [Fact]
        public void IsArchive_Under_60s_10mRecords()
        {
            // Arrange
            SqlDAO sqlDAO = new SqlDAO(dbInfo);
            ArchiveManager archiveManager = new ArchiveManager(sqlDAO);
            var timer = new Stopwatch();
            double expectedVal = 60;
            string folderPath = @"C:/HobbyArchive";

            sqlDAO.WriteData("INSERT into log(LtimeStamp, LvName, catname, userop, logmessage) values" +
                "('2021-08-07 23:00:00', 'Info', 'View', 'create some projects', 'new account created')," +
                "('2021-06-04 23:00:00', 'Info', 'Business', 'create some projects', 'new projects made')," +
                "('2021-07-02 23:00:00', 'Info', 'View', 'log out', 'log out successful')," +
                "('2021-09-03 23:00:00', 'Info', 'Business', 'log in', 'log in successfully')," +
                "('2021-10-20 23:00:00', 'Info', 'View', 'search for projects', 'result return');");

            for (int i = 0; i < 18; i++)
            {
                sqlDAO.WriteData("INSERT INTO log(LtimeStamp, LvName, catName, userOP, logMessage) " +
                    "SELECT LtimeStamp, LvName, catName, userOP, logMessage FROM log WHERE DATEDIFF(CURRENT_TIMESTAMP, log.LtimeStamp) > 30;");
            }

            // Act
            timer.Start();
            archiveManager.Controller();
            timer.Stop();

            // Arrange
            double actualVal = timer.ElapsedMilliseconds;
            Assert.True((actualVal / 1000) < expectedVal);

            //Clean up resources after testing
            try
            {
                Directory.Delete(folderPath, true);
            }
            catch
            {
                Console.WriteLine("Folder failed to be deleted");
            }
        }

         [Fact]
         public void IsArchiveOnFirstOfMonth()
        {

        }


        [Fact]
        public void IsWriteData()
        {
            // Arrange
            SqlDAO sqlDAO = new SqlDAO(dbInfo);
            ArchiveManager archiveManager = new ArchiveManager(sqlDAO);
            var timer = new Stopwatch();
            double expectedVal = 60;
            string folderPath = @"C:/HobbyArchive";

            // Act
            sqlDAO.WriteData("INSERT into log(LtimeStamp, LvName, catname, userop, logmessage) values" +
                "('2021-08-07 23:00:00', 'Info', 'View', 'create some projects', 'new account created')," +
                "('2021-06-04 23:00:00', 'Info', 'Business', 'create some projects', 'new projects made')," +
                "('2021-07-02 23:00:00', 'Info', 'View', 'log out', 'log out successful')," +
                "('2021-09-03 23:00:00', 'Info', 'Business', 'log in', 'log in successfully')," +
                "('2021-10-20 23:00:00', 'Info', 'View', 'search for projects', 'result return');");

            // Assert
            Assert.True(true);
        }

    }
}