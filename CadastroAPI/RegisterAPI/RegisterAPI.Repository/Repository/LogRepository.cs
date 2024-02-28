using MongoDB.Driver;
using RegisterAPI.Entity.Logger;
using RegisterAPI.Repository.Context;
using RegisterAPI.Repository.Interface;

namespace RegisterAPI.Repository.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly IMongoCollection<Log> _collection;
        public LogRepository(LogContext context)
        {
            _collection = context.Log;
        }
        public async Task<IEnumerable<Log>> GetAllLogs()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<int> InsertLog(Log log)
        {
            await _collection.InsertOneAsync(log);
            return log.Id;
        }
    }
}
