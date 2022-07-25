using System;
using StackExchange.Redis;

namespace RedisExchange.Services
{
    public class RedisService
    {
        private const string Server = "localhost";
        private const string Port = "6380";

        private ConnectionMultiplexer _redis;
        public IDatabase db { get; set; }

        public void Connect()
        {
            var connectionSetting = $"{Server}:{Port}";

            _redis = ConnectionMultiplexer.Connect(connectionSetting);
        }

        public IDatabase GetDb(int db)
        {
            return _redis.GetDatabase(db);
        }
    }
}

