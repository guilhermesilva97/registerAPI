using RegisterAPI.Entity.Entities;

namespace RegisterAPI.Service.Interface
{
    public interface IClientService
    {
        Task<Guid> InsertClient(Client user);
        Task<Client> GetClientByDocument(string document);
        Task<IEnumerable<Client>> GetAll();
    }
}
