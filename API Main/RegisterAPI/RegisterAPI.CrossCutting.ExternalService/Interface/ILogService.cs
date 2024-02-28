using RegisterAPI.Entity.Logger;

namespace RegisterAPI.CrossCutting.ExternalService.Interface
{
    public interface ILogService
    {
        Task InsertLog(Log request);
        Task<IEnumerable<Log>> GetAllLogs();
    }
}
