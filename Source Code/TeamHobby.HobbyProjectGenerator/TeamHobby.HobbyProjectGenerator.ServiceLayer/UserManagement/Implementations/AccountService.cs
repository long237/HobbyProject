using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using TeamHobby.HobbyProjectGenerator.DataAccessLayer;

namespace TeamHobby.HobbyProjectGenerator.ServiceLayer
{ 
    public class AccountService
    {
        public bool CreateUserRecord(UserAccount newUser, string CreatedBy, IDataSource<string> dbSource)
        {
            try
            {
                // Insert into users table
                if (newUser.email == null || newUser.email == "NULL")
                {
                    string sqlUser = $"INSERT INTO users (UserName, Password, Role, IsActive," +
                        $"CreatedBy, CreatedDate, Email) VALUES ('{newUser.username}', " +
                        $"'{newUser.password}','{newUser.role}', 1," +
                        $"'{CreatedBy}', NOW(),{newUser.email});";

                    bool insertNewUser = dbSource.WriteData(sqlUser);
                    return insertNewUser;
                }
                else
                {
                    string sqlUser = $"INSERT INTO users (UserName, Password, Role, IsActive," +
                        $"CreatedBy, CreatedDate, Email) VALUES ('{newUser.username}', " +
                        $"'{newUser.password}','{newUser.role}', 1," +
                        $"'{CreatedBy}', NOW(),'{newUser.email}');";

                    bool insertNewUser = dbSource.WriteData(sqlUser);
                    return insertNewUser;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool EditUserRecord(UserAccount editUser, string CreatedBy, IDataSource<string> dbSource)
        {
            try
            {
                // Insert into users table
                if (editUser.email == null || editUser.email == "NULL")
                {
                    string sqlUser = $"UPDATE users t SET t.Password = '{editUser.password}', " +
                          $"t.Role  = '{editUser.role}',t.Email = '{editUser.email}' " +
                          $"WHERE t.UserName = {editUser.username};";

                    bool updateNewUser = dbSource.UpdateData(sqlUser);
                    return updateNewUser;
                }
                else
                {
                    string sqlUser = $"UPDATE users t SET t.Password = '{editUser.password}', " +
                        $"t.Role  = '{editUser.role}',t.Email = '{editUser.email}' " +
                        $"WHERE t.UserName = '{editUser.username}';";

                    bool updateNewUser = dbSource.UpdateData(sqlUser);
                    return updateNewUser;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteUserRecord(UserAccount deleteUser, string CreatedBy, IDataSource<string> dbSource)
        {
            try
            {
                // Insert into users table
                string sqlUser = $"DELETE from users WHERE UserName = '{deleteUser.username}' " +
                    $"and Password = '{deleteUser.password}';";

                bool deleteNewUser = dbSource.DeleteData(sqlUser);
                return deleteNewUser;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DisableUser(UserAccount disableUser, string CreatedBy, IDataSource<string> dbSource)
        {
            try
            {
                // Insert into users table
                string sqlUser = $"UPDATE users u SET u.IsActive = 0 WHERE u.UserName = '{disableUser.username}'" +
                     $"and u.Role = '{disableUser.role}';";

                bool disableNewUser = dbSource.UpdateData(sqlUser);
                return disableNewUser;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool EnableUser(UserAccount enableUser, string CreatedBy, IDataSource<string> dbSource)
        {
            try
            {
                // Insert into users table
                string sqlUser = $"UPDATE users u SET u.IsActive = 1 WHERE u.UserName = '{enableUser.username}'" +
                    $"and u.Role = '{enableUser.role}';";

                bool disableNewUser = dbSource.UpdateData(sqlUser);
                return disableNewUser;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool BulkOperation(string signedUser, string path, IDataSource<string> dbSource)
        {
            try
            {
                // Get the current directory.
                //string path = Directory.GetCurrentDirectory();
                // Open file reader stream
                using StreamReader fileRead = new(path); // path +  + "\\BulkOps\\Bulk.txt"
                // Show location of the file being written
                //Console.WriteLine(path);

                // List choices of operations
                List<string> ops = new List<string> {"Create Account", "Edit Account",
                           "Delete Account", "Disable Account", "Enable Account"};

                // List to hold file operation and credentials
                List<string> fileList = new List<string>();
                string operation = "";
                string UN = "";
                string PWD = "";
                string Role = "";
                string Email = "";

                // Loop through file rows
                while (!fileRead.EndOfStream)
                {
                    // Create variable to hold current line
                    string line = fileRead.ReadLine();

                    // Check if line is not empty and contains the separator
                    if (line != null && line.Contains(":"))
                    {
                        // Assign value to hold current line parameter
                        string value = line.Split(':')[1].Trim();

                        // Check what operation is being called
                        if (ops.Contains(value))
                        {
                            // Add operation into temp
                            //Console.WriteLine("Found operation");
                            //Console.WriteLine(value);
                            operation = value;
                        }
                        else
                        {
                            string checkCredential = line.Split(':')[0].Trim();
                            //Console.WriteLine("Found credential");
                            switch (checkCredential)
                            {
                                case "Username":
                                    UN = value;
                                    break;
                                case "Password":
                                    PWD = value;
                                    break;
                                case "Role":
                                    Role = value;
                                    break;
                                case "Email":
                                    Email = value;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if(line != null)
                    {
                        switch (operation)
                        {
                            case "Create Account":
                                //Console.WriteLine(operation);
                                if (!String.IsNullOrEmpty(UN))
                                {
                                    // Create UserAccount object
                                    UserAccount user = new UserAccount(UN, PWD, Email, DateTime.UtcNow);
                                    // Create Account service object
                                    AccountService serviceTest = new AccountService();
                                    serviceTest.CreateUserRecord(user, signedUser, dbSource);
                                }
                                break;
                            case "Edit Account":
                                //Console.WriteLine(operation);
                                //Console.WriteLine(operation);
                                if (String.IsNullOrEmpty(UN))
                                {
                                    // Create UserAccount object
                                    UserAccount user = new UserAccount(UN, PWD, Email, DateTime.UtcNow);
                                    // Create Account service object
                                    AccountService serviceTest = new AccountService();
                                    serviceTest.CreateUserRecord(user, signedUser, dbSource);
                                }
                                break;
                            case "Delete Account":
                                //Console.WriteLine(operation);
                                //Console.WriteLine(operation);
                                if (String.IsNullOrEmpty(UN))
                                {
                                    // Create UserAccount object
                                    UserAccount user = new UserAccount(UN, PWD, Email, DateTime.UtcNow);
                                    // Create Account service object
                                    AccountService serviceTest = new AccountService();
                                    serviceTest.CreateUserRecord(user, signedUser, dbSource);
                                }                            
                                break;
                            case "Disable Account":
                                //Console.WriteLine(operation);
                                //Console.WriteLine(operation);
                                if (String.IsNullOrEmpty(Role) && UN != null)
                                {
                                    // Create UserAccount object
                                    UserAccount user = new UserAccount(UN, PWD, Email, DateTime.UtcNow);
                                    // Create Account service object
                                    AccountService serviceTest = new AccountService();
                                    serviceTest.CreateUserRecord(user, signedUser, dbSource);
                                }
                                else if (String.IsNullOrEmpty(Email) && UN != null)
                                {
                                    // Create UserAccount object
                                    UserAccount user = new UserAccount(UN, PWD, Role);
                                    // Create Account service object
                                    AccountService serviceTest = new AccountService();
                                    serviceTest.CreateUserRecord(user, signedUser, dbSource);
                                }
                                else if (String.IsNullOrEmpty(Role) && String.IsNullOrEmpty(Email) && UN != null)
                                {
                                    // Create UserAccount object
                                    UserAccount user = new UserAccount(UN, PWD, DateTime.UtcNow);
                                    // Create Account service object
                                    AccountService serviceTest = new AccountService();
                                    serviceTest.CreateUserRecord(user, signedUser, dbSource);
                                }
                                break;
                            case "Enable Account":
                                //Console.WriteLine(operation);
                                //Console.WriteLine(operation);
                                if (String.IsNullOrEmpty(Role) && UN != null)
                                {
                                    // Create UserAccount object
                                    UserAccount user = new UserAccount(UN, PWD, Email, DateTime.UtcNow);
                                    // Create Account service object
                                    AccountService serviceTest = new AccountService();
                                    serviceTest.CreateUserRecord(user, signedUser, dbSource);
                                }
                                else if (String.IsNullOrEmpty(Email) && UN != null)
                                {
                                    // Create UserAccount object
                                    UserAccount user = new UserAccount(UN, PWD, Role);
                                    // Create Account service object
                                    AccountService serviceTest = new AccountService();
                                    serviceTest.CreateUserRecord(user, signedUser, dbSource);
                                }
                                else if (String.IsNullOrEmpty(Role) && String.IsNullOrEmpty(Email) && UN != null)
                                {
                                    // Create UserAccount object
                                    UserAccount user = new UserAccount(UN, PWD, DateTime.UtcNow);
                                    // Create Account service object
                                    AccountService serviceTest = new AccountService();
                                    serviceTest.CreateUserRecord(user, signedUser, dbSource);
                                }
                                break;
                            default:
                                break;
                        }
                    }  
                }
                fileRead.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading file");
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
