using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ProductController : ApiController
    {
       List<Product> products = new List<Product>()
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 4 },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 17 }
        };

        // GET: api/Product
        public IHttpActionResult GetAll()
        {
            if (!products.Any())
            {
                return NotFound();
            }

            return Ok(products);
        }

        // GET: api/Product/5
        public IHttpActionResult GetById(int id)
        {
            Product product = null;

            product = products.Where(p => p.Id == id).FirstOrDefault();

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Product
        public IHttpActionResult Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            products.Add(product);

            return Ok();
        }

        // PUT: api/Product/5
        public IHttpActionResult Put(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            var existingProduct = products.Where(p => p.Id == product.Id).FirstOrDefault();

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Category = product.Category;
                existingProduct.Price = product.Price;
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        // DELETE: api/Product/5
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not a valid student id");
            }

            var product = products.Where(p => p.Id == id).FirstOrDefault();

            products.Remove(product);

            return Ok();
        }
    }
}
