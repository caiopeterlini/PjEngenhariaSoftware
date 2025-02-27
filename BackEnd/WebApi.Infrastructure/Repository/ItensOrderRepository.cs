using Microsoft.EntityFrameworkCore;
using QueueMessage.Consumer.DataBase;
using WebApi.Domain;
using WebApi.Infrastructure.Contracts.Repository;

namespace WebApi.Infrastructure.Repository
{
    public class ItensOrderRepository : IItensOrderRepository
    {
        private readonly MySqlDbContext _mySqlDbContext;

        public ItensOrderRepository(MySqlDbContext mySqlDbContext)
        {
            _mySqlDbContext = mySqlDbContext;
        }

        public async Task<List<ItensOrder>> GetAllItensOrders()
        {
            try
            {
                return await _mySqlDbContext.item_order.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ItensOrder?> GetItensOrderById(int id)
        {
            try
            {
                return await _mySqlDbContext.item_order.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ItensOrder> InsertItensOrder(ItensOrder itensOrder)
        {
            try
            {
                await _mySqlDbContext.item_order.AddAsync(itensOrder);
                await _mySqlDbContext.SaveChangesAsync();
                return itensOrder; // Retorna o item do pedido inserido com o Id gerado
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateItensOrder(ItensOrder itensOrder)
        {
            try
            {
                _mySqlDbContext.item_order.Update(itensOrder);
                await _mySqlDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteItensOrderAsync(int id)
        {
            try
            {
                var itensOrder = await _mySqlDbContext.item_order.FindAsync(id);
                if (itensOrder != null)
                {
                    _mySqlDbContext.item_order.Remove(itensOrder);
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
