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
        public string Post(Models.Order order)
        {
            string inXml = Utils.ObjectToXML(order);
            using (var db = new EntiTreesEntities())
            {                
                try
                {
                    var result = db.InsertNewOrder(inXml);

                    foreach (var item in result)
                    {
                        if (item.Result == "201")
                        {
                            return item.ResultMessage;
                        }
                        else {
                            return item.ResultMessage;
                        }
                    }
                    return "";
                }
                catch (Exception ex)
                {
                    return ex.Message;
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
