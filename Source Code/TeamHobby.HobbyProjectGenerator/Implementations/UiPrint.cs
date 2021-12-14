using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamHobby.HobbyProjectGenerator.UserManagement;

// Print Statements used throughout the program.

namespace TeamHobby.HobbyProjectGenerator
{
    public class UiPrint
    {
        public void InitialMenu()
        {
            // Create intial menu
            Console.WriteLine("What would you like to access?");
            int num = 1;
            Console.WriteLine(num + ".User Management");
            num += 1;
            Console.WriteLine(num + ".Logging");
        }
        public void SystemAccountMenu()
        {
            UserAccount user = new UserAccount();
            // Menu for all UserManagement options
            int menu = 0;
            Console.WriteLine($"Welcome {user.username} to User Management.\n");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine((menu + 1) + ". Create a new account.");
            Console.WriteLine((menu + 1) + ". Edit an account.");
            Console.WriteLine((menu + 1) + ". Delete an account.");
            Console.WriteLine((menu + 1) + ". Disable an account.");
            Console.WriteLine((menu + 1) + ". Enable an account.");
        }
    }
}
