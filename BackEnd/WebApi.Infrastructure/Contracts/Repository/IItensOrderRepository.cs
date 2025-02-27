using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain;

namespace WebApi.Infrastructure.Contracts.Repository
{
    public interface IItensOrderRepository
    {

        public Task<List<ItensOrder>> GetAllItensOrders();
        public Task<ItensOrder> GetItensOrderById(int id);
        public Task<ItensOrder> InsertItensOrder(ItensOrder ItensOrder);
        public Task UpdateItensOrder(ItensOrder ItensOrder);
        public Task DeleteItensOrderAsync(int id);
    }
}
