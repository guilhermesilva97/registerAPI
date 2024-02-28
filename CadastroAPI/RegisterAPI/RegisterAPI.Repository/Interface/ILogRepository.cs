using RegisterAPI.Entity.Logger;

namespace RegisterAPI.Repository.Interface
{
    public interface ILogRepository
    {
        Task<IEnumerable<Log>> GetAllLogs();
        Task<int> InsertLog(Log product);
    }
}
