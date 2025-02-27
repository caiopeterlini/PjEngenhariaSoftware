using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain;

namespace WebApi.Infrastructure.Contracts.Repository
{
    public interface IClientRepository
    {
        public Task<List<Client>> GetAllClients();
        public Task<Client> GetClientById(int id);
        public Task<Client> InsertClient(Client client);
        public Task UpdateClient(Client client);
        public Task DeleteAsync(int id);
    }
}
