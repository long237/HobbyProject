using System;

namespace app
{
    class ArchiveController{
        static void Main(string[] args)
        {
            string dsName = "SQL";
            string cmd = "";
            Console.WriteLine("Hello World!");    

            //SQLConnection myconnection = new SQLConnection();

            // Creating a DataSource Factory to make Data sources
            DataSourceFactory dsFactory = new DataSourceFactory();

            // Creating a SQL data source objects to gain access to the SQL database
            IDataSource dataSource = dsFactory.getDataSource("SQL");

            // Read from the database
            dataSource.ReadData("a");

            // Write to the database
            dataSource.WriteData("Insert newName");

            // Update the datasource
            dataSource.UpdateData();

            // Delete from the datasource
            dataSource.DeleteData();

            Console.WriteLine("-----------------");
            // Creating a Data Source for text files
            IDataSource txtSource = dsFactory.getDataSource("txt");

            // Read from the database
            txtSource.ReadData("a");

            // Write to the database
            txtSource.WriteData("a");

            // Update the datasource
            txtSource.UpdateData();

            // Delete from the datasource
            txtSource.DeleteData();

        }
    }

}
