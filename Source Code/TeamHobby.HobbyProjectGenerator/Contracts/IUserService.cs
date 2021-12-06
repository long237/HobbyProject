using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamHobby.HobbyProjectGenerator
{
    public interface IUserService
    {
        // Public methods shouldn't be void so it can be tested
        bool User(string username);
        IList<string> GetAllUsers();
    }
}
