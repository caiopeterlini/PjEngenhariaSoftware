using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain;
using WebApi.Infrastructure.Contracts.Repository;
using WebApi.Infrastructure.Contracts.Service;
using WebApi.Infrastructure.Repository;

namespace WebApi.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IItensOrderRepository _itensOrderRepository;

        public OrderService(IOrderRepository orderRepository, IItensOrderRepository itensOrderRepository)
        {
            _orderRepository = orderRepository;
            _itensOrderRepository = itensOrderRepository;
        }

        public async Task<List<Orders>> GetAllOrders()
        {
            try
            {
                return await _orderRepository.GetAllOrders();
            }
            catch (Exception ex)
            {
                // Trate a exceção conforme necessário
                throw new Exception("Erro ao obter todos os pedidos", ex);
            }
        }

        public async Task<Orders> GetOrderById(int id)
        {
            try
            {
                return await _orderRepository.GetOrderById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(GetOrderById)}: {ex.Message}", ex);
            }
        }


        public async Task InsertOrder(string queueName, string body)
        {
            try
            {
                await _orderRepository.InsertOrderByQueue(queueName, body);
            }
            catch (Exception ex)
            {
                // Trate a exceção conforme necessário
                throw new Exception(ex.Message);
            }

        }

        public async Task DeleteOrderAsync(int id)
        {
            try
            {
                await _orderRepository.DeleteOrderAsync(id);
            }
            catch (Exception ex)
            {
                // handle exception
                throw new ApplicationException($"An error occurred while deleting the order with ID {id}.", ex);
            }
        }

      


    }
}