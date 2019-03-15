using System.Threading.Tasks;

namespace LinnWorks.AWS.Redis
{
    public interface IRedisDataAgent
    {
        Task<string> GetValueAsync(string key);

        Task AddValueAsync(string key, string value);

        Task DeleteValueAsync(string key);
    }
}