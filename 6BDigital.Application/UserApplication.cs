using _6BDigital.Domain;
using _6BDigital.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6BDigital.Application
{
    public class UserApplication : IUserApplication
    {
        private IUserData _userData;

        public UserApplication(IUserData userData)
        {
            _userData = userData;
        }

        public User AuthenticateUser(string userName, string password)
        {
            return _userData.AuthenticateUser(userName, password);
        }
    }
}
