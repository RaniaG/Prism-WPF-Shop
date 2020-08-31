using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.DAL.XMLReader
{
    public interface IXMLWriter
    {
        void Write<T>(string filePath, string rootName, IEnumerable<T> items);
    }
}
