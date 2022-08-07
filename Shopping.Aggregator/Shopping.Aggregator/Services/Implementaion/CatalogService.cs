using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services.Contracts;

namespace Shopping.Aggregator.Services.Implementaion
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var response = await _client.GetAsync("/api/v1/Catalog");
            return await response.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            var response = await _client.GetAsync($"/api/v1/Catalog/category/{category}");
            return await response.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> GetCatalogById(string id)
        {
            var response = await _client.GetAsync($"/api/v1/Catalog/{id}");
            return await response.ReadContentAs<CatalogModel>();
        }
    }
}