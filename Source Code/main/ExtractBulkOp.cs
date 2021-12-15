using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using TeamHobby.HobbyProjectGenerator.DataAccess;
using TeamHobby.HobbyProjectGenerator.UserManagement;

namespace main
{
    public class ExtractBulkOp
    {
        public void ExtractBulkOP(string signedUser, IDataSource<string> dbSource)
        //public static void Main(string[] args)
        {
            try
            {
                // Get the current directory.
                string path = Directory.GetCurrentDirectory();
                // Open file reader stream
                using StreamReader fileRead = new(path + "\\BulkOps\\Bulk.txt"); // path + "\\BulkOps\\{input}"
                // Show location of the file being written
                Console.WriteLine(path + "\\BulkOps\\Bulk.txt");

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
                    switch (operation)
                    {
                        case "Create Account":
                            //Console.WriteLine(operation);
                            if (String.IsNullOrEmpty(Role))
                            {
                                Role = null;
                                // Create UserAccount object
                                UserAccount user = new UserAccount(UN, PWD, Email, DateTime.UtcNow);
                                // Create Account service object
                                AccountService serviceTest = new AccountService();
                                Console.WriteLine($"VALUES ('{user.username}', " +
                                    $"'{user.password}','{user.role}', 1," +
                                    $"'Jacob', NOW(),'{user.email}');");
                                //Console.WriteLine(serviceTest.CreateUserRecord(user, 'Jacob', dbSource));
                            }
                            else if (String.IsNullOrEmpty(Email))
                            {
                                Email = null;
                                // Create UserAccount object
                                UserAccount user = new UserAccount(UN, PWD, Role);
                                // Create Account service object
                                AccountService serviceTest = new AccountService();
                                Console.WriteLine($"VALUES ('{user.username}', " +
                                    $"'{user.password}','{user.role}', 1," +
                                    $"'Jacob', NOW(),'{user.email}');");
                                //serviceTest.CreateUserRecord(user, signedUser, dbSource);
                            }
                            else
                            {
                               // Console.WriteLine(UN);
                                // Create UserAccount object
                                UserAccount user = new UserAccount(UN, PWD, DateTime.UtcNow);
                                // Create Account service object
                                AccountService serviceTest = new AccountService();
                                if (user.email == null)
                                {
                                    Console.WriteLine("wokring correctly");
                                }
                                else
                                {
                                    Console.WriteLine($"VALUES ('{user.username}', " +
                                        $"'{user.password}','{user.role}', 1," +
                                        $"'Jacob', NOW(),'{user.email}');");
                                    //serviceTest.CreateUserRecord(user, signedUser, dbSource);
                                }
                            }
                            break;
                        case "Edit Account":
                            //Console.WriteLine(operation);
                            //Console.WriteLine(operation);
                                
                            break;
                        case "Delete Account":
                            //Console.WriteLine(operation);
                            //Console.WriteLine(operation);
                               
                            break;
                        case "Disable Account":
                            //Console.WriteLine(operation);
                            //Console.WriteLine(operation);
                               
                            break;
                        case "Enable Account":
                            //Console.WriteLine(operation);
                            //Console.WriteLine(operation);
                               
                            break;
                        default:
                            break;
                        
                    }
                }
                fileRead.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading file");
                Console.WriteLine(e.Message);
            }
        }
    }
}
