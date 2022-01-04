using System;
using System.Collections.Generic;
namespace TeamHobby.HobbyProjectGenerator.Logging
{
    // implementation of ILogger that stores logs as they initialize
    public class InMemoryLogger : ILogger
    {
        // make readonly to prevent it from being changed somewhere other than in the constructor
        private readonly IList<string> _logStore;

        public InMemoryLogger()
        {
            // list of current logs
            _logStore = new List<string>();
        }

        public InMemoryLogger(IList<string> logStore)
        {
            _logStore = logStore;
        }

        public bool Log(LogEntry entry)
        {
            if (entry is null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            try
            {
                // string interpolation to add UTC timestamp to log description
                //_logStore.Add($"{DateTime.UtcNow}-> {description}");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IList<string> GetAllLogs()
        {
            return _logStore;
        }

     
        public override bool Equals(object? obj)
        {
            return obj is InMemoryLogger logger &&
                   EqualityComparer<IList<string>>.Default.Equals(_logStore, logger._logStore);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_logStore);
        }
    }

}
