using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using enti_api.Models;

namespace enti_api.Controllers
{
    public class CategoriesController : ApiController
    {

        // GET: api/Categories/5
        public List<Models.Category> Get()
        {
            var cat = new Models.Category
            {
                id = 1,
                description = "asdf",
                imageSrc = "1.jpg",
                itemsCount = 5,
                name = "test",
                src = "shop/soil",
                systemName = "soil"
            };
            var list = new List<Models.Category>();
            list.Add(cat);
            return list;
        }
    }
}
