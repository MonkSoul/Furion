using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Fur.Web.Entry
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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseSetting(WebHostDefaults.HostingStartupAssembliesKey, nameof(Fur))   // 必须设置这一句
                        .UseStartup<Startup>();
                });
        }
    }
}