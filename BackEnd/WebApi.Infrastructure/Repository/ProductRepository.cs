using Microsoft.EntityFrameworkCore;
using QueueMessage.Consumer.DataBase;
using WebApi.Domain;
using WebApi.Infrastructure.Contracts.Repository;

namespace WebApi.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySqlDbContext _mySqlDbContext;

        public ProductRepository(MySqlDbContext mySqlDbContext)
        {
            _mySqlDbContext = mySqlDbContext;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                return await _mySqlDbContext.product.ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Product?> GetProductById(int id)
        {
            try
            {
                return await _mySqlDbContext.product.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> InsertProduct(Product product)
        {
            try
            {
                await _mySqlDbContext.product.AddAsync(product);
                await _mySqlDbContext.SaveChangesAsync();
                return product; // Retorna o produto inserido com o Id gerado
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateProduct(Product product)
        {
            try
            {
                _mySqlDbContext.product.Update(product);
                await _mySqlDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            try
            {
                var product = await _mySqlDbContext.product.FindAsync(id);
                if (product != null)
                {
                    _mySqlDbContext.product.Remove(product);
                    await _mySqlDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }


    }

}
