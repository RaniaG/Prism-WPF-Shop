using E_Shop.DAL.Consts;
using E_Shop.DAL.XMLReader;
using E_Shop.Entities;
using E_Shop.Entities.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IXMLReader _xMLReader;
        private IEnumerable<User> _users;
        public UserRepository(IXMLReader xMLReader)
        {
            _xMLReader = xMLReader;
            ReadAll();
        }

        private void ReadAll()
        {
            _users = _xMLReader.Read<User>(DataFilesPaths.Users);
        }

        public User GetUser(string username)
        {
            return _users.FirstOrDefault(e => e.Name == username);
        }

        public User GetUser(int id)
        {
            return _users.FirstOrDefault(e => e.Id == id);
        }
    }
}
