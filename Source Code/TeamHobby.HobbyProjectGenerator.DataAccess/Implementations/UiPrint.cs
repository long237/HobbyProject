// Print Statements used throughout the program.

namespace TeamHobby.HobbyProjectGenerator.DataAccess
{
    public class UiPrint
    {
        public void UserManagementMenu(string username)
        {
            // File must be within the main folder path
            // Get the current directory.
            string path = Directory.GetCurrentDirectory();
            // Menu for all UserManagement options
            int menu = 0;
            Console.WriteLine("-------------------------------------\n");
            Console.WriteLine($"\nWelcome {username} to User Management.\n");
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
            Console.WriteLine(menu + $") Bulk operation from file within the directory.\n{path}\\BulkOps\n");
            menu += 1;
            Console.WriteLine(menu + ") View log path.\n");
            menu += 1;
            Console.WriteLine(menu + ") View archive path.\n");
        }
    }
}
