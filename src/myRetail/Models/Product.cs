using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace myRetail.Models
{
    public class Product
    {
		public ObjectId mongoid { get; set; }
        
		[BsonElement("id")]
		public int id { get; set; }
        
		[BsonElement("name")]
		public string Name { get; set; }
        
		[BsonElement("current_price")]
		public CurrentPrice CurrentPrice { get; set; }
    }
}

public class CurrentPrice 
{
	
	[BsonElement("value")]
	public float Value {get; set;}
	
	[BsonElement("currency_code")]
	public string CurrencyCode {get; set;}
}