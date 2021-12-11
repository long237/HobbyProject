using System;

namespace TeamHobby.HobbyProjectGenerator.Main
{
    public class MainMenu
    {
        /*static void Main(string[] args)
        {
            /* ExampleDAO z = new ExampleDAO();
             z.UserData("Tomato");
             Console.Read();*/
            /*bool menu = true;

            // Set up menu loop
            while (menu == true)
            {
                // Console customization
                // Change the look of the console
                Console.Title = "HobbyProjectGenerator";
                // Change console text color
                Console.ForegroundColor = ConsoleColor.Green;
                // Change terminal height
                Console.WindowHeight = 40;
                

                // Create intial menu
                Console.WriteLine("What would you like to do?");
                int num = 1;
                Console.WriteLine(num + ".Create a new user account.");
                num += 1;
                Console.WriteLine(num + ".Acess Admin Features.");

                // Set up try-catch for invalid inputs
                try
                {
                    // Get user choice
                    string initialChoice = Console.ReadLine();
                    // Convert to integer
                    int Choice = Convert.ToInt32(initialChoice);

                    switch (Choice)
                    {
                        // Create a new account
                        case 1:
                            // Get username
                            Console.WriteLine("Please enter a username:");
                            string userName = Console.ReadLine();
                            
                            // Get Password
                            Console.WriteLine("Please enter a password:");
                            string userPassword = Console.ReadLine();

                            // Create bool value for password confirm loop
                            bool conPsswrd = true;
                            // Loop until password is confirmed
                            while (conPsswrd == true)
                            {
                                // Confirm Password
                                Console.WriteLine("Please re-enter the password:");
                                string checkPsswd = Console.ReadLine();
                                // Check if passwords match
                                if (userPassword == checkPsswd)
                                {
                                    // Get Security question for password reset
                                    Console.WriteLine("Please enter a security question.\n" +
                                        "(EX: What is your favorite food?");
                                    string SecQuest = Console.ReadLine();
                                    // Get Security question answer
                                    Console.WriteLine("Please enter the answer for your security question:");
                                    String SecAnswer = Console.ReadLine();

                                    // Call user manager method to create the new user
                                    //int userCreateConfirm = new CreateUser(userName,userPassword,SecQuest,SecAnswer);

                                    // Check if user creation was successful
                                   /* if (userCreateConfirm = 1)
                                    {

                                        // Confirm to user that the account has been created
                                        Console.WriteLine("Account created succesfully with the username of" + userName);
                                    }
                                    else
                                    {

                                    }*/
                                  /*  conPsswrd = false;
                                }
                                else
                                {
                                    Console.WriteLine("Passwords did not match, please try again.");
                                }
                            }
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
                            Console.WriteLine("3.");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");


                            break;
                        default:
                            menu = false;
                            break;
                    };
                }
                // Catch invalid keys such as spamming enter
                catch
                {
                    menu = false;
                };
            }*/

        //}
    }
}
