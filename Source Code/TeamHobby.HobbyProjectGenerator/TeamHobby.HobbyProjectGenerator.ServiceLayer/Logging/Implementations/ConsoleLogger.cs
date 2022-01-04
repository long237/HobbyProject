using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamHobby.HobbyProjectGenerator.Logging.Contracts;

namespace TeamHobby.HobbyProjectGenerator.ServiceLayer
{
    public class ConsoleLogger : ILogger
    {
        public bool Log(LogEntry log)
        {
            return true;
        }
    }
}
