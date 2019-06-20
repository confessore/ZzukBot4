using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using ZzukBot.Server.Core.Authentication;
using ZzukBot.Server.Core.Authentication.Interfaces;
using ZzukBot.Server.Services;
using ZzukBot.Server.Services.Interfaces;
using ZzukBot.Server.Services.Options;

namespace ZzukBot.Server
{
    class Program
    {
        IConfiguration Configuration { get; set; }
        IServiceProvider Services { get; set; }

        Program()
        {
            Configuration = BuildConfiguration();
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            Services = services.BuildServiceProvider();
            Services.GetRequiredService<ISslListenerAsync>().StartListening();
            Console.Read();
        }

        private IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMySqlQuerier, MySqlQuerier>();
            services.Configure<MySqlQuerierOptions>(Configuration.GetSection("ConnectionStrings"));

            services.AddSingleton<ISslListenerAsync, SslListenerAsync>();
        }

        static void Main(string[] args)
        {
            try
            {
                new Program();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
