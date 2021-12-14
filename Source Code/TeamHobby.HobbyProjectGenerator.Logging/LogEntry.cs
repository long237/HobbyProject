/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamHobby.HobbyProjectGenerator.Logging
{
    public enum LogLevel
    {
        Info, Debug, Warning, Error
    }

    public enum LogCategory
    {
        View, Business, Server, Data, Datastore
    }

    // LogEntry is a record type with init properties for each field
    // ensures that log entries are immutable after initialization
    public record LogEntry
    {
        public LogLevel level { get; init; }
        public LogCategory category { get; init; }
        public string user { get; init; }
        public string description { get; init; }
        public DateTime timestamp { get; init; }

    }
}
*/