﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Services
{
    public interface IMessageResourceManager
    {
        string GetMessage(string key);
    }
}