using System;
using Xunit;
using Xunit.Abstractions;
using System.Threading;
using TeamHobby.HobbyProjectGenerator.UserManagement;
using TeamHobby.HobbyProjectGenerator.DataAccessLayer;
using System.IO;

namespace TeamHobby.UserManagement.xTests
{
    public class UseerManagementTests
    {
        
        ITestOutputHelper output;
        public UseerManagementTests(ITestOutputHelper output)
        {
            this.output = output;  
        }
        // User is able to perform any single UM operation within 5 seconds upon invocation.A system message displays “UM operation was successful”
        [Fact]
        public void SingleOperationinFiveSec() 
        {
            // Arrange
            DateTime sTime = DateTime.Now;
            var TestAcc = new UserAccount("newUser", "4567", "email@a.com", "regular", sTime);
            var serviceTest = new AccountService();
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

            // Act
            // Create a file
            serviceTest.CreateUserRecord(TestAcc, "Rifat", datasource);
            DateTime eTime = DateTime.Now;
            TimeSpan timeDiff = (eTime - sTime);
            var sec = timeDiff.TotalSeconds;

            // Assert
            if (sec > 5)
            {                
                output.WriteLine("Single Operation was unsuccessful");
                Assert.True(false);
            }
            else 
            {
                Assert.True(true);
                output.WriteLine("Single Operation was successful");
            }
        }

        // Single UM operation takes longer than 5 seconds
        [Fact]
        public void FailSingleOperationinFiveSec()
        {
            // Arrange
            DateTime sTime = DateTime.Now;
            var TestAcc = new UserAccount("newUser", "4567", "email@a.com", "regular", sTime);
            var serviceTest = new AccountService();
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

            // Act
            // Create a file
            serviceTest.CreateUserRecord(TestAcc, "Rifat", datasource);
            Thread.Sleep(6000);
            DateTime eTime = DateTime.Now;
            TimeSpan timeDiff = (eTime - sTime);
            var sec = timeDiff.TotalSeconds;

            // Assert
            if (sec > 5)
            {
                output.WriteLine("Single Operation was unsuccessful");
                Assert.False(false);
            }
            else
            {
                Assert.True(false);
                output.WriteLine("Single Operation was successful");
            }
        }
        // Single UM operation completes within 5 seconds, but no system message is shown or inaccurate system message is shown
        [Fact]
        public void SingleOperationwithMessage()
        {
            // Arrange
            DateTime sTime = DateTime.Now;
            var TestAcc = new UserAccount("newUserMessage", "4567", "email@a.com", "regular", sTime);
            var serviceTest = new AccountService();
            string dbType = "sql";
            RDSFactory dbFactory = new RDSFactory();
            var checkMess = false;
            // Testing Data Access Layer
            string dbInfo = "DRIVER={MariaDB ODBC 3.1 Driver};" +
                "TCPIP=1;" +
                "SERVER=localhost;" +
                "DATABASE=hobby;" +
                "UID=root;" +
                "PASSWORD=Teamhobby;" +
                "OPTION=3";
            IDataSource<string> datasource = dbFactory.getDataSource(dbType, dbInfo);

            // Act
            // Create a file
            serviceTest.CreateUserRecord(TestAcc, "Rifat", datasource);
            DateTime eTime = DateTime.Now;
            TimeSpan timeDiff = (eTime - sTime);
            var sec = timeDiff.TotalSeconds;

            // Assert
            if (sec > 5)
            {
                var message = "Single Operation was unsuccessful";
                Assert.False(true);
                output.WriteLine(message);
                if (checkMess)
                {
                    Assert.False(true);
                    throw new Exception("Message was not printed");
                }
            }
            else
            {
                Assert.True(true);
                var message2 = "Single Operation was successful";
                output.WriteLine(message2);
                if (!checkMess)
                {
                    output.WriteLine("Message was printed");
                    Assert.True(true);
                }
            }
        }
        // Bulk UM operations completes within 60 seconds, but no system message is shown or inaccurate system message is shown
        [Fact]
        public void BulkOperationwithMessage()
        {
            // Arrange
            DateTime sTime = DateTime.Now;
            var checkMess = false;
            var serviceTest = new AccountService();
            string dbType = "sql";
            RDSFactory dbFactory = new RDSFactory();
            string path = Directory.GetCurrentDirectory();


            // Testing Data Access Layer
            string dbInfo = "DRIVER={MariaDB ODBC 3.1 Driver};" +
                "TCPIP=1;" +
                "SERVER=localhost;" +
                "DATABASE=hobby;" +
                "UID=root;" +
                "PASSWORD=Teamhobby;" +
                "OPTION=3";
            IDataSource<string> datasource = dbFactory.getDataSource(dbType, dbInfo);

            // Act
            // Create a file
            //for (int i = 0; i < 10000; i++)
            //{
                //var TestAcc = new UserAccount("newUser" + $"{i}", "4567", $"email{i}@a.com", "regular", sTime);
            serviceTest.BulkOperation("Rifat", path + "\\Bulk.txt", datasource);
            //}

            DateTime eTime = DateTime.Now;
            TimeSpan timeDiff = (eTime - sTime);
            var sec = timeDiff.TotalSeconds;


            // Assert
            if (sec > 60)
            {
                var message = "Bulk Operation was unsuccessful";
                Assert.True(false);
                output.WriteLine(message);
                if (checkMess)
                {
                    Assert.True(false);
                    throw new Exception("Message was not printed");
                }
            }
            else
            {
                Assert.True(true);
                var message2 = "Bulk Operation was successful";
                output.WriteLine(message2);
                if (!checkMess)
                {
                    output.WriteLine("Message was printed");
                    Assert.True(true);
                }
            }
        }
        // User is able to perform less than 10K UM operations in bulk within 60 seconds. A system message displays “Bulk UM operation was successful”
        [Fact]
        public void BulkOperationforSixty()
        {
            // Arrange
            DateTime sTime = DateTime.Now;

            var serviceTest = new AccountService();
            string dbType = "sql";
            RDSFactory dbFactory = new RDSFactory();
            string dir = Directory.GetCurrentDirectory();
            string path = dir + "\\Bulk.txt";
            output.WriteLine(path);


            // Testing Data Access Layer
            string dbInfo = "DRIVER={MariaDB ODBC 3.1 Driver};" +
                "TCPIP=1;" +
                "SERVER=localhost;" +
                "DATABASE=hobby;" +
                "UID=root;" +
                "PASSWORD=Teamhobby;" +
                "OPTION=3";
            IDataSource<string> datasource = dbFactory.getDataSource(dbType, dbInfo);

            // Act
            // Create a file
            //for (int i = 0; i < 10000; i++)
            //{
            //var TestAcc = new UserAccount("newUser" + $"{i}", "4567", $"email{i}@a.com", "regular", sTime);
            serviceTest.BulkOperation("Rifat", path, datasource);
            //}

            DateTime eTime = DateTime.Now;
            TimeSpan timeDiff = (eTime - sTime);
            var sec = timeDiff.TotalSeconds;


            // Assert
            if (sec > 60)
            {
                output.WriteLine("Bulk Operation was unsuccessful");
                Assert.True(false);
            }
            else
            {
                Assert.True(true);
                output.WriteLine("Bulk Operation was successful");
            }
        }
    }
}