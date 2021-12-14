using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Data.Odbc;

namespace TeamHobby.HobbyProjectGenerator.DataAccess
{
    public class SqlDAO : IDataSource<string>
    {
        private OdbcConnection _conn;

        public SqlDAO(string info)
        {
            try {
                Console.WriteLine("Establising Connection");
                _conn = new OdbcConnection(info);
                Console.WriteLine("Connection established");
            }
            catch {
                //_conn = null;
                Console.WriteLine("Error when creating a connection");
                throw;
            }
        }

        // Getter and setter for Odbc
        public OdbcConnection Connection { get; set; }

        public OdbcConnection GetConnection()
        {
            return _conn;
        }

        // TODO: Closing a connection here will cause a problem
        // Make sure whoever called this need to close the connection until we can fixed it. 
        public Object? ReadData(string cmd)
        {
            try
            {
                _conn.Open();
                OdbcCommand command = new OdbcCommand(cmd, _conn);
                OdbcDataReader reader = command.ExecuteReader();

                //_conn.Close();
                return reader;
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when Reading data from databse.");
                Console.WriteLine(e.Message);
                return null;
            }
            //finally
            //{
            //    _conn.Close();
            //}
        }

        // The also Identical to update data, maybe only one method is enough
        public bool DeleteData(string cmd)
        {
            try
            {
                _conn.Open();

                OdbcCommand command = new OdbcCommand(cmd, _conn);
                command.ExecuteNonQuery();

                _conn.Close();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when deleting data from database!!");
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                _conn.Close();
            }
        }


        // TODO: No idea how to check if the command is valid or where to check it
        public bool UpdateData(string cmd)
        {
            try
            {
                _conn.Open();

                OdbcCommand command = new OdbcCommand(cmd, _conn);
                command.ExecuteNonQuery();

                _conn.Close();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when updating data from database!!");
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                _conn.Close();
            }
        }

        // Very similar to UpdataData if not identical
        public bool WriteData(string cmd)
        {
            try
            {
                _conn.Open();

                OdbcCommand command = new OdbcCommand(cmd, _conn);
                command.ExecuteNonQuery();

                _conn.Close();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when Writing data from database!!");
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                _conn.Close();
            }
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
                Console.WriteLine("Error when compressing a file !!, will be handled higher up the call stack");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // Copy the Sql data to the a text file
        public bool CopyToFile(string filePath){
            try
            {
                _conn.Open();
                // Conver backward slash in to forward slash
                filePath = filePath.Replace("\\", "/");
                string sqlQuery = "SELECT 'LtimeStamp', 'logID', 'LvName', 'catName', 'userOP', 'logMessage' " +
                    "UNION ALL " +
                    "SELECT* FROM log " +
                    "WHERE DATEDIFF(CURRENT_TIMESTAMP, log.LtimeStamp) > 30 " +
                    "INTO OUTFILE '" + filePath + "'" +
                    "FIELDS ENCLOSED BY ''" + 
                    "TERMINATED BY ',' " +
                    "LINES TERMINATED BY '\r\n';";

                Console.WriteLine(sqlQuery);

                OdbcCommand command = new OdbcCommand(sqlQuery, _conn);
                //command.Parameters.AddWithValue("@filePath", filePath);
                //command.Prepare();

                Console.WriteLine("Output to a file started. ");
                command.ExecuteNonQuery();
                Console.WriteLine("Output to a file completed. ");


                _conn.Close();
                return true;
               
            }
            catch (Exception ex)
            {   
                _conn.Close();
                Console.WriteLine("Error when copying query to a file !!, will be handled higher up the call stack");
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public bool RemoveOutputFile(string filePath){
            try
            {
                if (File.Exists(filePath))
                {
                    Console.WriteLine("File at: {0} exist, proceed to remove.", filePath);
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch {
                Console.WriteLine("Removing file failed!!");
                throw; 
            }
        }

        // Remove data from the database, if failed, let the controller handle it. 
        public bool RemoveEntries() {
            
            string sqlCmd = "DELETE FROM log WHERE DATEDIFF(current_timestamp, log.LtimeStamp) > 30";

            try{
                DeleteData(sqlCmd);
                return true;
            }
            catch {
                Console.WriteLine("Eeep! an error in Remove Entries, not to worry, will be handled higher up the call stack");
                throw;
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
