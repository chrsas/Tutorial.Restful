using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Tutorial.Restful.Configurations;

namespace Tutorial.Restful
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            DealWithConfiguration();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static void DealWithConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Configurations\\FirstConfig.json")
                .AddJsonFile("Configurations\\SecondConfig.json");

            var firstConfig = new FirstConfig();


            Configuration = configBuilder.Build();
            Configuration.Bind(firstConfig);

            Console.WriteLine($"FirstConfig class Key1: {firstConfig.Key1}");
            Console.WriteLine($"FirstConfig class ChildKey1: {firstConfig.Key3.ChildKey1}");

            foreach (var item in Configuration.AsEnumerable())
            {
                System.Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
            }

            System.Console.WriteLine($"Key1: {Configuration["key1"]}");
            System.Console.WriteLine($"Key2: {Configuration["key2"]}");
            System.Console.WriteLine($"childkey1: {Configuration["key3:childkey1"]}");

            var key3Section = Configuration.GetSection("key3");
            Console.WriteLine($"key3:childkey1: {key3Section["childkey1"]}");
        }
    }
}
