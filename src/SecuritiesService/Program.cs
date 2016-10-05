using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using StackExchange.Redis;
using System.Reflection;
using SecuritiesService.Models;
using SecuritiesService.Helpers;

namespace SecuritiesService
{
    public class Program
    {
        private static readonly string REDIS_ADDRESS_ENV_PROPERTY_KEY = "MYREDIS_PORT_6379_TCP_ADDR";
        private static readonly string REDIS_PORT_ENV_PROPERTY_KEY = "MYREDIS_PORT_6379_TCP_PORT";

        public static void Main(string[] args)
        {
            redis();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:16555/")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

        private static void env()
        {
            Console.WriteLine(Environment.MachineName);
            foreach (string key in Environment.GetEnvironmentVariables().Keys)
            {
                Console.WriteLine(key + " : " + Environment.GetEnvironmentVariable(key));
            }
            
        }

        private static void redis()
        {
            IList<Security> securities = SecuritiesGenerator.GenerateRandomSecurities(10);

            var configurationOptions = new ConfigurationOptions
            {
                EndPoints =
                {
                    { Environment.GetEnvironmentVariable(REDIS_ADDRESS_ENV_PROPERTY_KEY), int.Parse(Environment.GetEnvironmentVariable(REDIS_PORT_ENV_PROPERTY_KEY)) }
                }
                //KeepAlive = 180,
                //Password = password,
                //DefaultVersion = new Version("2.8.5"),
                // Needed for cache clear
                //AllowAdmin = true
            };

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configurationOptions);

            IDatabase db = redis.GetDatabase();
            if (db.StringSet("testKey", "testValue"))
            {
                var val = db.StringGet("testKey");
                Console.WriteLine(val);
            }
            else
                Console.WriteLine("Did not manage to connect to Redis");


            
            foreach (Security security in securities)
            {
                db.HashSet(security.SecurityId, GenerateRedisHash<Security>(security));
            }
        }

        private static HashEntry[] GenerateRedisHash<T>(T obj)
        {
            var props = typeof(T).GetProperties();
            var hash = new HashEntry[props.Count()];
            for (int i = 0; i < props.Count(); i++)
                hash[i] = new HashEntry(props[i].Name, props[i].GetValue(obj).ToString());
            return hash;
        }
    }
}
