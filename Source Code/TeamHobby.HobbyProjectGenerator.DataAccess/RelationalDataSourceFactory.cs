using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamHobby.HobbyProjectGenerator.Archive;
using TeamHobby.HobbyProjectGenerator.DataAccess;

namespace TeamHobby.HobbyProjectGenerator.Archive
{
    public class RelationalDataSourceFactory
    {
        // Mehthod to create a specific data source suitable to the need
        public IDataSource<string> getDataSource(string name)
        {
            try
            {
                if (String.Equals(name, "SQL", StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
