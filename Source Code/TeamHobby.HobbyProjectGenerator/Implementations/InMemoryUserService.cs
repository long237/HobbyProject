using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamHobby.HobbyProjectGenerator.Implementations
{
    public class InMemoryUserService
    {
        // Make it readonly so it can't be changed
        private readonly IList<string> _logstore;

        public InMemoryUserService()
        {
            _logstore = new List<string>();
        }

        public IList<string> GetAllUsers()
        {
            return _logstore;
        }

        public bool User(string username)
        {
            try
            {
                //DateTime.UtcNow.ToString() + "->" + username; old way of doing it
                _logstore.Add($"{DateTime.UtcNow}->{username}");    // New way of doing it
                return true;
            }
            catch
            {
                return false;
            }
        }
        /*
          public bool User(DateTime timestamp, string username)
        {
            try
            {
                //DateTime.UtcNow.ToString() + "->" + username; old way of doing it
                _logstore.Add($"{timestamp.ToUniversalTime()}->{username}");    // New way of doing it
                return true;
            }
            catch
            {
                return false;
            }
        }
        */
    }
}
