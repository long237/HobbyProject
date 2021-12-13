using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamHobby.HobbyProjectGenerator.UserManagement
{
    public class UserAccount
    {
        // Admin Credentials
        private string UserName;
        private string Password;
        private DateTime Time;

        public UserAccount(string un, string pwd, DateTime TimeStamp)
        {
            UserName = un;
            Password = pwd;
            Time = TimeStamp;
        }
        public string username { get { return UserName; } } 
        public string password { get { return Password; } }

        // New User Credentials
        private string newusername;
        private string newpassword;
        private string newemail;
        private DateTime newtime;
        public void NewUser(string newUN, string newPWD, string Email, DateTime newTime)
        {
            newusername = newUN;
            newpassword = newPWD;
            newemail = Email;
            newtime = newTime;
        }
        public string NewUserName { get { return newusername; } }
        public string NewPassword { get { return newpassword; } }
        public string NewEmail { get { return newemail; } }
        public DateTime Newtime { get { return newtime;} }
    }
}
