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
                return "Invalid input\n";
            }
        }
        // Check with database if user is an admin
        public bool isAdmin(UserAccount user, IDataSource<string> dbSource)
        {
            // select r.Role from roles r, users u where UserName = '{user.username}' and Password = '{user.password}' and r.RoleID = u.RoleID;
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
                        case 0:
                            return "Exiting UserManagement.\n";
                            break;
                        case 1:
                            break;
                        default:
                            Console.WriteLine("Invalid input.\nPlease enter a valid option.\n");
                            break;
                    }
                }



                // Notify user of new credentials to be input
                Console.WriteLine("Please enter the new user information:\n");
                GetCredentials credentials = new GetCredentials();


                
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
