using E_Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace E_Shop.DAL.Entities
{
    [XmlRoot("Users")]
    public class UsersXml
    {
        [XmlElement("User")]
        public List<User> Users { get; set; }
    }
}
