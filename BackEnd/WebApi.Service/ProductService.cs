using WebApi.Domain;
using WebApi.Infrastructure.Contracts.Repository;
using WebApi.Infrastructure.Contracts.Service;

namespace WebApi.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                return await _productRepository.GetAllProducts();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(GetAllProducts)}: {ex.Message}", ex);
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            try
            {
                return await _productRepository.GetProductById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(GetProductById)}: {ex.Message}", ex);
            }
        }

        public async Task<Product> InsertProduct(Product product)
        {
            try
            {
                return await _productRepository.InsertProduct(product);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(InsertProduct)}: {ex.Message}", ex);
            }
        }

        public async Task UpdateProduct(Product product)
        {
            try
            {
                await _productRepository.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(UpdateProduct)}: {ex.Message}", ex);
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            try
            {
                await _productRepository.DeleteProductAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(DeleteProductAsync)}: {ex.Message}", ex);
            }
        }
    }
}
