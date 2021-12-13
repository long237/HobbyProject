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



        public bool Log(string description)
        {
            try
            {
                // string interpolation to add UTC timestamp to log description
                _logStore.Add($"{DateTime.UtcNow}-> {description}");
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
    }

}