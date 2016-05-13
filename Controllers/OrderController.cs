using System;
using System.Collections.Generic;
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
