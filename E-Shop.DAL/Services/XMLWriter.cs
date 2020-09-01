using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace E_Shop.DAL.Services
{
    public class XMLWriter:IXMLWriter
    {
        public void Write<T>(string filePath, T root)
        {
            XElement rootElement = Serializer(root);
            rootElement.Save(filePath);
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
