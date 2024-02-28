using RegisterAPI.CrossCutting.ExternalService.Interface;
using RegisterAPI.Entity.Logger;
using RegisterAPI.Repository.Interface;

namespace RegisterAPI.CrossCutting.ExternalService
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<IEnumerable<Log>> GetAllLogs()
        {
            return await _logRepository.GetAllLogs();
        }

        public async Task InsertLog(Log request)
        {
            await _logRepository.InsertLog(request);
        }
    }
}
