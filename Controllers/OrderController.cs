using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using enti_api.Models;

namespace enti_api.Controllers
{
    public class OrderController : ApiController
    {
        // GET: api/Order
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Order/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Order
        //Content-Type: application/json
        //{"shoppingCart": [{"quantity": "2", "item": {"title":"Test title"}}], "customer": {"name":"Lily", "email":"test@abv.bg", "phone": "9847", "address": "asdf", "message":"msg msg"}}
        public void Post(Order order)
        {

        }

        //// PUT: api/Order/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Order/5
        //public void Delete(int id)
        //{
        //}
    }
}
