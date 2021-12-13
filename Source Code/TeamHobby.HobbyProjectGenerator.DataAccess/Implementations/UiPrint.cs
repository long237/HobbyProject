// Print Statements used throughout the program.

namespace TeamHobby.HobbyProjectGenerator.DataAccess
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
        public void UserManagementMenu(string username)
        {
            // Menu for all UserManagement options
            int menu = 0;
            Console.WriteLine($"Welcome {username} to User Management.\n");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine(menu + ") To exit User Management.\n");
            menu += 1;
            Console.WriteLine(menu + ") Create a new account.\n");
            menu += 1;
            Console.WriteLine(menu + ") Edit an account.\n");
            menu += 1;
            Console.WriteLine(menu + ") Delete an account.\n");
            menu += 1;
            Console.WriteLine(menu + ") Disable an account.\n");
            menu += 1;
            Console.WriteLine(menu + ") Enable an account.\n");
            menu += 1;
            Console.WriteLine(menu + ") View logs.\n");
            menu += 1;
            Console.WriteLine(menu + ") View Archive.\n");
        }
    }
}
