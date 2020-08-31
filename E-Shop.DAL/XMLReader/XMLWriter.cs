using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace E_Shop.DAL.XMLReader
{
    public class XMLWriter:IXMLWriter
    {
        public void Write<T>(string filePath,string rootName, IEnumerable<T> items)
        {
            var root = new XElement(rootName);
            foreach (var item in items)
            {
                var element = Serializer(item);
                root.Add(element);
            }

        }

        private XElement Serializer<T>(T item)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(streamWriter, item);
                    return XElement.Parse(Encoding.ASCII.GetString(memoryStream.ToArray()));
                }
            }
        }
    }
}
