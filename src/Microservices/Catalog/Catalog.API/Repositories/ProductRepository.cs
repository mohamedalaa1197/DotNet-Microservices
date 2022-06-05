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

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>
                                                   .Filter.Eq(p => p.Category, categoryName);
            return await _Context
                                .Products
                                .Find(filter)
                                .ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _Context
                           .Products
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string productName)
        {
            FilterDefinition<Product> filter = Builders<Product>
                                                    .Filter.Eq(p => p.Name, productName);
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

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _Context
                                    .Products
                                    .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
