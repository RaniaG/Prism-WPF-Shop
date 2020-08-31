using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace E_Shop.DAL.XMLReader
{
    public class XMLReader:IXMLReader
    {
        public IEnumerable<T> Read<T>(string filePath)
        {
            XElement xelement = XElement.Load(filePath);
            IEnumerable<XElement> employees = xelement.Elements();
            List<T> result = new List<T>();
            foreach (var employee in employees)
            {
                result.Add(DeSerializer<T>(employee));
            }
            return result;
        }
        private T DeSerializer<T>(XElement element)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(element.CreateReader());
        }
    }
}
