using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace myRetail.Models
{
    public class Product
    {
		public Product() { }

		public ObjectId id { get; set; }
        
		[BsonElement("targetid")]
		public int targetid { get; set; }
        
		[BsonElement("name")]
		public string Name { get; set; }
        
		[BsonElement("current_price")]
		public CurrentPrice CurrentPrice { get; set; }
    }
}

public class CurrentPrice 
{
	
	[BsonElement("value")]
	public decimal Value {get; set;}
	
	[BsonElement("currency_code")]
	public string CurrencyCode {get; set;}
}