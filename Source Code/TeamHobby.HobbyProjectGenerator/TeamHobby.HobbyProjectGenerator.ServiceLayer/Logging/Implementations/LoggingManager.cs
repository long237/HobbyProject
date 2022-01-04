using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamHobby.HobbyProjectGenerator.DataAccessLayer;
using TeamHobby.HobbyProjectGenerator.ServiceLayer.Contracts;
using TeamHobby.HobbyProjectGenerator.ServiceLayer.Implementations;


namespace TeamHobby.HobbyProjectGenerator.ServiceLayer
{
    internal class LoggingManager
    {
        private readonly IDataSource<string> _conn;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _factory;
        private readonly LogEntry _logEntry;

        // Both Constructors take an IDataSource arg to make logging extensible to future data sources
        // first constructor has no additional args - defaults to using the DBFactiry implementation
        public LoggingManager(IDataSource<string> dataSource)
        {
            _conn = dataSource;
            _factory = new DBLoggerFactory();
            _logger = _factory.CreateLogger();
        }
        // second constructor takes an additional ILoggerFactory arg - allows for extensible logger types 
        public LoggingManager(IDataSource<string> dataSource, ILoggerFactory factory)
        {
            _conn = dataSource;
            _factory = factory;
            _logger = _factory.CreateLogger();

        }

        public void Process()
        {
            _logger.Log(_logEntry);
        }



    }
}
