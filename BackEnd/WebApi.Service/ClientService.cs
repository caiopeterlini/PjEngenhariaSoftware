using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain;
using WebApi.Infrastructure.Contracts.Repository;
using WebApi.Infrastructure.Contracts.Service;

namespace WebApi.Service
{
    public class ClientService : IClientService
    {

        private IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async  Task<List<Client>> GetAllClients()
        {
            try
            {
            return await _clientRepository.GetAllClients();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Client> GetClientById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Client> InsertClient(int id, Client client)
        {
            throw new NotImplementedException();
        }

        public Task<Client> UpdateClient(int id, Client client)
        {
            throw new NotImplementedException();
        }
    }
}
