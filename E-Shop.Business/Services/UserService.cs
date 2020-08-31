using E_Shop.DAL.XMLReader;
using E_Shop.Entities;
using E_Shop.Entities.Interfaces.Repositories;
using E_Shop.Entities.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User GetUser(string username)
        {
            return _userRepository.GetUser(username);
        }

        public User GetUser(int id)
        {
            return _userRepository.GetUser(id);
        }
    }
}
