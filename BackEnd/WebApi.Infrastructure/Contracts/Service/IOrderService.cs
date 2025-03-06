using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain;
using WebApi.Domain.Request;

namespace WebApi.Infrastructure.Contracts.Service
{
    public interface IOrderService
    {
        Task<List<Orders>> GetAllOrders();
        Task<List<Orders>> GetOrderById(int id);
        Task InsertOrder(string queueName, OrdersRequest order);
        Task DeleteOrderAsync(int id);
    }
}
