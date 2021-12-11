using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamHobby.HobbyProjectGenerator.DataAccess;

namespace TeamHobby.HobbyProjectGenerator.DataAccess
{
    public class RelationalDataSourceFactory
    {
        // Mehthod to create a specific data source suitable to the need
        public IDataSource<string> getDataSource(string name, string info)
        {
            try
            {
                if (String.Equals(name, "SQL", StringComparison.OrdinalIgnoreCase))
                {
                    return new SqlDAO(info);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("RelationDataFactory: Data Access object creation failed!");
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
