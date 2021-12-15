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
        public bool EditUserRecord(UserAccount editUser, string CreatedBy, IDataSource<string> dbSource)
        {
            try
            {
                // Insert into users table
                string sqlUser = $"UPDATE users t SET t.Password = '{editUser.password}', " +
                    $"t.Role  = '{editUser.role}',t.Email = '{editUser.email}' " +
                    $"WHERE t.UserName = '{editUser.username}';";

                bool updateNewUser = dbSource.UpdateData(sqlUser);
                return updateNewUser;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteUserRecord(UserAccount deleteUser, string CreatedBy, IDataSource<string> dbSource)
        {
            try
            {
                // Insert into users table
                string sqlUser = $"DELETE from users WHERE UserName = '{deleteUser.username}' " +
                    $"and Password = '{deleteUser.password}';";

                bool deleteNewUser = dbSource.DeleteData(sqlUser);
                return deleteNewUser;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DisableUser(UserAccount disableUser, string CreatedBy, IDataSource<string> dbSource)
        {
            try
            {
                // Insert into users table
                string sqlUser = $"UPDATE users u SET u.IsActive = 0 WHERE u.UserName = '{disableUser.username}'" +
                     $"and u.Role = '{disableUser.role}';";

                bool disableNewUser = dbSource.UpdateData(sqlUser);
                return disableNewUser;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool EnableUser(UserAccount enableUser, string CreatedBy, IDataSource<string> dbSource)
        {
            try
            {
                // Insert into users table
                string sqlUser = $"UPDATE users u SET u.IsActive = 1 WHERE u.UserName = '{enableUser.username}'" +
                    $"and u.Role = '{enableUser.role}';";

                bool disableNewUser = dbSource.UpdateData(sqlUser);
                return disableNewUser;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
