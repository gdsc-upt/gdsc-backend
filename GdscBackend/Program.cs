using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace gdsc_web_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
               .ConfigureAppConfiguration(
                    appConfig =>
                    {
                        appConfig.AddJsonFile("appsettings.json", false, true);
                        appConfig.AddJsonFile("appsettings.Development.json", true, true);
                        appConfig.AddJsonFile("appsettings.Local.json", true, true);
                    });
        }
    }
}