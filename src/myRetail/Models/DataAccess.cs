using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
 
namespace myRetail.Models
{
    public class DataAccess
    {

		protected static IMongoClient _client;
		protected static IMongoDatabase _database;
		
        public DataAccess()
        {
            _client = new MongoClient("mongodb://mongo:27017");
            _database = _client.GetDatabase("myretail");      
        }
 
        public IEnumerable<Product> GetAll()
        {
			var filter = new BsonDocument();
            return _database.GetCollection<Product>("Products").Find(filter).ToList();
        }
 
        public Product Find(int targetid)
        {
			var filter = Builders<Product>.Filter.Eq("targetid", targetid);
			var result = _database.GetCollection<Product>("Products").Find(filter);
			if (result.Count() > 0) 
			{
				return result.First();
			}
			return null;
        }
 
        public Product Add(Product p)
        {
			_database.GetCollection<Product>("Products").InsertOne(p);
            return p;
        }
 
        public void Update(Product p)
        {
			// var filter = Builders<Product>.Filter.Eq("targetid", p.targetid);
			// var collection = _database.GetCollection<Product>("Products");
			// var update = Builders<Product>.Update
			// .Set("current_price.value", p.CurrentPrice.Value)
			// .Set("current_price.currency_code", p.CurrentPrice.Value);
			// collection.UpdateOne(filter, update);
			var filter = Builders<Product>.Filter.Eq("targetid", p.targetid);
			var collection = _database.GetCollection<Product>("Products");
			collection.ReplaceOne(filter, p);

        }
        public void Remove(int targetid)
        {
			var filter = Builders<Product>.Filter.Eq("targetid", targetid);
			var collection = _database.GetCollection<Product>("Products");
			collection.DeleteOne(filter);
        }
    }
}