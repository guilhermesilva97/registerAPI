namespace RegisterAPI.CrossCutting.QueueService.Interface
{
    public interface IReceiveMessageService
    {
        Task ReceiveMessages();
        Task ReceiveClientSyncMessages();
    }
}
