using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamHobby.HobbyProjectGenerator.ServiceLayer
{
    public interface ILogger
    {
        bool Log(LogEntry log);

      //  IList<string> GetAllLogs();

    }
}
