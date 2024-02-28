using RegisterAPI.CrossCutting.ExternalService.Models;
using RegisterAPI.Entity.Entities;

namespace RegisterAPI.CrossCutting.QueueService.Interface
{
    public interface ISendMessageService
    {
        void SendClientMessage(Client user);
        void SendClientSync(ClientIntegration client);
    }
}
