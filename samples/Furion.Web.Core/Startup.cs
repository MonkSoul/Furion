using Furion.Application;
using Furion.Localization;
using Furion.Schedule;
using Furion.VirtualFileServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace Furion.Web.Core;

[AppStartup(700)]
public sealed class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddConsoleFormatter();

        // 注册 JWT 授权
        services.AddJwt<AuthHandler>();

        services.AddCorsAccessor();

        services.AddControllersWithViews()
                // 配置多语言
                .AddAppLocalization()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.AddDateTimeTypeConverters(localized: true);
                    options.JsonSerializerOptions.Converters.AddClayConverters();

                    options.JsonSerializerOptions.Converters.AddDateOnlyConverters("yyyy-MM-dd");
                    options.JsonSerializerOptions.Converters.AddTimeOnlyConverters("HH:mm:ss");
                })
                .AddInjectWithUnifyResult()
                .AddUnifyJsonOptions("special", new JsonSerializerOptions
                {
                    PropertyNamingPolicy = null
                });

        services.AddUnifyProvider<SpeciallyResultProvider>("specially");

        services.AddRemoteRequest();

        services.AddEventBus(options =>
        {
            options.AddFallbackPolicy<EventFallbackPolicy>();
        });

        // 添加实时通讯
        services.AddSignalR();

        services.AddFileLogging();

        services.AddDatabaseLogging<DatabaseLoggingWriter>();

        services.AddMonitorLogging();

        services.AddFromConvertBinding();

        // 新版本定时任务测试
        services.AddSchedule(options =>
        {
            //options.UseUtcTimestamp = true;
            //options.AddJob(JobBuilder.Create<TestJob>().SetDescription("这是定时任务包含多个作业触发器")
            //    , Triggers.Minutely(), Triggers.Period(5000).SetDescription("这是作业触发器，间隔 5 秒"));
            //options.AddJob<TestJob>(Triggers.Hourly());

            //options.AddHttpJob(request =>
            //{
            //    request.RequestUri = "https://www.chinadot.net";
            //    request.HttpMethod = HttpMethod.Get;
            //    request.Headers.Add("framework", "Furion");
            //    request.GroupName = "group";
            //    request.Description = "作业请求描述";
            //}, Triggers.PeriodSeconds(5));

            //options.AddJob((context, stoppingToken) =>
            //{
            //    context.ServiceProvider.GetLogger().LogInformation($"{context}");
            //    return Task.CompletedTask;
            //}, Triggers.PeriodSeconds(2));

            //options.AddPersistence<TestJobPersistence>();

            options.GroupSet("group1", () =>
            {
                options.AddJob<TestJob>(Triggers.PeriodHours(1).SetMaxNumberOfRuns(2), Triggers.PeriodSeconds(4));
                //options.AddJobFactory<JobFactory>();
                options.AddJob<TestCancelJob>("cancelJob", Triggers.PeriodSeconds(30));
            });

            options.GroupSet("group2", () =>
            {
                options.AddJob<TestJob>(Triggers.PeriodHours(1)
                    .SetStartTime(DateTime.Now.AddMonths(-2))
                    .SetRunOnStart(true));
            });

            options.AddJob<TestJob>(Triggers.PeriodHours(2));

            options.AddJob<TestCancelJob>(Triggers.PeriodHours(2)
               , Triggers.PeriodHours(2));

            options.AddJob<TestCancelJob>();

            options.AddJob<TestCancelJob>();
        });

        // 新版本任务队列
        services.AddTaskQueue(options =>
        {
            //options.Concurrent = false;
        });

        services.AddViewEngine();

        services.AddSensitiveDetection(options =>
        {
            //options.EmbedFileName = "custom-words.txt";
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
        app.UseAppLocalization(options =>
        {
            // options.AddInitialRequestCultureProvider(new CustomizeQueryStringRequestCultureProvider("my-culture"));
        });

        app.UseStaticFiles(new StaticFileOptions
        {
            ContentTypeProvider = FS.GetFileExtensionContentTypeProvider()
        });

        app.UseScheduleUI(options =>
        {
        });

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