using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // Create intial menu
            Console.WriteLine("What would you like to do?");
            int num = 1;
            Console.WriteLine(num + ".Create a new account.");
            num += 1;
            Console.WriteLine(num + ".Access Admin Features");
        }
    }
}
