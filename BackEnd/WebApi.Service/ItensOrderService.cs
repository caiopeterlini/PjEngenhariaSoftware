using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain;
using WebApi.Infrastructure.Contracts.Repository;

namespace WebApi.Service
{
    public class ItensOrderService
    {
        private readonly IItensOrderRepository _itensOrderRepository;

        public ItensOrderService(IItensOrderRepository itensOrderRepository)
        {
            _itensOrderRepository = itensOrderRepository;
        }

        public async Task<List<ItensOrder>> GetAllItensOrdersAsync()
        {
            try
            {
                return await _itensOrderRepository.GetAllItensOrders();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while getting all itens orders.", ex);
            }
        }

        public async Task<ItensOrder> GetItensOrderByIdAsync(int id)
        {
            try
            {
                return await _itensOrderRepository.GetItensOrderById(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while getting the itens order with ID {id}.", ex);
            }
        }

        public async Task<ItensOrder> InsertItensOrderAsync(ItensOrder itensOrder)
        {
            try
            {
                return await _itensOrderRepository.InsertItensOrder(itensOrder);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while inserting the itens order.", ex);
            }
        }

        public async Task UpdateItensOrderAsync(ItensOrder itensOrder)
        {
            try
            {
                await _itensOrderRepository.UpdateItensOrder(itensOrder);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the itens order.", ex);
            }
        }

        public async Task DeleteItensOrderAsync(int id)
        {
            try
            {
                await _itensOrderRepository.DeleteItensOrderAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while deleting the itens order with ID {id}.", ex);
            }
        }
    }

}
