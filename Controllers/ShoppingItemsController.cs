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
        public List<Models.ShoppingItem> Get(int? categoryId)
        {
            var items = new List<Models.ShoppingItem>();

            using (var db = new EntiTreesEntities())
            {
                var query = db.SelectShoppingItems(categoryId);

                foreach (var item in query)
                {
                    items.Add(new Models.ShoppingItem
                    {
                       id = item.Id,
                       description = item.Description,
                       discount = item.Discount,
                       lastUpdated = item.LastModified,
                       price = item.Price,
                       quantity = item.Quantity,
                       src = item.ImageSrc,
                       title = item.Title,
                       categoryId = item.CategoryId
                    });
                }
            }

            return items;
        }

        //create shopping item
        // POST: api/ShoppingItems
        public void Post([FromBody]Models.ShoppingItem item)
        {
            using (var db = new EntiTreesEntities())
            {
                db.InsertNewShopItem(item.title, item.description, item.price, item.discount, item.src, item.quantity, item.categoryId);               
            }
        }

        //update shopping item
        // PUT: api/ShoppingItems/5
        public void Put([FromBody]Models.ShoppingItem item)
        {
            using (var db = new EntiTreesEntities())
            {
                db.UpdateShopItem(item.id, item.title, item.description, item.price, item.discount, item.src, item.quantity, item.categoryId);
            }
        }

        // DELETE: api/ShoppingItems/5
        public void Delete(int id)
        {
            using (var db = new EntiTreesEntities())
            {
                db.DeleteShopItem(id);
            }
        }
    }
}
