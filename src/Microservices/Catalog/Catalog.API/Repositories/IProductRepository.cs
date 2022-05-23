using Catalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProductById(string Id);
        public Task<IEnumerable<Product>> GetProductByName(string ProductName);
        public Task<IEnumerable<Product>> GetProductByCategory(string CategoryName);
        public Task CreateProduct(Product Product);
        public Task<bool> UpdateProduct(Product Product);
        public Task<bool> DeleteProduct(string Id);
    }
}
