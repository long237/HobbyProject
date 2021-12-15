using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamHobby.HobbyProjectGenerator.Logging.Contracts
{
    public interface ILoggerFactory
    {
        ILogger CreateLogger()
        {
            return new DBLogger();
        }
    }
}
