using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;
 
namespace myRetail.Models
{
    public class DataAccess
    {
        MongoClient _client;
        MongoServer _server;
        MongoDatabase _db;
 
        public DataAccess()
        {
            _client = new MongoClient("mongodb://mongo:27017");
            _server = _client.GetServer();
            _db = _server.GetDatabase("myretail");      
        }
 
        public IEnumerable<Product> GetAll()
        {
            return _db.GetCollection<Product>("Products").FindAll();
        }
 
        public Product Find(int targetid)
        {
            var res = Query<Product>.EQ(p=>p.targetid,targetid);
            return _db.GetCollection<Product>("Products").FindOne(res);
        }
 
        public Product Add(Product p)
        {
            _db.GetCollection<Product>("Products").Save(p);
            return p;
        }
 
        public void Update(Product p)
        {
            var res = Query<Product>.EQ(pd => pd.id,p.id);
            var operation = Update<Product>.Replace(p);
            _db.GetCollection<Product>("Products").Update(res,operation);
        }
        public void Remove(int targetid)
        {

            var res = Query<Product>.EQ(e => e.targetid, targetid);
            var operation = _db.GetCollection<Product>("Products").Remove(res);
        }
    }
}