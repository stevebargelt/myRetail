using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using myRetail.Models;
using System.Net.Http;
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
    
		protected string redskyBase = "http://redsky.target.com/v1/pdp/tcin/13860428?excludes=taxonomy,price,promotion,bulk_ship,rating_and_review_reviews,rating_and_review_statistics,question_answer_statistics";

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
			
			Product product = null; 
			var item = objds.Find(id);
			if (item == null)
			{
				product = await GetTargetProduct();
				if (product == null)
				{ 
					return NotFound();
				}
				else
				{
					objds.Add(product);
				}
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
		public IActionResult Update(int targetid, [FromBody] Product item)
		{
			if (item == null || item.targetid != targetid)
			{
				return BadRequest();
			}

			var product = objds.Find(targetid);
			if (product == null)
			{
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
 
		private async Task<Product> GetTargetProduct()
		{
			// myRetail.Models.Target.Product model = null;
			// var task = await client.GetAsync(redskyBase);
			// System.Console.WriteLine("**********Task status code: " + task.StatusCode);
			// var jsonString = await task.Content.ReadAsStringAsync();
			// System.Console.WriteLine("**********JSON String: " + jsonString);
			// //filler
			// model = JsonConvert.DeserializeObject<myRetail.Models.Target.Product>(jsonString);
			// System.Console.WriteLine("**********JSON String: " + model.item.product_description.title);
			// return model;

			JObject o = null;
        	var client = new HttpClient();
        	var task = client.GetAsync(redskyBase)
          		.ContinueWith((taskwithresponse) =>
          		{
              		var response = taskwithresponse.Result;
              		var jsonString = response.Content.ReadAsStringAsync();
              		jsonString.Wait();
					System.Console.WriteLine("**********JSON String: " + jsonString.Result);
              		//model = JsonConvert.DeserializeObject<myRetail.Models.Target.Product>(jsonString.Result);
					o = JObject.Parse(jsonString.Result);
					
		          });
        	task.Wait();
			string prodid = (string)o["product"]["available_to_promise_network"]["product_id"];
			string name = (string)o["product"]["item"]["product_description"]["title"];
			System.Console.WriteLine("********** ID: " + prodid);
			System.Console.WriteLine("********** title: " + name);
			Product p = new Product {
				targetid = System.Convert.ToInt32(prodid),	
				Name = name
			};

			return p;
		}

    } //class
} // namespace