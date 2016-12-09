using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using myRetail.Models;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;



namespace myRetail.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
		DataAccess objds;

		protected string redskyBase = "http://redsky.target.com/v1/pdp/tcin/{0}?excludes=taxonomy,price,promotion,bulk_ship,rating_and_review_reviews,rating_and_review_statistics,question_answer_statistics";

		static HttpClient client = new HttpClient();

        public ProductController(DataAccess d)
        {
            objds = d;
			client.BaseAddress = new System.Uri(redskyBase);
			client.DefaultRequestHeaders.Accept.Clear();
    		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //public IProductRepository Products { get; set; }

		[HttpGet]
		public IEnumerable<Product> GetAll()
		{
			return objds.GetAll();
		}

		[HttpGet("{id}", Name = "GetProduct")]
		public async Task<IActionResult> GetById(int id)
		{
			Console.WriteLine("****** GetById(int id) | id (param) = " + id);
			Product product = null; 
			var item = objds.Find(id);
			if (item == null)
			{
				var redskyProduct = string.Format(redskyBase, id);
				System.Console.WriteLine("**********URL: " + redskyProduct);
				JObject o = null;
				var client = new HttpClient();

				HttpResponseMessage response = await client.GetAsync(redskyProduct);
				//var task = client.GetAsync(redskyProduct)
				if (response.IsSuccessStatusCode)
				{
					var jsonString = await response.Content.ReadAsStringAsync();
					System.Console.WriteLine("**********JSON String: " + jsonString);
					o = JObject.Parse(jsonString);
								string prodid = (string)o["product"]["available_to_promise_network"]["product_id"];
					string name = (string)o["product"]["item"]["product_description"]["title"];
					System.Console.WriteLine("********** ID: " + prodid);
					System.Console.WriteLine("********** title: " + name);
					product = new Product();
					product.targetid = System.Convert.ToInt32(prodid);	
					product.Name = name;
				}	
				else
				{
					switch (response.StatusCode)
					{
						case HttpStatusCode.Forbidden : 
							return StatusCode(403);
							break;
						case HttpStatusCode.NotFound :
							return StatusCode(401);
							break;
						default :
							return StatusCode(401);
							break;
					}
			
				}

				if (product == null)
				{ 
					return NotFound();
				}
				else
				{
					objds.Add(product);
				}
			}

			return new ObjectResult(product);
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
		public IActionResult Update(int id, [FromBody] Product item)
		{
			Console.WriteLine("****** item.targetid =" + item.targetid);
			Console.WriteLine("****** id (param) =" + id);
			Console.WriteLine("****** new price" + item.CurrentPrice.Value);
			
			if (item == null || item.targetid != id)
			{
				Console.WriteLine("****** Return Bad Request");
				return BadRequest();
			}

			var product = objds.Find(id);
			if (product == null)
			{
				Console.WriteLine("****** Return Not Found");
				return NotFound();
			}
			

			objds.Update(item);
			return new NoContentResult();
		}
 
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var product = objds.Find(id);
			if (product == null)
			{
				return NotFound();
			}

			objds.Remove(id);
			return new NoContentResult();
		}
 
		private async Task<IActionResult> GetTargetProduct(string targetid)
		{
			var redskyProduct = string.Format(redskyBase, targetid);
			System.Console.WriteLine("**********URL: " + redskyProduct);
			JObject o = null;
			Product p = null;
        	var client = new HttpClient();

			HttpResponseMessage response = await client.GetAsync(redskyProduct);
        	//var task = client.GetAsync(redskyProduct)
			if (response.IsSuccessStatusCode)
			{
            	var jsonString = await response.Content.ReadAsStringAsync();
              	System.Console.WriteLine("**********JSON String: " + jsonString);
            	o = JObject.Parse(jsonString);
							string prodid = (string)o["product"]["available_to_promise_network"]["product_id"];
				string name = (string)o["product"]["item"]["product_description"]["title"];
				System.Console.WriteLine("********** ID: " + prodid);
				System.Console.WriteLine("********** title: " + name);
				p.targetid = System.Convert.ToInt32(prodid);	
				p.Name = name;
			}	
			else
			{
				if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
				{
					return new ForbidResult();
				}
			}

			return new ObjectResult(p);
		}

    } //class
} // namespace