using RegisterAPI.Entity.Entities;

namespace RegisterAPI.Repository.Interface
{
    public interface IIntegrationRepository
    {
        Task<IEnumerable<Integration>> GetAll();
        Task<Integration> GetIntegrationById(Guid integrationGuid);
    }
}
