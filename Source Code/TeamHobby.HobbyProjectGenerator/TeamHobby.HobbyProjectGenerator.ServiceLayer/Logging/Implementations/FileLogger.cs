using System;
using TeamHobby.HobbyProjectGenerator.Logging.Contracts;
using TeamHobby.HobbyProjectGenerator.ServiceLayer.Contracts;

namespace TeamHobby.HobbyProjectGenerator.ServiceLayer
{
    public class FileLogger : ILogger
    {
        public FileLogger()
        {
        }

        public IList<string> GetAllLogs()
        {
            throw new NotImplementedException();
        }

        public bool Log(LogEntry log)
        {
            throw new NotImplementedException();
        }

    }

}
