using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using myRetail.Models;

namespace myRetail.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        public ProductController(IProductRepository products)
        {
            Products = products;
        }
        public IProductRepository Products { get; set; }

		[HttpGet]
		public IEnumerable<Product> GetAll()
		{
			return Products.GetAll();
		}

		[HttpGet("{id}", Name = "GetProduct")]
		public IActionResult GetById(string id)
		{
			var item = Products.Find(id);
			if (item == null)
			{
				return NotFound();
			}
			return new ObjectResult(item);
		}
		[HttpPost]
		public IActionResult Create([FromBody] Product item)
		{
			if (item == null)
			{
				return BadRequest();
			}
			Products.Add(item);
			return CreatedAtRoute("GetProduct", new { id = item.Key }, item);
		}
		[HttpPut("{id}")]
		public IActionResult Update(string id, [FromBody] Product item)
		{
			if (item == null || item.Key != id)
			{
				return BadRequest();
			}

			var todo = Products.Find(id);
			if (todo == null)
			{
				return NotFound();
			}

			Products.Update(item);
			return new NoContentResult();
		}
 
		[HttpDelete("{id}")]
		public IActionResult Delete(string id)
		{
			var todo = Products.Find(id);
			if (todo == null)
			{
				return NotFound();
			}

			Products.Remove(id);
			return new NoContentResult();
		}
 
    }
}