using QueueMessage.Consumer.DataBase;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain;
using WebApi.Infrastructure.Contracts.Repository;

namespace WebApi.Infrastructure.Repository
{
    public class ClientRepository : IClientRepository
    {

        private MySqlDbContext _mySqlDbContext;

        public ClientRepository(MySqlDbContext mySqlDbContext)
        {
            this._mySqlDbContext = mySqlDbContext;
        }

        public async Task<List<Client>> GetAllClients()
        {
            try
            {
                return await _mySqlDbContext.client.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Client?> GetClientById(int id)
        {
            try
            {
                return await _mySqlDbContext.client.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Client> InsertClient(Client client)
        {
            try
            {
                await _mySqlDbContext.client.AddAsync(client);
                await _mySqlDbContext.SaveChangesAsync();

                return client;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateClient(Client client)
        {
            _mySqlDbContext.client.Update(client);
            await _mySqlDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _mySqlDbContext.client.FindAsync(id);
            if (client != null)
            {
                 _mySqlDbContext.client.Remove(client);
                await _mySqlDbContext.SaveChangesAsync();
            }
        }

        
    }
}
