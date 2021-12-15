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
        //public void ExtractBulkOP()
        public static void Main(string[] args)
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

                // Create Account service object
                AccountService serviceTest = new AccountService();

                // List to hold file operation and credentials
                List<string[]> fileList = new List<string[]>();
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
                        if(ops.Contains(value))
                        {
                            // Declare empty list each time operation is found
                            string[] temp = new string[] { };
                            // Loop until empty line is found
                            while (true)
                            {
                                string currLine = fileRead.ReadLine();
                                // Check if line is not empty
                                if(currLine != null && currLine.Contains(":"))
                                {
                                    currLine = currLine.Split(':')[1].Trim();
                                    if (ops.Contains(currLine))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        temp.Append(currLine);
                                    }
                                }
                            }
                            fileList.Add(temp);
                        }
                    }
                }
                Console.WriteLine(fileList[2]);
/*            for (int i = 0; i < fileList.Count; i++)
            {
                for(int j = 0; j < fileList[i].Length; j++)
                    {
                        Console.WriteLine(fileList[i][j]);
                    }
            }*/
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
