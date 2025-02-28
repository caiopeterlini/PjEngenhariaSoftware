using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain;

namespace WebApi.Infrastructure.Contracts.Service
{
    public interface IClientService
    {
        Task<List<Client>> GetAllClients();
        Task<Client> GetClientById(int id);
        Task<Client> InsertClient(Client client);
        Task UpdateClient(Client client);
        Task DeleteClientAsync(int id);
    }
}
