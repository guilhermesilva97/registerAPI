using RegisterAPI.Application.Interface;
using RegisterAPI.CrossCutting.ExternalService.Interface;
using RegisterAPI.Entity.Logger;
using RegisterAPI.Model.Common;

namespace RegisterAPI.Application
{
    public class LoggerApp : ILoggerApp
    {
        private readonly ILogService _logService;
        public LoggerApp(ILogService logService)
        {
            _logService = logService;
        }
        public async Task<ResultResponseObject<IEnumerable<Log>>> GetAllLogs()
        {
            ResultResponseObject<IEnumerable<Log>> result = new ResultResponseObject<IEnumerable<Log>>();

            result.Value = await _logService.GetAllLogs();

            return result;
        }
    }
}
