using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using TeamHobby.HobbyProjectGenerator.UserManagement;
using TeamHobby.HobbyProjectGenerator.DataAccess;

namespace TeamHobby.HobbyProjectGenerator.UserManagement
{
    public class SystemAccountManager
    {
        public string IsInputValid(string checkUN, string checkPWD)
        {
            // Create bool variables to check if username and password are valid
            bool validUN = checkUN.All(un=>Char.IsLetterOrDigit(un) || un=='@' 
            || un == '.' || un == ',' || un == '!');

            bool validPwd = checkPWD.All(Char.IsLetterOrDigit);


            // Check if any are empty
            if (checkUN == null || checkPWD == null)
            {
                return "Invalid input\n";
            }
            // Check if username is within the restricted length
            else if (checkUN.Length > 15 || checkUN.Length <= 0
                || validUN is false)
            {
                return "Invalid Username\n";
            }
            // Check if password is within the restricted length
            else if (checkPWD.Length > 18 || checkPWD.Length <= 0
                || validPwd is false)
            {
                return "Invalid Password\n";
            }
            // Check if username and password are valid
            else if (checkUN.Length <= 15 && checkUN.Length > 0
                && validUN is true && checkPWD.Length <= 18
                && checkPWD.Length > 0 && validPwd is true)
            {
                return "Username and password is valid.\n";
            }
            // Check if username and password and email are valid
            else if (checkUN.Length <= 15 && checkUN.Length > 0
                && validUN is true && checkPWD.Length <= 18
                && checkPWD.Length > 0 && validPwd is true)
            {
                return "Username and password is valid.\n";
            }
            // For testing to make sure username is valid
            else if (checkUN.Length <= 15 && checkUN.Length > 0
                && validUN is true)
            {
                return "Valid Username\n";
            }
            // For testing to make sure password is valid
            else if (checkPWD.Length <= 18 && checkPWD.Length > 0
                && validPwd is true)
            {
                return "Valid Password\n";
            }
            else
            {
                return "Invalid input\n";
            }
        }
        public string IsInputValid(string checkUN, string checkPWD, string email, string role)
        {
            // Create bool variables to check if username and password and email are valid
            bool validUN = checkUN.All(un => Char.IsLetterOrDigit(un) || un == '@'
            || un == '.' || un == ',' || un == '!');

            bool validPwd = checkPWD.All(Char.IsLetterOrDigit);

            bool validEmail = email.All(email => Char.IsLetterOrDigit(email) && email == '@'
            && email == '.');

            bool validRole = role.All(role => Char.IsLetter(role));

            // Check if any are empty
            if (checkUN == null || checkPWD == null || email == null)
            {
                return "Invalid input\n";
            }
            // Check if username is within the restricted length
            else if (checkUN.Length > 15 || checkUN.Length <= 0
                || validUN is false)
            {
                return "Invalid Username\n";
            }
            // Check if password is within the restricted length
            else if (checkPWD.Length > 18 || checkPWD.Length <= 0
                || validPwd is false)
            {
                return "Invalid Password\n";
            }
            // Check if email is within the restricted length
            else if (email.Length > 18 || email.Length <= 0
               || validEmail is false)
            {
                return "Invalid Email.\n";
            }
            // Check if username and password are valid
            else if (checkUN.Length <= 15 && checkUN.Length > 0
                && validUN is true && checkPWD.Length <= 18
                && checkPWD.Length > 0 && validPwd is true)
            {
                return "Username and password is valid.\n";
            }
            // Check if username and password and email are valid
            else if (checkUN.Length <= 15 && checkUN.Length > 0
                && validUN is true && checkPWD.Length <= 18
                && checkPWD.Length > 0 && validPwd is true
                && email.Length > 18 && email.Length <= 0
                && validEmail is true)
            {
                return "Username and password is valid.\n";
            }
            // For testing to make sure username is valid
            else if (checkUN.Length <= 15 && checkUN.Length > 0
                && validUN is true)
            {
                return "Valid Username\n";
            }
            // For testing to make sure password is valid
            else if (checkPWD.Length <= 18 && checkPWD.Length > 0
                && validPwd is true)
            {
                return "Valid Password\n";
            }
            // For testing to make sure email is valid
            else if (email.Length > 18 && email.Length <= 0
                && validEmail is true)
            {
                return "Valid Password\n";
            }
            else
            {
                return "Invalid input\n";
            }
        }
        // Check with database if user is an admin
        public bool isAdmin(UserAccount user, IDataSource<string> dbSource)
        {
            // string checkAdmin = $"Select * from users where username = {user.username} and password = {user.password};";
            string checkAdmin = $"select r.Role from roles r, users u where " +
                $"UserName = '{user.username}' and Password = '{user.password}' and r.RoleID = u.RoleID;";
            Object confirmAdmin = dbSource.ReadData(checkAdmin);
            //Console.WriteLine("type of Reesult:" + confirmAdmin.GetType());
            OdbcDataReader reader = null;

            if (confirmAdmin.GetType() == typeof(OdbcDataReader))
            {
                reader = (OdbcDataReader)confirmAdmin;
            }

            // Create String to hold sql output
            string checkSql = "";

            // Read Sql query results
            while (reader.Read())
            {
                checkSql = reader.GetString(0);
            }
            
            SqlDAO sqlDS = (SqlDAO)dbSource;
            Console.WriteLine("");

            // Closing the connection
            sqlDS.getConnection().Close();

            if (checkSql == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string CreateUserRecord(UserAccount user, IDataSource<string> dbSource)
        {
            // Check Login inputs
            IsInputValid(user.username, user.password);

            bool Admin = isAdmin(user, dbSource);

            // Give access if the user is and Admin
            if (Admin is false)
            {
                return "Access Denied: Unauthorized\n";
            }
            else
            {
                // db.users layout (UserName, Password, RoleID, IsActive, CreatedBy, CreatedDate)
                // db.roles layout (RoleID(AutoGen), Role, CreatedBy, CreatedDate)
                // Create UiPrint Object
                UiPrint ui = new UiPrint();
                // Create Account Service object
                AccountService accountService = new AccountService();
                // Create credentials object for new inptus
                GetCredentials newCredentials = new GetCredentials();
                // Print User Management menu
                ui.UserManagementMenu(user.username);
                // Create bool object for menu loop
                bool menuLoop = true;
                // Create loop for menu
                while (menuLoop is true) {
                    // Get user choice
                    int menuChoice = Convert.ToInt32(Console.ReadLine());
                    // Complete the appropriate action
                    switch (menuChoice)
                    {
                        // Exit menu
                        case 0:
                            return "Exiting UserManagement.\n";
                            break;
                        // Create account 
                        case 1:
                            UserAccount newUser = new UserAccount(newCredentials.GetUserName(),
                            newCredentials.GetPassword(), newCredentials.GetEmail(), 
                                newCredentials.GetRole(), DateTime.UtcNow);
                            accountService.CreateUserRecord(newUser, dbSource);
                            break;
                        // Edit account
                        case 2:
                            UserAccount newEditUser = new UserAccount(newCredentials.GetUserName(),
                                newCredentials.GetPassword(), DateTime.UtcNow);
                            accountService.EditUserRecord(newEditUser, dbSource);
                            break;
                        // Delete account
                        case 3:
                            UserAccount newDeleteUser = new UserAccount(newCredentials.GetUserName(),
                                newCredentials.GetPassword(), DateTime.UtcNow);
                            accountService.DeleteUserRecord(newDeleteUser, dbSource);
                            break;
                        // Disable account
                        case 4:
                            UserAccount newDisableUser = new UserAccount(newCredentials.GetUserName(),
                                newCredentials.GetPassword(), DateTime.UtcNow);
                            accountService.DisableUser(newDisableUser, dbSource);
                            break;
                        // Enable account
                        case 5:
                            UserAccount newEnableUser = new UserAccount(newCredentials.GetUserName(),
                                newCredentials.GetPassword(), DateTime.UtcNow);
                            accountService.EnableUser(newEnableUser, dbSource);
                            break;
                        // View logs
                        case 6:
                            break;
                        // View archive
                        case 7:
                            break;
                        default:
                            Console.WriteLine("Invalid input.\nPlease enter a valid option.\n");
                            break;
                    }
                }
                
                string dbAction = user.NewUserName;
                return dbAction;
            }
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

            
        }

    }
}
