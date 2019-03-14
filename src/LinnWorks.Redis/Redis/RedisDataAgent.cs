using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace LinnWorks.Redis
{
    public class RedisDataAgent
    {
        private static IDatabase _database;
        public RedisDataAgent()
        {
            var connection = RedisConnectionFactory.GetConnection();

            _database = connection.GetDatabase();
        }

        public string GetFile(string key)
        {
            return _database.StringGet(key);
        }

        public void AddFile(string key, string value)
        {
            _database.StringSet(key, value);
        }

        public void DeleteFileValue(string key)
        {
            _database.KeyDelete(key);
        }
    }
}