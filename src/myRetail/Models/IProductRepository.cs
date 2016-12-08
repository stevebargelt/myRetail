using System.Collections.Generic;

namespace myRetail.Models
{
    public interface IProductRepository
    {
        Product Add(Product item);
        IEnumerable<Product> GetAll();
        Product Find(string id);
        void Remove(string id);
        void Update(Product item);
    }
}