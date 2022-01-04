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


        // Constructor requireing only the description as an arg
        // Assigns default values for LogLevel (Info), LogCategory (Server), user ("System"), and the UTC time at initialization for timestamp
        public LogEntry(string description)
        {
            this.level = LogLevel.Info;
            this.category = LogCategory.Server;
            this.user = "System";
            this.description = description;
            this.timestamp = DateTime.UtcNow;
        }
        // Constructor with args for all fields except timestamp
        // timestamp is always set to the UTC time at the time of initialization
        public LogEntry(LogLevel level, string user, LogCategory category, string description)
        {
            this.level = level;
            this.category = category;
            this.user = user;
            this.description = description;
            this.timestamp = DateTime.UtcNow;
        }


    }
}
*/