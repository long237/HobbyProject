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
                $"UserName = '{user.username}' and Password = '{user.password}' and r.Role = u.Role;";
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
            sqlDS.GetConnection().Close();

            if (checkSql == "Admin")
            {
                Console.WriteLine("-- Logged in successfully.");
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
                // Create bool object for menu loop
                bool menuLoop = true;
                // Create loop for menu
                while (menuLoop is true) {
                    // Print User Management menu
                    ui.UserManagementMenu(user.username);
                    // Get user choice
                    int menuChoice = Convert.ToInt32(Console.ReadLine());
                    // Complete the appropriate action
                    switch (menuChoice)
                    {
                        // Exit menu
                        case 0:
                            return "Back to Login";
                        // Create account 
                        case 1:
                            // Get all credentials and create newUser
                            UserAccount newUser = new UserAccount(newCredentials.GetUserName(),
                            newCredentials.ConfirmPassword(), newCredentials.GetEmail(), 
                                newCredentials.GetRole(), DateTime.UtcNow);           
                            bool accountValid = accountService.CreateUserRecord(newUser, user.username, dbSource);
                            if (accountValid is true)
                            {
                                Console.WriteLine("\nAccount created Successfully");
                                break;
                            }
                            else
                            {
                                return "Database Timed out";
                            }
                        // Edit account
                        case 2:
                            // State what account is being edited
                            string userName = newCredentials.GetUserName();
                            string userRole = newCredentials.GetRole();

                            // Notify the user of what can be edited
                            Console.WriteLine($"\n****The following information will be used to update {userName}");

                            // Get updated parameters
                            UserAccount newEditUser = new UserAccount(userName,
                                newCredentials.GetPassword(), newCredentials.GetEmail(), 
                                newCredentials.GetRole(), DateTime.UtcNow);
                            bool editValid = accountService.EditUserRecord(newEditUser, user.username, dbSource);
                            if (editValid is true)
                            {
                                Console.WriteLine("\nAccount updated Successfully");
                                break;
                            }
                            else
                            {
                                return "Database Timed out";
                            }
                            break;
                        // Delete account
                        case 3:
                            UserAccount newDeleteUser = new UserAccount(newCredentials.GetUserName(),
                                newCredentials.GetPassword(), DateTime.UtcNow);
                            bool deleteValid = accountService.DeleteUserRecord(newDeleteUser, user.username , dbSource);
                            if (deleteValid is true)
                            {
                                Console.WriteLine("\nAccount deleted Successfully");
                                break;
                            }
                            else
                            {
                                return "User does not exist";
                            }
                            break;
                        // Disable account
                        case 4:
                            UserAccount newDisableUser = new UserAccount(newCredentials.GetUserName(),
                                newCredentials.GetRole());
                            bool disableValid = accountService.DisableUser(newDisableUser, user.username , dbSource);
                            if (disableValid is true)
                            {
                                Console.WriteLine("\nAccount disabled Successfully");
                                break;
                            }
                            else
                            {
                                return "Database Timed out";
                            }
                            break;
                        // Enable account
                        case 5:
                            UserAccount newEnableUser = new UserAccount(newCredentials.GetUserName(),
                                newCredentials.GetRole());
                            bool enableValid = accountService.EnableUser(newEnableUser, user.username , dbSource);
                            if (enableValid is true)
                            {
                                Console.WriteLine("\nAccount enabled Successfully");
                                break;
                            }
                            else
                            {
                                return "Database Timed out";
                            }
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
                return "Back to Login";
            }
        }    
    }
}
