using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using myRetail.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace myRetail.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
		DataAccess objds;
    
		protected string redskyBase = "http://redsky.target.com/v1/pdp/tcin/{1}?excludes=taxonomy,price,promotion,bulk_ship,rating_and_review_reviews,rating_and_review_statistics,question_answer_statistics";

        public ProductController(DataAccess d)
        {
            objds = d;
        }

        //public IProductRepository Products { get; set; }

		[HttpGet]
		public IEnumerable<Product> GetAll()
		{
			return objds.GetAll();
		}

		[HttpGet("{id}", Name = "GetProduct")]
		public IActionResult GetById(string id)
		{
			//HttpClient httpClient = new HttpClient();
        	//string url = string.Format(redskyBase, "13860428"); 
			//var response = httpClient.GetAsync(url).Result;
            //var obj = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
			
			var item = objds.Find(id);
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
			objds.Add(item);
			return CreatedAtRoute("GetProduct", new { id = item.id }, item);
		}
		[HttpPut("{id}")]
		public IActionResult Update(string id, [FromBody] Product item)
		{
			if (item == null || item.id != id)
			{
				return BadRequest();
			}

			var todo = objds.Find(id);
			if (todo == null)
			{
				return NotFound();
			}

			objds.Update(item);
			return new NoContentResult();
		}
 
		[HttpDelete("{id}")]
		public IActionResult Delete(string id)
		{
			var todo = objds.Find(id);
			if (todo == null)
			{
				return NotFound();
			}

			objds.Remove(id);
			return new NoContentResult();
		}
 
    }
}