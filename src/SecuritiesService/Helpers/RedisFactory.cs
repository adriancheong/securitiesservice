using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace SecuritiesService.Helpers
{
    public class RedisFactory
    {
        private static readonly string REDIS_ALIAS = "MYREDIS";
        private static readonly string REDIS_ADDRESS_ENV_PROPERTY_KEY = REDIS_ALIAS + "_PORT_6379_TCP_ADDR";
        private static readonly string REDIS_PORT_ENV_PROPERTY_KEY = REDIS_ALIAS + "_PORT_6379_TCP_PORT";
        private static IDatabase redisDatabase;

        public static IDatabase GetRedisDatabase()
        {
            if (redisDatabase == null)
            {
                var configurationOptions = new ConfigurationOptions
                {
                    EndPoints =
                    {
                        { Environment.GetEnvironmentVariable(REDIS_ADDRESS_ENV_PROPERTY_KEY), int.Parse(Environment.GetEnvironmentVariable(REDIS_PORT_ENV_PROPERTY_KEY)) }
                    }
                };
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configurationOptions);
                redisDatabase = redis.GetDatabase();
            }
            return redisDatabase;
        }
    }
}
