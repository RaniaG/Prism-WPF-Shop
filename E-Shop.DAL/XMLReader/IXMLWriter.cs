using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.DAL.XMLReader
{
    public interface IXMLWriter
    {
        void Append<T>(string filePath, IEnumerable<T> items, string itemsParentNode = null);
    }
}
