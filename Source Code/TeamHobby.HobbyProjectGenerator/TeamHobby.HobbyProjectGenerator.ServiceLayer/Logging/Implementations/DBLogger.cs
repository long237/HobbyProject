using System;
using System.Data.Odbc;
using TeamHobby.HobbyProjectGenerator.DataAccessLayer;
using TeamHobby.HobbyProjectGenerator.Logging.Contracts;

namespace TeamHobby.HobbyProjectGenerator.ServiceLayer
{
 public class DBLogger : ILogger
    {
        private SqlDAO _datasource;
        public DBLogger() 
        {   
        }

        //  SELECT * FROM log
        // public IList<LogEntry> GetAllLogs()
        // {
        //     throw new NotImplementedException();
        // }

        //INSERT INTO log(LvName, catName, userOP, logMessage) VALUES ('Debug','Data', 'SYSTEM', 'testing insert method')   

        //  public bool Connect(SqlDAO logDatabase) {
        //
        //      }  

        
        public bool IsActive() {
            string dbInfo = "DRIVER={MariaDB ODBC 3.1 Driver};" +
                "TCPIP=1;" +
                "SERVER=ocalhost;" +
                "DATABASE=hbby;" +
                "UID=ot;" + // omit password to force SQLException for incorrect passeword
                "PASSWORD=wrong" +
                "OPTION=3";
           // bool isActive = true;
                OdbcConnection active = new OdbcConnection(dbInfo);
            try {
                active.Open();
                return false;
                }
            catch (OdbcException ex) {
               // Console.WriteLine(ex.Message);
                if (ex.ErrorCode == -2146232009 ) {
                    //Console.WriteLine(ex.ErrorCode);
                    Console.WriteLine("Database is active");
                    return true;
                }
                else {
                    Console.WriteLine("Database is inactive");
                    return false;
                } 
            } 
            finally {
              //  Console.WriteLine("closed1");
                active.Close();
            }
        // return false;
        }        
        
        public bool IsAccessible() {
            string dbInfo = "DRIVER={MariaDB ODBC 3.1 Driver};" +
                "TCPIP=1;" +
                "SERVER=localhost;" +
                "DATABASE=hobby;" +
                "UID=root;" +
                "PASSWORD=Teamhobby;" +
                "OPTION=3";
            OdbcConnection access = new OdbcConnection(dbInfo);
            try {
                access.Open();
                Console.WriteLine("Database is active and accessible by the system");
                return true;
            }
            catch {
                Console.WriteLine("Database is active but inaccessible to the system at this time.");
                return false;
            }  
            finally {
                access.Close();
            }              
        }

        public bool HasCapacity() {
            string dbInfo = "DRIVER={MariaDB ODBC 3.1 Driver};" +
                "TCPIP=1;" +
                "SERVER=localhost;" +
                "DATABASE=hobby;" +
                "UID=root;" +
                "PASSWORD=Teamhobby;" +
                "OPTION=3";
            OdbcConnection conn = new OdbcConnection(dbInfo);

            // get the current capacity of the database in MB using the query: 
            // SELECT SUM(storage.MB) as CURRENT_CAPACITY_MB FROM (SELECT table_schema as name, ROUND(SUM(data_length + index_length)/1024/1024,1) as MB FROM information_schema.tables GROUP BY table_schema) as storage;

            string sqlCurrentCap = $"SELECT SUM(storage.mb) as currentCap FROM (SELECT table_schema as name, ROUND(SUM(data_length + index_length)/1024/1024,1) as mb FROM information_schema.tables GROUP BY table_schema) as storage;";

            // get the max capacity in GB; too big to be shown as MB
            string sqlMaxCap = $"SELECT SUM(storage.mb) as maxCap FROM (SELECT table_schema as name, ROUND(SUM(max_data_length + max_index_length)/1024,1) as mb FROM information_schema.tables GROUP BY table_schema) as storage;";

            // query the data base to get currentCap; the current storage capacity of the system's database (in megabytes)
            conn.Open();
            OdbcCommand command1 = new OdbcCommand(sqlCurrentCap, conn);
            OdbcDataReader result1 = command1.ExecuteReader();
            double currentCap = 0;
            while (result1.Read()) {
                //Console.WriteLine(result1[0].GetType());
                //Console.WriteLine("currentCap = {0}", result1[0]);
                currentCap = result1.GetDouble(0);
            }

          //  Console.WriteLine(currentCap);
            //float currentCap1 = (float) currentCap;
            result1.Close();
            command1.Dispose();
            conn.Close();
           // Console.WriteLine("close conn");

            // query the data base again to get maxCap; the maximum possible  storage capacity of the system's database (in megabytes)
            conn.Open();
            //    Console.WriteLine("open conn");
            OdbcCommand command2 = new OdbcCommand(sqlMaxCap, conn);
            OdbcDataReader result2 = command2.ExecuteReader();
             //  Console.WriteLine("open reader 1");
            double maxCap = 0;
            try {
                while (result2.Read()) {
                    //Console.WriteLine(result2[0].GetType());
                    maxCap = result2.GetDouble(0);
                //  Console.WriteLine("maxCap = {0}", result2[0]);
                } 
                // compare currentCap to maxCap
                // return true if there is at least 1MB of free space available
                // decrement max by 1 MB to allow for any required space from new logs
                if ( (currentCap * 1024) < (maxCap - 1024) ) { 
                   // Console.WriteLine(maxCap);
                    Console.WriteLine("Database has sufficient storage capacity to be written to at this time.\n Current Capacity: {0} MB\n Max Capacity: {1} MB\n", currentCap, maxCap);
                }                
            }
            catch {
                Console.WriteLine("Insufficient Storage Capacity to log entry to database.\n Current Capacity: {0} MB \n Max Capacity: {1} MB \n", currentCap, maxCap);  
                return false;              
            }
            finally {
                result2.Close();
                command2.Dispose();
                conn.Close();
            }
            return false;
        }
    
            //Console.WriteLine("close conn 2");            
            //Console.WriteLine(maxCap);

           
           


        public bool Log(LogEntry log)
        {                       
            log.ShowLog();
   
            //bool status = HasCapacity();
            if ( (IsActive() == false || IsAccessible() == false) || (HasCapacity() == false) ) {
                return false;
            }

            else {
                //  SqlDAO sqlDS = (SqlDAO)_datasource;
                string dbInfo = "DRIVER={MariaDB ODBC 3.1 Driver};" +
                "TCPIP=1;" +
                "SERVER=localhost;" +
                "DATABASE=hobby;" +
                "UID=root;" +
                "PASSWORD=Teamhobby;" +
                "OPTION=3";
                _datasource = new SqlDAO(dbInfo); 
                
                //bool storage = HasCapacity();
                try 
                {
                    // once connection is confirmed, try inserting the log via SQL statements
                    string sqlLog = $"INSERT INTO log (Lvname, catName, userOP, logMessage) VALUES ('{log.GetLevel()}','{log.GetCategory()}', " + $"'{log.GetUser()}', '{log.GetDescription()}');";

                    bool insertNewLog = _datasource.WriteData(sqlLog);
                    //Console.WriteLine($"Logging Succeeded: The following entry was written to the database: {log}");
                  //  Console.WriteLine(_datasource.ReadData($"SELECT * FROM log;"));
                    return insertNewLog;
                }
                // if failed, that means the connection was a sucess but actually writing to the database failed
                catch 
                {
                    Console.WriteLine("Failed to write log to database; likely due to error in SQL syntax");
                    return false;
                }
            }
        }  
    }            
}
