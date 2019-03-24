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

        public Task AddValueAsync(string key, string value)
        {
            return _database.ListLeftPushAsync(key, value);
        }

        public Task DeleteValueAsync(string key)
        {
            return _database.KeyDeleteAsync(key);
        }


        public Task<RedisValue> RightPopAsync(string key)
        {
            return _database.ListRightPopAsync(key);
        }
    }
}