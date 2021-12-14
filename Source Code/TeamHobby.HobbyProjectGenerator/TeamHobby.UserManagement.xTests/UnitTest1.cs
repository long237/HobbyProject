using System;
using Xunit;
using Xunit.Abstractions;
using System.Threading;
using TeamHobby.HobbyProjectGenerator.UserManagement;
using TeamHobby.HobbyProjectGenerator.DataAccess;

namespace TeamHobby.UserManagement.xTests
{
    public class UnitTest1
    {
        
        ITestOutputHelper output;
        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;  
        }/*
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
                "PASSWORD=hobby;" +
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
                Assert.False(false);
                output.WriteLine("Greater than 5 seconds");
                
            }
            else 
            {
                Assert.True(true);
                output.WriteLine("Less than 5 seconds");
            }
        }
        */
        [Fact]
        public void BulkOperationforSixty()
        {
            // Arrange
            DateTime sTime = DateTime.Now;
            
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

            // Act
            // Create a file
            for (int i = 0; i < 10000; i++)
            {
                var TestAcc = new UserAccount("newUser" + $"{i}", "4567", $"email{i}@a.com", "regular", sTime);
                serviceTest.CreateUserRecord(TestAcc, "Rifat", datasource);
            }
            
            DateTime eTime = DateTime.Now;
            TimeSpan timeDiff = (eTime - sTime);
            var sec = timeDiff.TotalSeconds;


            // Assert
            if (sec > 60)
            {
                Assert.False(false);
                output.WriteLine("Greater than 60 seconds");

            }
            else
            {
                Assert.True(true);
                output.WriteLine("Less than 60 seconds");
            }
        }

    }
}