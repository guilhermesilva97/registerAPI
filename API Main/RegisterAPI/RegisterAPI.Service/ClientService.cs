using RegisterAPI.Entity.Entities;
using RegisterAPI.Repository.Interface;
using RegisterAPI.Service.Interface;

namespace RegisterAPI.Service
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<Guid> InsertClient(Client user)
        {
            Client userInsert = await _clientRepository.Insert(user);

            return userInsert.Id;
        }

        public async Task<Client> GetClientByDocument(string document)
        {
            return await _clientRepository.GetByDocument(document);
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _clientRepository.GetAll();
        }
    }
}
