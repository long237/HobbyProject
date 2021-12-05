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
        //    conn = new SqlConnection(info);
        //}

        public bool DeleteData()
        {
            throw new NotImplementedException();
        }

        public Object ReadData(string cmd)
        {
            try
            {
                // conn.open();
                // conn.Execute();
                Console.WriteLine("Access a SQL database");
                Console.WriteLine("Select * from archive");

                // while (myReader)
                // Print to console
                // conn.close();
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool UpdateData()
        {
            throw new NotImplementedException();
        }

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

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
