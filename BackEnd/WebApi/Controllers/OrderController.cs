using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using WebApi.Domain;
using WebApi.Domain.Request;
using WebApi.Infrastructure.Contracts.MessageQueue;
using WebApi.Infrastructure.Contracts.Service;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private ILogger<OrderController> _logger;
        private IOrderService _orderService;
        private string _QueueName;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
            _QueueName = "order";
        }


        [HttpPost]
        public async Task<IActionResult> InsertOrderAsync([FromBody] OrdersRequest order)
        {
            var responseQueue = "";
            try
            {
                 await _orderService.InsertOrder(_QueueName, order);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in InsertOrder" + ex);
                return BadRequest(ex.Message);
            }
           return Accepted(responseQueue);
        }


        [HttpGet("GetOrderById")]
        public async Task<ActionResult<List<Orders>?>> GetOrderById([FromBody] int id)
        {
            try
            {
                var orders = await _orderService.GetOrderById(id);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


    [HttpGet]
        public async Task<ActionResult<List<Orders>>> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
