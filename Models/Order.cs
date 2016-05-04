using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace enti_api.Models
{
    public class Order
    {
        public List<OrderItem> shoppingCart { get; set; }
        public Customer customer { get; set; }
    }
    
    public class OrderItem
    {
        public ShoppingItem item { get; set; }
        public int quantity { get; set; }
    }
}