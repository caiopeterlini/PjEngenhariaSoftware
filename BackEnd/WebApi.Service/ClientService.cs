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

        public async Task<Client> GetClientById(int id)
        {
            try
            {
                return await _clientRepository.GetClientById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(GetClientById)}: {ex.Message}", ex);
            }
        }

     
        public async Task<Client> InsertClient(Client client)
        {
            try
            {
                return await _clientRepository.InsertClient(client);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(InsertClient)}: {ex.Message}", ex);
            }
        }

        public async Task UpdateClient(Client client)
        {
            try
            {
                await _clientRepository.UpdateClient(client);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(UpdateClient)}: {ex.Message}", ex);
            }
        }

        public async Task DeleteClientAsync(int id)
        {
            try
            {
                await _clientRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(DeleteClientAsync)}: {ex.Message}", ex);
            }
        }

    }
}
