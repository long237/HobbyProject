// See https://aka.ms/new-console-template for more information
//using System.Data.SqlClient;
using System.IO.Compression;
using TeamHobby.HobbyProjectGenerator.Archive;
//using MySql.Data.MySqlClient;
using Microsoft.Data.SqlClient;
using System.Data.Odbc;

public class HobbyMain
{
    static void Main(string[] args)
    {
        // Testing file compression Start
        //Console.WriteLine("Hello World!");

        //// Testing Compressing a file
        //string fileName = @"C:\Users\Chunchunmaru\Documents\csulbFall2021\HobbyProject\Source Code\TeamHobby.Main\rando";
        ////string fileName = @"arhiving2.txt";
        //FileInfo fileInfo = new FileInfo(fileName);

        //Console.WriteLine("File Name: {0}", fileInfo.FullName);

        //SQLSource sqlSource = new SQLSource();
        //bool res = sqlSource.CompressFile(fileName);
        ////bool res = CompressFile(fileName);
        //Console.WriteLine(res);

        //Console.WriteLine(CreateFileName());

        // Testing File compression end

        // testing end

        // Testing connection to MariaDB

        string connString = "user id=root;" + "password=Teamhobby;server=localhost;" + "Trusted_Connection=yes;" + "database=Alatreon; " + "connection timeout=5";
        string connMariaDB = "server=localhost;port=3306;uid=root;pwd=Teamhobby;connection timeout=5";
        var conn = new SqlConnection(connString);




        //// SqlConnection conn = new SqlConnection(connString);
        string sqlQ = "Select * from log;";

        //MySql.Data.MySqlClient.MySqlConnection connect;

        //try
        //{
        //    Console.WriteLine("Open connection: ");
        //    //connect = new MySql.Data.MySqlClient.MySqlConnection();
        //    //connect.ConnectionString = connMariaDB;
        //    //connect.Open();
        //    conn.Open();

        //    SqlCommand cmd = new SqlCommand(sqlQ, conn);

        //    SqlDataReader myReader = cmd.ExecuteReader();
        //    while (myReader.Read())
        //    {
        //        Console.WriteLine(myReader["Column1"].ToString());
        //        Console.WriteLine(myReader["Column2"].ToString());
        //    }
        //    conn.Close();

        //    //if (connect.State == System.Data.ConnectionState.Open)
        //    //{
        //    //    Console.WriteLine("Connection established");
        //    //}
        //    //connect.Close();

        //}
        try
        {
            Console.WriteLine("Opening connection: ");
            //Connection string for Connector/ODBC 3.51
            string MyConString = "DRIVER={MariaDB ODBC 3.1 Driver};" +
              "SERVER=localhost;" +
              "DATABASE=hobby;" +
              "UID=root;" +
              "PASSWORD=Teamhobby;" +
              "OPTION=3";

            //Connect to MySQL using Connector/ODBC
            //OdbcConnection MyConnection = new OdbcConnection(MyConString);
            //MyConnection.Open();

            //Console.WriteLine("\n !!! success, connected successfully !!!\n");

            ////Display connection information
            //Console.WriteLine("Connection Information:");
            //Console.WriteLine("\tConnection String:" +
            //                  MyConnection.ConnectionString);
            //Console.WriteLine("\tConnection Timeout:" +
            //                  MyConnection.ConnectionTimeout);
            //Console.WriteLine("\tDatabase:" +
            //                  MyConnection.Database);
            //Console.WriteLine("\tDataSource:" +
            //                  MyConnection.DataSource);
            //Console.WriteLine("\tDriver:" +
            //                  MyConnection.Driver);
            //Console.WriteLine("\tServerVersion:" +
            //                  MyConnection.ServerVersion);

            //Console.WriteLine("Connection Successful");

            ReadData(MyConString);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("COnnection failed");

        }
    }

    public static void ReadData(string connectionString)
    {
        string queryString = "SELECT logID from log;";

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            OdbcCommand command = new OdbcCommand(queryString, connection);

            connection.Open();

            // Execute the DataReader and access the data.
            OdbcDataReader reader = command.ExecuteReader();
            Console.WriteLine("Read the database");
            while (reader.Read())
            {
                //Console.WriteLine("Date={0} {1} {2} {3} {4} {5}", reader[0], reader[1], reader[2], reader[3],reader[4], reader[5]);
                Console.WriteLine("Col A: {0} ", reader[0]);
                //Console.WriteLine("Column: " + reader.FieldCount);
                //Console.WriteLine("Column={1}", reader[1]);

            }

            // Call Close when done reading.
            reader.Close();
            connection.Close();
        }
    }

    public static string CreateFileName()
    {
        return DateTime.Now.ToString() + "archive.txt";
    }
}
