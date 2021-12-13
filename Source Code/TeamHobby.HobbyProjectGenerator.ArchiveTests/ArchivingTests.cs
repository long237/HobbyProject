using System;
using System.Collections.Generic;
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
        public void CreateFolderTest(IDataSource<string> sqlDAO)
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
        public void CreateFileNameTest(IDataSource<string> sqlDAO)
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

        public void FolderCreationFailed()
        {

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
            test.CreateFolderTest(sqlDAO);
            Console.WriteLine("-----------------");
            Console.WriteLine("");

            // Testing file creation
            test.CreateFileNameTest(sqlDAO);
            Console.WriteLine("-----------------");
            Console.WriteLine("");


        }

    }

}
