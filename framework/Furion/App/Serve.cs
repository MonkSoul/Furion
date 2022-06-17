// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace System;

/// <summary>
/// 主机服务静态类
/// </summary>
public static class Serve
{
    /// <summary>
    /// 启动一个轻量级迷你主机
    /// </summary>
    /// <param name="url">默认 5000/5001 端口</param>
    public static void Run(string url = default)
    {
        Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webHostBuilder =>
            {
                webHostBuilder.Inject()
                              .ConfigureServices((context, services) =>
                              {
                                  services.AddCorsAccessor();
                                  services.AddControllers().AddInjectWithUnifyResult();
                              })
                              .Configure((context, app) =>
                              {
                                  if (context.HostingEnvironment.IsDevelopment())
                                  {
                                      app.UseDeveloperExceptionPage();
                                  }

                                  app.UseUnifyResultStatusCodes();

                                  app.UseHttpsRedirection();

                                  app.UseStaticFiles();

                                  app.UseRouting();

                                  app.UseCorsAccessor();

                                  app.UseAuthentication();
                                  app.UseAuthorization();

                                  app.UseInject(string.Empty);

                                  app.UseEndpoints(endpoints =>
                                  {
                                      endpoints.MapControllers();
                                  });
                              });

                if (!string.IsNullOrWhiteSpace(url))
                {
                    webHostBuilder.UseUrls(url);
                }
            })
            .Build()
            .Run();
    }
}