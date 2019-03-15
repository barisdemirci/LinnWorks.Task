using System.Threading.Tasks;
using StackExchange.Redis;

namespace LinnWorks.AWS.Redis
{
    public class RedisDataAgent : IRedisDataAgent
    {
        private static IDatabase _database;
        public RedisDataAgent()
        {
            var connection = RedisConnectionFactory.GetConnection();

            _database = connection.GetDatabase();
        }

        public async Task<string> GetValueAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task AddValueAsync(string key, string value)
        {
            await _database.StringSetAsync(key, value);
        }

        public async Task DeleteValueAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
    }
}