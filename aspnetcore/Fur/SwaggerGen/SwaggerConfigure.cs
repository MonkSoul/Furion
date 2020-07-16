using Fur.ApplicationBase;
using Fur.SwaggerGen.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Fur.SwaggerGen
{
    /// <summary>
    /// Swagger 配置
    /// </summary>
    internal sealed class SwaggerConfigure
    {
        /// <summary>
        /// Swagger分组
        /// </summary>
        private static readonly string[] SwaggerGroups = ScanAssemblyGroups();

        /// <summary>
        /// Swagger选项
        /// </summary>
        private static SwaggerOptions swaggerOptions;

        #region 设置Swagger选项配置 + public static void SetSwaggerOptions(SwaggerOptions swaggerOptions)

        /// <summary>
        /// 设置Swagger选项配置
        /// </summary>
        /// <param name="swaggerOptions">Swagger选项配置</param>
        public static void SetSwaggerOptions(SwaggerOptions swaggerOptions) => SwaggerConfigure.swaggerOptions = swaggerOptions;

        #endregion 设置Swagger选项配置 + public static void SetSwaggerOptions(SwaggerOptions swaggerOptions)

        #region 初始化Swagger服务 -/* public static void Initialize(SwaggerGenOptions swaggerGenOptions)

        /// <summary>
        /// 初始化Swagger服务
        /// </summary>
        /// <param name="swaggerGenOptions">Swagger生成器选项</param>
        public static void Initialize(SwaggerGenOptions swaggerGenOptions)
        {
            CreateSwaggerDocs(swaggerGenOptions);
            SetSwaggerBaseConfigure(swaggerGenOptions);
        }

        #endregion 初始化Swagger服务 -/* public static void Initialize(SwaggerGenOptions swaggerGenOptions)

        #region 初始化Swagger路由 -  public static void Initialize(SwaggerUIOptions swaggerUIOptions)

        /// <summary>
        /// 初始化Swagger路由
        /// </summary>
        /// <param name="swaggerUIOptions">Swagger UI选项</param>
        public static void Initialize(SwaggerUIOptions swaggerUIOptions)
        {
            CreateSwaggerEndpointsAndBaseConfigure(swaggerUIOptions);
        }

        #endregion 初始化Swagger路由 -  public static void Initialize(SwaggerUIOptions swaggerUIOptions)

        #region 扫描程序集中所有的Swagger分组 + private static string[] ScanAssemblyGroups()

        /// <summary>
        /// 扫描程序集中所有的Swagger分组
        /// </summary>
        /// <returns>分组名</returns>
        private static string[] ScanAssemblyGroups()
        {
            var controllerTypes = ApplicationCore.ApplicationWrapper.PublicClassTypeWrappers.Where(u => u.IsControllerType);
            var controllerActionTypes = ApplicationCore.ApplicationWrapper.PublicMethodWrappers.Where(u => u.IsControllerActionType);

            var swaggerGroups = controllerTypes
                    .Where(u => u.SwaggerGroups != null)
                    .SelectMany(u => u.SwaggerGroups)
                    .Union(
                        controllerActionTypes
                        .Where(u => u.SwaggerGroups != null)
                        .SelectMany(u => u.SwaggerGroups)
                     )
                    .Distinct()
                    .ToList();

            return swaggerGroups.ToArray();
        }

        #endregion 扫描程序集中所有的Swagger分组 + private static string[] ScanAssemblyGroups()

        #region 生成分组文档 -/* private static void CreateSwaggerDocs(SwaggerGenOptions swaggerGenOptions)

        /// <summary>
        /// 生成分组文档
        /// </summary>
        /// <param name="swaggerGenOptions">Swagger生成器选项</param>
        private static void CreateSwaggerDocs(SwaggerGenOptions swaggerGenOptions)
        {
            foreach (var group in SwaggerGroups)
            {
                swaggerGenOptions.SwaggerDoc(group, LoadSwaggerGroupOptions(group));
            }
        }

        #endregion 生成分组文档 -/* private static void CreateSwaggerDocs(SwaggerGenOptions swaggerGenOptions)

        #region 加载分组配置信息 -/* private static OpenApiInfo LoadSwaggerGroupOptions(string group)

        /// <summary>
        /// 加载分组配置信息
        /// </summary>
        /// <param name="group">分组名</param>
        /// <returns></returns>
        private static OpenApiInfo LoadSwaggerGroupOptions(string group)
        {
            var groupOptions = swaggerOptions?.Groups?.FirstOrDefault(u => u.Name == group);
            return new OpenApiInfo
            {
                Title = groupOptions?.Title ?? group,
                Description = groupOptions?.Description,
                Version = groupOptions?.Version,
                TermsOfService = string.IsNullOrEmpty(groupOptions?.TermsOfService) ? null : new Uri(groupOptions?.TermsOfService),
                Contact = groupOptions?.Contact == null ? null : new OpenApiContact
                {
                    Name = groupOptions?.Contact?.Name,
                    Email = groupOptions?.Contact?.Email,
                    Url = string.IsNullOrEmpty(groupOptions?.Contact?.Uri) ? null : new Uri(groupOptions?.Contact.Uri)
                },
                License = groupOptions?.License == null ? null : new OpenApiLicense
                {
                    Name = groupOptions?.License?.Name,
                    Url = string.IsNullOrEmpty(groupOptions?.License?.Uri) ? null : new Uri(groupOptions?.License.Uri)
                }
            };
        }

        #endregion 加载分组配置信息 -/* private static OpenApiInfo LoadSwaggerGroupOptions(string group)

        #region 设置Swagger基础配置 -/* private static void SetSwaggerBaseConfigure(SwaggerGenOptions swaggerGenOptions)

        /// <summary>
        /// 设置Swagger基础配置
        /// </summary>
        /// <param name="swaggerGenOptions">Swagger生成器选项</param>
        private static void SetSwaggerBaseConfigure(SwaggerGenOptions swaggerGenOptions)
        {
            AddSecurityAuthorization(swaggerGenOptions);

            swaggerGenOptions.DocInclusionPredicate((currentGroup, apiDesc) => SwaggerGroupSwitchPredicate(currentGroup, apiDesc));
            swaggerGenOptions.CustomSchemaIds(x => x.FullName);
            //options.AddFluentValidationRules();

            var loadCommentsAssemblies = swaggerOptions.LoadCommentsAssemblies;
            foreach (var assemblyName in loadCommentsAssemblies)
            {
                var assemblyXml = $"{assemblyName}.xml";
                var assemblyXmlPath = Path.Combine(AppContext.BaseDirectory, assemblyXml);
                if (File.Exists(assemblyXmlPath))
                {
                    swaggerGenOptions.IncludeXmlComments(assemblyXmlPath, true);
                }
            }
        }

        #endregion 设置Swagger基础配置 -/* private static void SetSwaggerBaseConfigure(SwaggerGenOptions swaggerGenOptions)

        #region Swagger分组切换接口显示列表 - private static bool SwaggerGroupSwitchPredicate(string currentGroup, ApiDescription apiDescription)

        /// <summary>
        /// Swagger分组切换接口显示列表
        /// </summary>
        /// <param name="currentGroup"></param>
        /// <param name="apiDescription"></param>
        /// <returns></returns>
        private static bool SwaggerGroupSwitchPredicate(string currentGroup, ApiDescription apiDescription)
        {
            if (!apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
            var methodSwaggerGroups = ApplicationCore.GetMethodWrapper(methodInfo).SwaggerGroups;

            return methodSwaggerGroups.Contains(currentGroup);
        }

        #endregion Swagger分组切换接口显示列表 - private static bool SwaggerGroupSwitchPredicate(string currentGroup, ApiDescription apiDescription)

        #region 创建Swagger终点路由配置 -/* private static void CreateSwaggerEndpointsAndBaseConfigure(SwaggerUIOptions swaggerUIOptions)

        /// <summary>
        /// 创建Swagger终点路由配置
        /// </summary>
        /// <param name="swaggerUIOptions"></param>
        private static void CreateSwaggerEndpointsAndBaseConfigure(SwaggerUIOptions swaggerUIOptions)
        {
            foreach (var group in SwaggerGroups)
            {
                var groupOptions = swaggerOptions?.Groups?.FirstOrDefault(u => u.Name == group);
                swaggerUIOptions.SwaggerEndpoint($"/swagger/{group}/swagger.json", groupOptions?.Title ?? group);
            }

            swaggerUIOptions.RoutePrefix = string.Empty;
            swaggerUIOptions.DocumentTitle = swaggerOptions?.DocumentTitle;

            if (swaggerOptions.EnableMiniProfiler)
            {
                var thisType = typeof(SwaggerConfigure);
                var thisAssembly = thisType.Assembly;
                swaggerUIOptions.IndexStream = () => thisAssembly.GetManifestResourceStream($"{thisType.Namespace}.Assets.MiniProfilerIndex.html");
            }
        }

        #endregion 创建Swagger终点路由配置 -/* private static void CreateSwaggerEndpointsAndBaseConfigure(SwaggerUIOptions swaggerUIOptions)

        private static void AddSecurityAuthorization(SwaggerGenOptions swaggerGenOptions)
        {
            swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "bearer",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT"
            });

            swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id ="Bearer" } },
                       new string[]{ }
                    }
                });
        }
    }
}