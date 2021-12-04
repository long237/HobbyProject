using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamHobby.HobbyProjectGenerator.Archive.Implementations
{
    public class SQLSource : IDataSource
    {
        private SqlConnection conn;

        public SQLSource(string info)
        {
            conn = new SqlConnection(info);
        }
        public int DeleteData()
        {
            throw new NotImplementedException();
        }

        public int ReadData(string cmd)
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
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public int UpdateData()
        {
            throw new NotImplementedException();
        }

        public int WriteData(string cmd)
        {
            throw new NotImplementedException();
        }
    }
}
