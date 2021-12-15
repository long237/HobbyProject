using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using TeamHobby.HobbyProjectGenerator.DataAccess;

namespace TeamHobby.HobbyProjectGenerator.UserManagement
{
    public class AccountService
    {
        public bool CreateUserRecord(UserAccount newUser, string CreatedBy, IDataSource<string> dbSource)
        {
            try
            {
                // Insert into users table
                string sqlUser = $"INSERT INTO users (UserName, Password, Role, IsActive," +
                    $"CreatedBy, CreatedDate, Email) VALUES ('{newUser.username}', " +
                    $"'{newUser.password}','{newUser.role}', 1," +
                    $"'{CreatedBy}', NOW(),'{newUser.email}');";

                bool insertNewUser = dbSource.WriteData(sqlUser);
                return insertNewUser;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool EditUserRecord(UserAccount newUser, string CreatedBy, IDataSource<string> dbSource)
        {
/*            // Insert into users table
            string sqlUser = $"UPDATE hobby.roles r SET r.Role = 'regular',r.CreatedBy = 'colin'WHERE r.RoleID = 5; ";

            Object insertNewUser = dbSource.WriteData(sqlUser);
            // Insert into users table
            string sqlRoles = $"INSERT INTO roles (Role, CreatedBy, CreatedDate) " +
                $"VALUES ('{newUser.NewRole}', '{CreatedBy}', NOW());";
            Object insertNewRole = dbSource.WriteData(sqlUser);
            // Create string for confirming user account
            string confirmUser = $"Select * from users where username = {newUser.NewUserName} " +
                $"and password = {newUser.NewPassword};";
            Object conUser = dbSource.ReadData(confirmUser);

            //Console.WriteLine("type of Reesult:" + confirmAdmin.GetType());
            OdbcDataReader reader = null;

            if (conUser.GetType() == typeof(OdbcDataReader))
            {
                reader = (OdbcDataReader)conUser;
            }

            // Read Sql query results
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0));
            }

            SqlDAO sqlDS = (SqlDAO)dbSource;
            Console.WriteLine("");

            // Closing the connection
            sqlDS.getConnection().Close();*/
            return true;
        }
        public bool DeleteUserRecord(UserAccount newUser, IDataSource<string> dbSource)
        {
            return true;
        }
        public bool DisableUser(UserAccount newUser, IDataSource<string> dbSource)
        {
            return true;
        }
        public bool EnableUser(UserAccount newUser, IDataSource<string> dbSource)
        {
            return true;
        }
    }
}
