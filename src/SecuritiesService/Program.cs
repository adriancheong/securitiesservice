using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using StackExchange.Redis;
using SecuritiesService.Models;
using SecuritiesService.Helpers;

namespace SecuritiesService
{
    public class Program
    {

        public static void Main(string[] args)
        {
            //initAndPopulateRedisStore();

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
    }
}
