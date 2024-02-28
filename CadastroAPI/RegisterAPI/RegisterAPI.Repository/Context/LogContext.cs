using MongoDB.Driver;
using RegisterAPI.Entity.Logger;

namespace RegisterAPI.Repository.Context
{
    public class LogContext
    {
        private readonly IMongoDatabase _database;

        public LogContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Log> Log => _database.GetCollection<Log>("log");
    }
}
