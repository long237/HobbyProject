using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamHobby.HobbyProjectGenerator.ServiceLayer.Contracts;

namespace TeamHobby.HobbyProjectGenerator.ServiceLayer.Implementations
{
    public class DBLoggerFactory : ILoggerFactory
    {
        public DBLoggerFactory()
        {
            //this._logDatabase = logDatabase;
        }
        
        public ILogger CreateLogger() {
            return new DBLogger();
        }
    }
}
