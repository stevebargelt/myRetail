using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace myRetail.Models
{
    public class ProductRepository : IProductRepository
    {
        private static ConcurrentDictionary<string, Product> _Products =
              new ConcurrentDictionary<string, Product>();

        public ProductRepository()
        {
            Add(new Product { Name = "Item1" });
        }

        public IEnumerable<Product> GetAll()
        {
            return _Products.Values;
        }

        public void Add(Product item)
        {
            item.Key = Guid.NewGuid().ToString();
            _Products[item.Key] = item;
        }

        public Product Find(string key)
        {
            Product item;
            _Products.TryGetValue(key, out item);
            return item;
        }

        public Product Remove(string key)
        {
            Product item;
            _Products.TryRemove(key, out item);
            return item;
        }

        public void Update(Product item)
        {
            _Products[item.Key] = item;
        }
    }
}