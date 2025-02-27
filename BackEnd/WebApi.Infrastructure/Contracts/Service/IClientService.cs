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
        public Task<List<Client>> GetAllClients();
        public Task<Client> GetClientById(int id);
        public Task<Client> UpdateClient(int id, Client client);
        public Task<Client> InsertClient(int id, Client client);
    }
}
