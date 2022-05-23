using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ICatalogContext _Context { get; }
        public ProductRepository(ICatalogContext Context)
        {
            _Context = Context ?? throw new ArgumentNullException(nameof(Context));
        }


        public async Task CreateProduct(Product Product)
        {
            await _Context.Products.InsertOneAsync(Product);
        }

        public async Task<bool> DeleteProduct(string Id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, Id);
            var deleteResult = await _Context
                                        .Products
                                        .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                                && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string CategoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>
                                                   .Filter.Eq(p => p.Category, CategoryName);
            return await _Context
                                .Products
                                .Find(filter)
                                .ToListAsync();
        }

        public async Task<Product> GetProductById(string Id)
        {
            return await _Context
                           .Products
                           .Find(p => p.Id == Id)
                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string ProductName)
        {
            FilterDefinition<Product> filter = Builders<Product>
                                                    .Filter.Eq(p => p.Name, ProductName);
            return await _Context
                                .Products
                                .Find(filter)
                                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _Context
                        .Products
                        .Find(p => true)
                        .ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product Product)
        {
            var UpdateResult = await _Context
                                    .Products
                                    .ReplaceOneAsync(filter: g => g.Id == Product.Id, replacement: Product);
            return UpdateResult.IsAcknowledged && UpdateResult.ModifiedCount > 0;
        }
    }
}
