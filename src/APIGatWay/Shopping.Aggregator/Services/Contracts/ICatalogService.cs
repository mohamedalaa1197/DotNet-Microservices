using System.Collections.Generic;
using System.Threading.Tasks;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services.Contracts
{
    public interface ICatalogService
    {
        public Task<IEnumerable<CatalogModel>> GetCatalog();
        public Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);
        public Task<CatalogModel> GetCatalogById(string id);
    }
}