using E_Shop.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Services
{
    public class MessageResourceManager : IMessageResourceManager
    {
        private readonly ResourceManager _resourceManager;
        public MessageResourceManager()
        {
            _resourceManager = new ResourceManager("E_Shop.Resources.Messages", typeof(Messages).Assembly);
        }
        public string GetMessage(string key)
        {
            return _resourceManager.GetString(key);
        }
    }
}
