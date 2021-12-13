﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamHobby.HobbyProjectGenerator.UserManagement
{
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
    }
    public class UserAccount
    {
        // Admin Credentials
        private string _userName;
        private string _password;
        private DateTime _logginTime;

        public UserAccount(string un, string pwd, DateTime TimeStamp)
        {
            _userName = un;
            _password = pwd;
            _logginTime = TimeStamp;
        }
        public string username { get { return _userName; } } 
        public string password { get { return _password; } }

        // New User Credentials
        private string _newUserName;
        private string _newPassword;
        private string _newEmail;
        private DateTime _newTime;
        public UserAccount(string newUN, string newPWD, string Email, DateTime newTime)
        {
            _newUserName = newUN;
            _newPassword = newPWD;
            _newEmail = Email;
            _newTime = newTime;
        }
        public string NewUserName { get { return _newUserName; } }
        public string NewPassword { get { return _newPassword; } }
        public string NewEmail { get { return _newEmail; } }
        public DateTime Newtime { get { return _newTime; } }
    }
}
