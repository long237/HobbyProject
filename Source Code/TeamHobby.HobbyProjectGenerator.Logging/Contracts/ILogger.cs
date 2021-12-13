using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamHobby.HobbyProjectGenerator.Logging
{
    public interface ILogger
    {
        bool Log(LogEntry log);

        IList<string> GetAllLogs();

    }
}
