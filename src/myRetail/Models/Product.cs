using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace myRetail.Models
{
    public class Product
    {
		public Product() { }

		[JsonIgnore]
		[BsonIgnoreIfDefault]
		public ObjectId id { get; set; }
        
		[BsonElement("targetid")]
		public int targetid { get; set; }
        
		[BsonElement("name")]
		public string Name { get; set; }
        
		[BsonElement("current_price")]
		[JsonProperty(PropertyName = "current_price")]
		public CurrentPrice CurrentPrice { get; set; }
    }
}


public class CurrentPrice 
{	
	[BsonElement("value")]
	[JsonProperty(PropertyName = "value")]
	public decimal Value {get; set;}
	
	[BsonElement("currency_code")]
	[JsonProperty(PropertyName = "currency_code")]
	public string CurrencyCode {get; set;}
}