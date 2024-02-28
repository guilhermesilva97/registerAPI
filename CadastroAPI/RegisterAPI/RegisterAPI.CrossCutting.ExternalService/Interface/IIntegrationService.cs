using RegisterAPI.CrossCutting.ExternalService.Models;
using RegisterAPI.Entity.Entities;

namespace RegisterAPI.CrossCutting.ExternalService.Interface
{
    public interface IIntegrationService
    {
        Task SendClientToIntegrations(Client request);
        Task SendClientToIntegration(Integration integration, Client client);
        Task SyncClientToIntegration(ClientIntegration request);
        Task<Integration> GetIntegrationById(Guid integrationGuid);
        Task<IEnumerable<Integration>> GetAll();
    }
}
