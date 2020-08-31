using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Entities.Interfaces.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string username);
        User GetUser(int id);
    }
}
