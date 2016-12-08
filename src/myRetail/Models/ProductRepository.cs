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

        public Product Add(Product item)
        {
            //item.id = Guid.NewGuid().ToString();
            _Products[item.id] = item;
			return item;
        }

        public Product Find(string id)
        {
            Product item;
            _Products.TryGetValue(id, out item);
            return item;
        }

        public void Remove(string id)
        {
            Product item;
            _Products.TryRemove(id, out item);
            //return item;
        }

        public void Update(Product item)
        {
            _Products[item.id] = item;
        }
    }
}