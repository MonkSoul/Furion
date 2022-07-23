using Furion.Application;
using Furion.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace Furion.Web.Core;

[AppStartup(700)]
public sealed class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // 注册 JWT 授权
        services.AddJwt<AuthHandler>();

        services.AddCorsAccessor();

        services.AddControllersWithViews()
                // 配置多语言
                .AddAppLocalization()
                .AddInjectWithUnifyResult();

        services.AddRemoteRequest();

        services.AddEventBus();

        // 添加实时通讯
        services.AddSignalR();

        // 基础配置，支持电脑环境变量
        services.AddFileLogging("application.log");

        // 读取配置文件
        services.AddFileLogging();

        // 自定义配置节点
        services.AddFileLogging(() => "MyLogger");

        // 每天创建一个文件
        services.AddFileLogging("app-{0:yyyy}-{0:MM}-{0:dd}.log", options =>
        {
            options.FileNameRule = fileName =>
            {
                return string.Format(fileName, DateTime.UtcNow);
            };
        });

        // 筛选日志，比如分类
        services.AddFileLogging("xxx.log", options =>
        {
            options.WriteFilter = (logMsg) =>
            {
                return logMsg.LogName.Contains("TestLoggerServices");
            };
        });

        // 自定义日志模板
        services.AddFileLogging("template-obj.log", options =>
        {
            options.MessageFormat = (logMsg) =>
            {
                // 高性能写入
                return logMsg.WriteArray(writer =>
                {
                    writer.WriteStringValue("哈哈哈哈!");
                    writer.WriteStringValue(DateTime.Now.ToString("o"));
                    writer.WriteStringValue(logMsg.LogLevel.ToString());
                    writer.WriteStringValue(logMsg.LogName);
                    writer.WriteNumberValue(logMsg.EventId.Id);
                    writer.WriteStringValue(logMsg.Message);
                    writer.WriteStringValue(logMsg.Exception?.ToString());
                });
            };
        });

        // 处理文件写入错误
        services.AddFileLogging("template-obj.log", options =>
        {
            options.HandleWriteError = (writeError) =>
            {
                writeError.UseRollbackFileName(Path.GetFileNameWithoutExtension(writeError.CurrentFileName) + "-oops" + Path.GetExtension(writeError.CurrentFileName));
            };
        });

        // 读取配置文件
        services.AddDatabaseLogging<DatabaseLoggingWriter>();

        // 读取配置文件，默认配置
        services.AddDatabaseLogging<DatabaseLoggingWriter>(options =>
        {
        });

        // 读取配置文件，默认配置
        services.AddDatabaseLogging<DatabaseLoggingWriter>(options =>
        {
            options.WriteFilter = (logMsg) =>
            {
                return logMsg.LogLevel == Microsoft.Extensions.Logging.LogLevel.Error;
            };
        });

        // 处理文件写入错误
        services.AddDatabaseLogging<DatabaseLoggingWriter>(options =>
        {
            options.HandleWriteError = (writeError, writer) =>
            {
            };
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // 添加规范化结果状态码，需要在这里注册
        app.UseUnifyResultStatusCodes();

        app.UseHttpsRedirection();

        // 配置多语言，必须在 路由注册之前
        app.UseAppLocalization();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseCorsAccessor();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseInject();

        app.UseEndpoints(endpoints =>
        {
            // 批量注册集线器
            endpoints.MapHubs();

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}