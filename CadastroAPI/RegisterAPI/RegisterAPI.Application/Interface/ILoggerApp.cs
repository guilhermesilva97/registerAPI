using RegisterAPI.Entity.Logger;
using RegisterAPI.Model.Common;

namespace RegisterAPI.Application.Interface
{
    public interface ILoggerApp
    {
        Task<ResultResponseObject<IEnumerable<Log>>> GetAllLogs();
    }
}
