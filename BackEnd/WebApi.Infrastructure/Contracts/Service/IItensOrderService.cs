using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain;

namespace WebApi.Infrastructure.Contracts.Service
{
    public interface IItensOrderService
    {
        Task<List<ItensOrder>> GetAllItensOrdersAsync();
        Task<ItensOrder> GetItensOrderByIdAsync(int id);
        Task<ItensOrder> InsertItensOrderAsync(ItensOrder itensOrder);
        Task UpdateItensOrderAsync(ItensOrder itensOrder);
        Task DeleteItensOrderAsync(int id);
    }
}
