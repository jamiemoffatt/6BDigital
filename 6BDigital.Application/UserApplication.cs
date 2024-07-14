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
        public User AuthenticateUser(string userName, string passwrd)
        {
            return new User { Name = "Moff", UserName = userName };
        }
    }
}
