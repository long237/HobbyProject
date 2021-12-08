using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamHobby.HobbyProjectGenerator.Archive;

namespace TeamHobby.HobbyProjectGenerator.Archive
{
    public class RelationalDataSourceFactory
    {
        // Mehthod to create a specific data source suitable to the need
        public IDataSource? getDataSource(string name)
        {
            try
            {
                if (String.Equals(name, "SQL", StringComparison.OrdinalIgnoreCase))
                {
                    return new SQLSource();
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
