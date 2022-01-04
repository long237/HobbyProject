using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamHobby.HobbyProjectGenerator.Logging.Contracts;

namespace TeamHobby.HobbyProjectGenerator.ServiceLayer
{
  public interface ILogger
    {
      // private IDataSource<string> _datasource = T;
      // attempts to connect to the data source provided by the type T
      // returns true if the connection is successful    
 
     // public bool Connect();
      public bool Log(LogEntry Log);

      //  IList<string> GetAllLogs();

    }
}
