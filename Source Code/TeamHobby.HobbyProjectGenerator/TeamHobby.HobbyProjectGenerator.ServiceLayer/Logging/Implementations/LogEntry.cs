using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum LogLevel
    {
        Info, Debug, Warning, Error
    }

    public enum LogCategory
    {
        View, Business, Server, Data, Datastore
    }


namespace TeamHobby.HobbyProjectGenerator.Logging.Contracts
{
    // LogEntry is a record type with init properties for each field
    // ensures that log entries are immutable after initialization
     // LogEntry is a record type with init properties for each field
    // ensures that log entries are immutable after initialization
    public record LogEntry
    {
        private LogLevel _level { get; init; }
        private LogCategory _category { get; init; }
        private string _user { get; init; }
        private string _description { get; init; }

        // Constructor with args for all fields except timestamp
        // timestamp is always set to the UTC time at the time of initialization
        public LogEntry(LogLevel level, LogCategory category,  string user, string description)
        {
            this._level = level;
            this._category = category;
            this._user = user;
            this._description = description;
        }

        // Constructor requiring only the description as an arg
        // Assigns default values for LogLevel (Info), LogCategory (Server), user ("System"), and the UTC time at initialization for timestamp
        // public LogEntry(string description)
        // {
        //     this._level = LogLevel.Debug;
        //     this._category = LogCategory.Server;
        //     this._user = "SYSTEM";
        //     this._description = description;
        // }

        public LogLevel GetLevel() {
            return this._level;
        }

        public LogCategory GetCategory() {
            return this._category;
        }

        public string GetUser() {
            return this._user;
        }

        public string GetDescription() {
            return this._description;
        }

        public void ShowLog() {
            Console.WriteLine("Timestamp: {0} | Level: {1}, | Category: {2}, | User: {3}, | Description: '{4}' ", DateTime.Now, this._level.ToString(),this._category.ToString(), this._user, this._description);
        }
    }
}


