using E_Shop.Core.Entities;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Core.Events
{
    public class UpdateCartEvent:PubSubEvent<CartEventItem>
    {
    }
}
