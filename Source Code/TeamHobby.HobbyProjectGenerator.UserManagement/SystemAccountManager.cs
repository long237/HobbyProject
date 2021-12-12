using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamHobby.HobbyProjectGenerator.UserManagement;
using TeamHobby.HobbyProjectGenerator.Implementations;

namespace TeamHobby.HobbyProjectGenerator.UserManagement
{
    public class SystemAccountManager
    {
        public string IsInputValid(string checkUN, string checkPWD)
        {
            // Create bool variables to check if username and password are valid
            bool ValidUN = checkUN.All(un=>Char.IsLetterOrDigit(un) || un=='@' 
            || un == '.' || un == ',' || un == '!');

            bool ValidPwd = checkPWD.All(Char.IsLetterOrDigit);


            if (checkUN == null || checkPWD == null)
            {
                return "Invalid input\n";
            }
            else if (checkUN.Length > 15 || checkUN.Length <= 0
                || ValidUN is false)
            {
                return "Invalid Username\n";
            }
            else if (checkPWD.Length > 18 || checkPWD.Length <= 0
                || ValidPwd is false)
            {
                return "Invalid Password\n";
            }
            else if (checkUN.Length <= 15 && checkUN.Length > 0
                || ValidUN is true && checkPWD.Length <= 18 
                && checkPWD.Length > 0 || ValidPwd is true)
            {
                return "Username and password is valid.\n";
            }
            else if (checkUN.Length <= 15 && checkUN.Length > 0
                || ValidUN is true)
            {
                return "Valid Username\n";
            }
            else if (checkPWD.Length <= 18 && checkPWD.Length > 0
                || ValidPwd is true)
            {
                return "Valid Password\n";
            }
            else
            {
                return "Invalid Input\n";
            }
        }
        public bool isAdmin()
        {
            return false;
        }
        public void CreateUserRecord(UserAccount user)
        {
            Console.Write(IsInputValid(user.username, user.password));

        }



        /*public NewUserName()
        {

        }
        public NewPassword()
        {
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
                    /*if (userCreateConfirm = 1)
                    {

                        // Confirm to user that the account has been created
                        Console.WriteLine("Account created succesfully with the username of" + userName);
                    }
                    else
                    {

                    }*/
                    /*conPsswrd = false;
                }
                else
                {
                    Console.WriteLine("Passwords did not match, please try again.");
                }
            }
            return true;
        }*/
    
        public void AccountController()
        {
            // Create objects
            //UserAccount user = new UserAccount();
            UiPrint ui = new UiPrint();
            
            bool foo = true;

            while (foo == true)
            {
                // sub menu
                ui.SystemAccountMenu();
                // 

            }

        }

    }
}
