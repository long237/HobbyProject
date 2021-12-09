using Microsoft.Data.SqlClient;
using TeamHobby.HobbyProjectGenerator.Models;

namespace TeamHobby.HobbyProjectGenerator.DAL
{
    public class SqlDAO
    {
        public IList<Credentials> GetUserData(string username)
        {
            // Sql server connection string, needs to be changed accordingly to connect
            var connString = "server=localhost;userid=root;password=Plop20;database=users"; // Using @" " makes the string literal
            
            // ADO.NET - ODBC
            using (var conn = new SqlConnection(connString))
            {
                // More complex sql commands are done in this method instead
                var sql = "Select * from roles";
                using (var command = new SqlCommand(sql, conn))
                {
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

                }

               /*
                * this is meant for specific basic sql commands
                using (var adapter = new SqlDataAdapter())
                {
                    adapter.SelectCommand
                }
               */
            } 
        }
    }
}
