using RegisterAPI.Entity.Entities;

namespace RegisterAPI.Repository.Interface
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAll();
        Task<Client> Insert(Client user);
        Task<Client> Update(Client user);
        Task<bool> Delete(int id);
        Task<Client> GetById(int id);
        Task<Client> GetByDocument(string document);
    }
}
