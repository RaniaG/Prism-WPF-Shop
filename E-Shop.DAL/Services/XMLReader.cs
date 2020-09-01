using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace E_Shop.DAL.Services
{
    public class XMLReader:IXMLReader
    {
        public T Read<T>(string filePath)
        {
            XElement xelement = XElement.Load(filePath);
            return DeSerializer<T>(xelement);
        }
        private T DeSerializer<T>(XElement element)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(element.CreateReader());
        }
    }
}
