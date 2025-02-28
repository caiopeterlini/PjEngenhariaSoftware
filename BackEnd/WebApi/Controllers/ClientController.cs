using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain;
using WebApi.Infrastructure.Contracts.Service;
using WebApi.Service;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clientService.GetAllClients();

            return Ok(clients);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClientById([FromQuery] int id)
        {
            var client = await _clientService.GetClientById(id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult<Client>> InsertClient([FromBody] Client client)
        {
            var newClient = await _clientService.InsertClient(client);
            return CreatedAtAction(nameof(GetClientById), new { id = newClient.Id }, newClient);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient([FromBody] Client client)
        {
            await _clientService.UpdateClient(client);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient([FromQuery] int id)
        {
            await _clientService.DeleteClientAsync(id);
            return NoContent();
        }
    }
}

