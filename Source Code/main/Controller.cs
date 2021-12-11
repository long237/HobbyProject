using main;
using System;


namespace TeamHobby.HobbyProjectGenerator.Main
{
    public class GetCredentials
    {
        public string? GetUserName()
        {
            Console.WriteLine("Please enter a username:");
            string? userName = Console.ReadLine();
            return userName;
        }
        public string? GetPassword()
        {
            Console.WriteLine("Please enter a password:");
            string? userPassword = Console.ReadLine();
            return userPassword;
        }
    }
    public class Controller 
    {

        public static void Main(string[] args)
        {
            GetCredentials credentials = new GetCredentials();
            string? username = credentials.GetUserName();
            string? password = credentials.GetPassword();
            
                       
            Console.WriteLine(value: $"username is {username}\npassword is {password}");
            
            /* ExampleDAO z = new ExampleDAO();
             z.UserData("Tomato");
             Console.Read();*/
            /*bool MainMenu = true;

            // Set up menu loop
            while (MainMenu == true)
            {
                // Console customization
                // Change the look of the console
                Console.Title = "HobbyProjectGenerator";
                // Change console text color
                Console.ForegroundColor = ConsoleColor.Green;
                // Change terminal height
                Console.WindowHeight = 40;


                // Create class objects
                UiPrint menu = new UiPrint();
                UserAccount user = new UserAccount();


                // Print main menu
                menu.InitialMenu();

                // Set up try-catch for invalid inputs
                try
                {
                    // Get user choice
                    string initialChoice = Console.ReadLine();
                    // Convert to integer
                    int Choice = Convert.ToInt32(initialChoice);

                    switch (Choice)
                    {
                        case 0:
                            MainMenu = false;
                            break;
                        // Create a new account
                        case 1:
                               user.newUser();
                            break;
                        // Access Admin features
                        case 2:
                            // Ask for Admin login credentials
                            Console.WriteLine("Please enter a username:");
                            string AdminUser = Console.ReadLine();
                            Console.WriteLine("Please enter the password for" + AdminUser);
                            string AdminPsswrd = Console.ReadLine();

                            // Check if the username and password match a record within the administrators


                            // Show new administrator menu
                            int AdminNum = 0;
                            Console.WriteLine("Welcome" + AdminUser);
                            Console.WriteLine("What would you like to do?");
                            Console.WriteLine("1.View normal user records.");
                            Console.WriteLine("2.View Administrator user records.");
                            Console.WriteLine("3.View log files.");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");


                            break;
                        default:
                            Console.WriteLine("Invalid choice, please enter a valid number.");
                            break;
                    };
                }
                // Catch invalid keys such as spamming enter
                catch
                {
                    MainMenu = false;
                };
            }*/

        }
    }
}
