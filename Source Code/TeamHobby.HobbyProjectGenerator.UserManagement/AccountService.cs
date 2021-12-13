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
        public bool CreateUserRecord(UserAccount newUser, IDataSource<string> dbSource)
        {
            // string checkAdmin = $"Select * from users where username = {user.username} and password = {user.password};";
            // Insert into users table
            string sqlUser = $"INSERT INTO hobby.users (UserName, Password," +
                $"CreatedBy, CreatedDate, Email) VALUES ('{newUser.NewUserName}', " +
                $"'{newUser.NewPassword}','{newUser.username}', NOW(),'{newUser.NewEmail}');";
               
            Object insertNewUser = dbSource.WriteData(sqlUser);
            // Insert into users table
            string sqlRoles = $"INSERT INTO hobby.roles (Role, CreatedBy, CreatedDate) " +
                $"VALUES ('{newUser.NewRole}', '{newUser.username}', NOW());";
            Object insertNewRole = dbSource.WriteData(sqlUser);
            // Create string for confirming user account
            string confirmUser = $"Select * from users where username = {newUser.NewUserName} " +
                $"and password = {newUser.NewPassword};";
            Object conUser = dbSource.WriteData(confirmUser);

            //Console.WriteLine("type of Reesult:" + confirmAdmin.GetType());
            OdbcDataReader reader = null;

            if (conUser.GetType() == typeof(OdbcDataReader))
            {
                reader = (OdbcDataReader)conUser;
            }

            // Create String to hold sql output
            string checkSql = "";

            // Read Sql query results
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0));
            }

            SqlDAO sqlDS = (SqlDAO)dbSource;
            Console.WriteLine("");

            // Closing the connection
            sqlDS.getConnection().Close();
            return true;
        }
        public bool EditUserRecord(UserAccount newUser, IDataSource<string> dbSource)
        {
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
