using RegisterAPI.Entity.Entities;

namespace RegisterAPI.CrossCutting.QueueService.Interface
{
    public interface ISendMessageService
    {
        void SendUserMessage(Client user);
    }
}
