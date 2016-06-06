﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using enti_api.Code;

namespace enti_api.Controllers
{
    public class OrderController : ApiController
    {
        // GET: api/Order
        [BasicAuthenticationAttribute]
        public List<Models.Orders> Get()
        {
            var orders = new List<Models.Orders>();

            using (var db = new EntiTreesEntities())
            {
                var query = db.SelectOrders();

                foreach (var item in query)
                {
                    orders.Add(new Models.Orders
                    {
                        orderId = item.Id,
                        customerId = item.CustomerId,
                        shoppingCartId = item.ShoppingCartId,
                        orderDate = item.Date,
                        isCompleted = item.IsCompleted,
                        itemsCount = item.Quantity == null ? 0 : item.Quantity.Value,
                        message = item.Message,
                        totalPrice = item.TotalPrice == null ? 0 : item.TotalPrice.Value
                    });
                }
            }

            return orders;
        }

        // GET: api/Order/5
        public Models.OrderDetails Get(int orderId)
        {
            var order = new Models.OrderDetails();

            using (var db = new EntiTreesEntities())
            {
                var query = db.SelectOrderByID(orderId);
                foreach (var item in query)
                {
                    order.orderId = item.OrderId;
                    order.shoppingCartId = item.ShoppingCartId;
                    order.customerId = item.CustomerId;
                    order.orderDate = item.Date;
                    order.isCompleted = item.IsCompleted;
                    order.customer = new Models.Customer
                    {
                        message = item.Message,
                        name = item.Name,
                        phone = item.Phone,
                        email = item.Email,
                        address = item.Address
                    };
                }

                var query2 = db.SelectOrderItemsByOrderID(orderId);
                order.items = new List<Models.OrderDetails.OrderItem>();
                foreach (var item in query2)
                {
                    order.items.Add(new Models.OrderDetails.OrderItem
                    {
                        shoppingItemId = item.ShoppingItemId,
                        categoryName = item.Name,
                        discount = item.Discount == null ? 0 : item.Discount.Value,
                        imageSrc = item.ImageSrc,
                        itemPrice = item.SinglePrice == null ? 0 : item.SinglePrice.Value,
                        quantity = item.Quantity,
                        sellPrice = item.Price,
                        title = item.Title
                    });
                }
            }
           
            return order;
        }

        private Func<ObjectResult<object>> ObjectResult()
        {
            throw new NotImplementedException();
        }

        // POST: api/Order
        //Content-Type: application/json
        public Models.ReturnValue<string> Post(Models.Order order)
        {
            string inXml = Utils.ObjectToXML(order);
            using (var db = new EntiTreesEntities())
            {                
                try
                {
                    foreach(Models.OrderItem item in order.shoppingCart)
                    {
                        if(item.quantity > item.item.quantity)
                        {
                            return new Models.ReturnValue<string>(Models.Codes.NotEnoughQty, "There is not enough quantity for this item " + item.item.title);
                        }
                    }

                    var result = db.InsertNewOrder(inXml);
                    Models.ReturnValue<string> returnVal;

                    foreach (var item in result)
                    {
                        Models.Codes code = (Models.Codes)Enum.Parse(typeof(Models.Codes), item.Result);
                        returnVal = new Models.ReturnValue<string>(code, item.ResultMessage);
                        return returnVal;                        
                    }
                    return new Models.ReturnValue<string>(Models.Codes.DBError, "There is something wrong with the DB");
                }
                catch (Exception ex)
                {
                    return new Models.ReturnValue<string>(Models.Codes.Error, ex.Message);
                }
            }
        }

        // PUT: api/Order/5
        // Update of order status
        public void Put(int id, bool isCompleted)
        {
            using (var db = new EntiTreesEntities())
            {
                db.UpdateOrder(id, isCompleted);
            }
        }


    }
}
