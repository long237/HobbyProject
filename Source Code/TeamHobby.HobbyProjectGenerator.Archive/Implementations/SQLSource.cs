using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;


namespace TeamHobby.HobbyProjectGenerator.Archive
{
    public class SQLSource : IDataSource, IRelationArchivable
    {
        //private SqlConnection conn;

        //public SQLSource(string info)
        //{
        //   // Do I put using here to make sure the connection closed once the object is gone?
        //   conn = new SqlConnection(info);
        //   // Perhaps open the connection here?
        //   // conn.Open();
        //}

        // The also Identical to update data, maybe only one method is enough
        public bool DeleteData(string cmd)
        {
            throw new NotImplementedException();
        }

        // Makre sure to Check for instanceof() before casting to a SQLReader in the controller
        public Object ReadData(string cmd)
        {
            try
            {
                //conn.Open();
                //SqlCommand command = new SqlCommand(null, conn);
                //SqlCommand command = new SqlCommand(cmd, conn);

                // conn.Execute();
                Console.WriteLine("Access a SQL database");
                Console.WriteLine("Select * from archive");

                // Execute the command to query the data
                //using SqlDataReader sqlReader = command.ExecuteReader();

                // while (myReader)
                // Print to console
                //conn.Close();
                //conn.Dispose();
                //return sqlReader;
                return new SqlConnection();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        // TODO: No idea how to check if the command is valid or where to check it
        public bool UpdateData(string cmd)
        {
            try
            {
                //conn.Open();

                // Create the SQL command object
                //SqlCommand command = new SqlCommand(cmd, conn);

                // Execute the command to change the rows in a table
                //command.ExecuteNonQuery();

                //conn.Close();
                //conn.Dispose();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        // Very similar to UpdataData if not identical
        public bool WriteData(string cmd)
        {
            throw new NotImplementedException();
        }

        // Method to archive data
        public bool CreateArchived(string fileName)
        {
            throw new NotImplementedException();
        }

        public bool CompressFile(string fileName)
        {
            try
            {   
                // Get the stream of the original file
                using FileStream origFile = File.Open(fileName, FileMode.Open);


                // Get the attribute of the file
                FileAttributes atrribute = File.GetAttributes(fileName);

                // Check to see if the file is hidden or already compressed before compressing the file. 
                if (atrribute != FileAttributes.Hidden && atrribute != FileAttributes.Compressed)
                {
                    using FileStream outputFile = File.Create(fileName + ".gz");
               
                    using GZipStream compressor = new GZipStream(outputFile, CompressionMode.Compress);
                    origFile.CopyTo(compressor);

                return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //public Object ReadPreparedStmt(string table){
        //    //conn.Open();
        //    //SqlCommand command = new SqlCommand(null, conn);
        //    SqlCommand command = new SqlCommand("SELECT * from @table;", conn);
        //    SqlParameter tableParam = new SqlParameter("@table", System.Data.SqlDbType.Text, 50);

        //    tableParam.Value = table;
        //    command.Parameters.Add(tableParam);

        //    // conn.Execute();
        //    Console.WriteLine("Access a SQL database");
        //    Console.WriteLine("Select * from archive");

        //    // Make the Prepare statement and excute the query:
        //    command.Prepare();
        //    SqlDataReader sqlReader = command.ExecuteReader();

        //    // while (myReader)
        //    // Print to console
        //    // conn.close();
        //    return sqlReader;
        //}

    }
}
