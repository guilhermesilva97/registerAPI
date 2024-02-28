using Microsoft.EntityFrameworkCore;
using RegisterAPI.Entity.Entities;
using RegisterAPI.Repository.Context;
using RegisterAPI.Repository.Interface;

namespace RegisterAPI.Repository.Repository
{
    public class IntegrationRepository : IIntegrationRepository
    {
        private readonly RegisterAPIContext _context;
        public IntegrationRepository(RegisterAPIContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Integration>> GetAll()
        {
            return await _context.Integration.ToListAsync();
        }

        public async Task<Integration> GetIntegrationById(Guid integrationGuid)
        {
            return await _context.Integration.FirstAsync(x=> x.Id == integrationGuid);
        }
    }
}
