using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamHobby.HobbyProjectGenerator.UserManagement
{   // Class for getting user credentials
    public class GetCredentials
    {
        public string? GetUserName()
        {
            Console.WriteLine("Please enter a username:");
            string? userName = Console.ReadLine();
            return userName;
        }
        public string? GetPassword()
        {
            Console.WriteLine("Please enter a password:");
            string? userPassword = Console.ReadLine();
            return userPassword;
        }
        public string ConfirmPassword()
        {
            while (true)
            {
                Console.WriteLine("Please enter a password:");
                string? userPassword = Console.ReadLine();
                // Confirm Password
                Console.WriteLine("Please re-enter the password:");
                string checkPsswd = Console.ReadLine();
                // Check if passwords match
                if (userPassword == checkPsswd)
                {
                    return userPassword;
                }
                else
                {
                    Console.WriteLine("Passwords do not match.\n");
                }
            }
        }
        public string GetEmail()
        {
            Console.WriteLine("Please enter an email:");
            return Console.ReadLine();
        }
        public string GetRole()
        {
            Console.WriteLine("Please enter the role of the user:");
            return Console.ReadLine();
        }
    }
    public class UserAccount
    {
        // Admin Credentials
        private string _userName;
        private string _password;
        private DateTime _Time;
        // New User Credentials
        private string _Email;
        private string _Role;

        public UserAccount(string un, string pwd, DateTime TimeStamp)
        {
            _userName = un;
            _password = pwd;
            _Time = TimeStamp;
        }
        public UserAccount(string un, string role)
        {
            _userName = un;
            _Role = role;
            _Time = DateTime.UtcNow;
        }
        public UserAccount(string newUN, string newPWD, string Email, string role, DateTime newTime)
        {
            _userName = newUN;
            _password = newPWD;
            _Role = role;
            _Email = Email;
            _Time = newTime;
        }
        public UserAccount()
        {
            _Role = "regular";
            _Time= DateTime.Now;
        }

        public string username { get { return _userName; } }
        public string password { get { return _password; } }
        public string email { get { return _Email; } }
        public string role { get { return _Role; } }
        public DateTime Time { get { return _Time; } }
    }
}
