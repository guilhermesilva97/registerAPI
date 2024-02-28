using Microsoft.EntityFrameworkCore;
using RegisterAPI.Entity.Entities;
using RegisterAPI.Repository.Context;
using RegisterAPI.Repository.Interface;

namespace RegisterAPI.Repository.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly RegisterAPIContext _context;

        public ClientRepository(RegisterAPIContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return false;
            }

            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _context.Client.ToListAsync();
        }

        public async Task<Client> GetByDocument(string documentClient)
        {
            return await _context.Client.FirstOrDefaultAsync(x => x.Document == documentClient);
        }

        public async Task<Client> GetById(int id)
        {
            return await _context.Client.FindAsync(id);
        }

        public async Task<Client> Insert(Client client)
        {
            _context.Client.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> Update(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(client.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return client;
        }

        private bool ClientExists(Guid id)
        {
            return _context.Client.Any(e => e.Id == id);
        }
    }
}
