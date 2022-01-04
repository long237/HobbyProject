using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamHobby.HobbyProjectGenerator.Archive
{
    public interface IRelationArchivable
    {
        bool CreateArchived(string fileName);
    }
}
