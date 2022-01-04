using System;
using TeamHobby.HobbyProjectGenerator.Logging.Contracts;

namespace TeamHobby.HobbyProjectGenerator.Logging
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
