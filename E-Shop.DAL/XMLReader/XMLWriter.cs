using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace E_Shop.DAL.XMLReader
{
    public class XMLWriter:IXMLWriter
    {
        public void Append<T>(string filePath, IEnumerable<T> items, string itemsParentNode=null)
        {
            XElement root = XElement.Load(filePath);

            XElement parentItem = null;
            if (!string.IsNullOrEmpty(itemsParentNode))
                parentItem = new XElement(itemsParentNode);

            foreach (var item in items)
            {
                var element = Serializer(item);
                if (parentItem != null)
                    parentItem.Add(element);
                else
                    root.Add(element);
            }

            if (parentItem != null)
                root.Add(parentItem);

            root.Save(filePath);
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
