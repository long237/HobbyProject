using System;
using Microsoft.Data.SqlClient;
using TeamHobby.HobbyProjectGenerator;
using TeamHobby.HobbyProjectGenerator.DAL;
using TeamHobby.HobbyProjectGenerator.Models;

namespace TeamHobby.HobbyProjectGenerator.Main
{
    public class ExampleDAO 
    {
      
        public void UserData(string username)
        {
            // Sql server connection string, needs to be changed accordingly to connect
            var connString = "server=localhost,3316;user=root;database=users;password=Plop20"; // Using @" " makes the string literal

            // ADO.NET - ODBC
            using var conn = new SqlConnection(connString);
            {
                // More complex sql commands are done in this method instead
                var sql = "Select * from roles";
                using (var command = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    // Get the results from the query
                    SqlDataReader r = command.ExecuteReader();

                    // If you wanted to get a singular value back such as a count of a certain item
                    //command.ExecuteScalar();

                    // To execute something that doesn't expect results to come back
                    // Use this method instead, IE. Update command
                    //command.ExecuteNonQuery();

                    // Read data from query
                    while (r.Read())
                    {
                        Console.WriteLine(r.ToString());
                    }
                    conn.Close();

                }
                // Console.Read();
                /*
                 * this is meant for specific basic sql commands
                 using (var adapter = new SqlDataAdapter())
                 {
                     adapter.SelectCommand
                 }
                */
                //return null;
            }
        }
    }
}
