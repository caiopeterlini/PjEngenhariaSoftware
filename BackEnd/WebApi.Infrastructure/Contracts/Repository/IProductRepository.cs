using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain;

namespace WebApi.Infrastructure.Contracts.Repository
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllProducts();
        public Task<Product> GetProductById(int id);
        public Task<Product> InsertProduct(Product client);
        public Task UpdateProduct(Product client);
        public Task DeleteProductAsync(int id);
    }
}
