using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace enti_api.Controllers
{
    public class ShoppingItemsController : ApiController
    {
       
        // GET: api/ShoppingItems/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ShoppingItems
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ShoppingItems/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ShoppingItems/5
        public void Delete(int id)
        {
        }
    }
}
