using WebApi.Domain;

namespace WebApi.Infrastructure.Contracts.Repository
{
    public interface IOrderRepository
    {
        public Task<List<Orders>> GetAllOrders();
        public Task<List<Orders>> GetOrderById(int id);
        public Task InsertOrderByQueue(string queueName, string body);
        public Task DeleteOrderAsync(int id);
    }
}
