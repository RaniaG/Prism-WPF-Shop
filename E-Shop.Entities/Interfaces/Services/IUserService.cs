using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Entities.Interfaces.Services
{
    public interface IUserService
    {
        User GetUser(string username);
        User GetUser(int id);
        bool Login(string username);
        User GetCurrentUser();
    }
}
