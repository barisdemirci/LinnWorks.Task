using System.Threading.Tasks;
using StackExchange.Redis;

namespace LinnWorks.AWS.Redis
{
    public interface IRedisDataAgent
    {
        Task<string> GetValueAsync(string key);

        Task AddValueAsync(string key, string value);

        Task DeleteValueAsync(string key);

        Task<RedisValue> RightPopAsync(string key);
    }
}