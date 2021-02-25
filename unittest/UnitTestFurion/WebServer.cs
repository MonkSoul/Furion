using Furion;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTestFurion
{
    public class WebServer : IDisposable
    {
        private IHost host;
        CancellationTokenSource sourceToken;
        public async Task BaseServerRunAsync(params string[] args)
        {
            host = CreateBaseHostBuilder(args).Build();
            sourceToken = new CancellationTokenSource();
            await host.StartAsync(sourceToken.Token);
            //取消下面注释可以卡主host浏览swagger-ui
            //while (!sourceToken.Token.IsCancellationRequested)
            //{
            //    await Task.Delay(5);
            //}
        }
        public IHostBuilder CreateBaseHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .Inject()
                        .UseStartup<Startup>()
                        .UseUrls("http://*:5000")
                        ;
                });
        }

        public void Dispose()
        {
            sourceToken.Cancel();
            if (host != null)
                host.StopAsync();
        }
    }
}
