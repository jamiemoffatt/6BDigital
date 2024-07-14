using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6BDigital.Domain.Interfaces
{
    public interface IUserData
    {
        User AuthenticateUser(string userName, string password);
    }
}
