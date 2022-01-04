using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamHobby.HobbyProjectGenerator.DataAccessLayer;
using TeamHobby.HobbyProjectGenerator.Logging.Contracts;
using TeamHobby.HobbyProjectGenerator.ServiceLayer.Contracts;
using TeamHobby.HobbyProjectGenerator.ServiceLayer.Implementations;


namespace TeamHobby.HobbyProjectGenerator.ServiceLayer
{
    
    public class LoggingManager
    {
      //  private static IDataSource<string> _datasource;
        private ILogger _logger;
        private ILoggerFactory _factory;
        private LogEntry _logEntry;

        // assign default values for LogEntry fields
        // these are not readonly because they can be overwritten to specify logging at specific field(s) from the main controller by using the public setters for each field
        private LogLevel _level = LogLevel.Debug; 
        private LogCategory _category = LogCategory.Server;
        private string _user = "System";

        // Both Constructors take an IDataSource arg to make logging extensible to future data sources
        // first constructor has no additional args - defaults to using a DBLoggerFactory to log to database
        public LoggingManager()
        {
           // _datasource = datasource;
            _logEntry = new LogEntry(_level, _category, _user, "Logging Manager instantiated");
            _factory = new DBLoggerFactory();
            _logger = _factory.CreateLogger();
        }
        // second constructor takes an additional ILoggerFactory arg - allows for extensible logger types 
        public LoggingManager(ILoggerFactory factory)
        {
            //_datasource = datasource;
            _logEntry = new LogEntry(_level, _category, _user, "Logging Manager instantiated");
            _factory = factory;
            _logger = _factory.CreateLogger();

        }

        // // used to create a template entry with the desired fields to write entries at
        // public void CreateLog(LogLevel level, LogCategory category, string user, string description) {
        //     _logEntry = new LogEntry(level, 
        //     category, user, description);
        //     _logger.Log(_logEntry);
        // }

        // creates a LogEntry object using a description arg
        // uses whateever the current assignments for the other fields ares  
        public bool CreateLog(string description) {
            // creates a LogEntry using the passed in description and current assignements from the necessary fields 
            _logEntry = new LogEntry(_level,  _category, _user, description);

            // attempt to log the LogEntry by calling the _logger's Log() method
            try {
                _logger.Log(_logEntry);
                return true;
            }
            catch {
                return false;
            }

        }

        public void Level(LogLevel level) {
            this._level = level;
        }

        public void Category(LogCategory category) {
            this._category = category;
        }

        public void User(string user) {
            this._user = user;
        }

        // public static void Source(IDataSource<string> datasource) {
        //     _datasource = datasource;
        // }

        // public static IDataSource<string> GetDataSource() {
        //     return _datasource;
        // }
    }
}
