using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeamHobby.HobbyProjectGenerator;
using TeamHobby.HobbyProjectGenerator.Implementations;

namespace TeamHobby.UserManagement.Tests
{
    [TestClass]
    public class InMemeoryUserServiceShould
    {
        [TestMethod]
        public void GetNoLogs()
        {
            // Triple A Format
            // Arrange
            var userService = new InMemoryUserService();
            var expectedCount = 0;

            // Act
            var actualFetch = userService.GetAllUsers();

            // Assert
            // This is the best format
            Assert.IsTrue(actualFetch.Count == expectedCount);

        }

        [TestMethod]
        public void AllowValidUserInput()
        {
            // Triple A Format
            // Arrange
            var userService = new InMemoryUserService();
            var expectedCount = 1;
            var expectedUserMessage = "Test log entry";

            // Act
            var actual = userService.User("Test log entry");
            var actualFetch = userService.GetAllUsers();

            // Assert
            // This is the best format
            Assert.IsTrue(actualFetch.Count == expectedCount);
            Assert.IsTrue(actualFetch[0].Contains(expectedUserMessage));

        }

        /*
        [TestMethod]
        public void Users()
        {
            // Triple A Format
            // Arrange
            var inMemoryUserService = new InMemoryUserService();
            var expected = true;

            // Act
            var actual = inMemoryUserService.User("Potato");


            // Assert
            // This is the best format
            Assert.IsTrue(expected == actual);
            // Alternate is
            //Assert.AreEqual(expected, actual);

        }
        */
    }
}