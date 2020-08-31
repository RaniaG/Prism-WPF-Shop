using E_Shop.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Core.Events
{
    public class AddToCartEvent:PubSubEvent<CartItemModel>
    {
    }
}
