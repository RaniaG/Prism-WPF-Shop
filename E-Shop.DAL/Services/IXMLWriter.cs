using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.DAL.Services
{
    public interface IXMLWriter
    {
        void Write<T>(string filePath, T root);
    }
}
