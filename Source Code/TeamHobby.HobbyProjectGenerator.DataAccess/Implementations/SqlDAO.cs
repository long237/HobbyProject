﻿using System;
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

        public OdbcConnection getConnection()
        {
            return _conn;
        }

        // Closing a connection here will cause a problem
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
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Copy the Sql data to the a text file
        public bool CopyToFile(string filePath){
            return true;
        }

        public bool RemoveOutputFile(string filePath){
            return true;
        }

        // Remove data from the database, if failed, let the controller handle it. 
        public bool RemoveEntries() {
            
            string sqlCmd = "DELETE FROM log WHERE DATE_DIFF(current_timestamp, log.LtimeStamp) > 30";

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