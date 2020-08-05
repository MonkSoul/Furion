using Fur.Extensions;
using Fur.MirrorController.Attributes;
using Fur.SwaggerDoc.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Fur.SwaggerDoc
{
    /// <summary>
    /// Swagger 配置
    /// </summary>
    internal static class SwaggerDocBuilder
    {
        static SwaggerDocBuilder()
        {
            swaggerOptions = App.Settings.SwaggerDocOptions;
            _controllerTypeSwaggerGroupsCache = new ConcurrentDictionary<Type, IEnumerable<string>>();
            _controllerActionSwaggerGroupsCache = new ConcurrentDictionary<MethodInfo, IEnumerable<string>>();
            SwaggerGroups = ScanAssemblyGroups();
        }

        /// <summary>
        /// Swagger分组
        /// </summary>
        private static readonly string[] SwaggerGroups;

        /// <summary>
        /// Swagger选项
        /// </summary>
        private static readonly FurSwaggerDocOptions swaggerOptions;

        /// <summary>
        /// 初始化Swagger服务
        /// </summary>
        /// <param name="swaggerGenOptions">Swagger生成器选项</param>
        internal static void Initialize(SwaggerGenOptions swaggerGenOptions)
        {
            CreateSwaggerDocs(swaggerGenOptions);
            SetSwaggerBaseConfigure(swaggerGenOptions);
        }

        /// <summary>
        /// 初始化Swagger路由
        /// </summary>
        /// <param name="swaggerUIOptions">Swagger UI选项</param>
        internal static void Initialize(SwaggerUIOptions swaggerUIOptions)
        {
            CreateSwaggerEndpointsAndBaseConfigure(swaggerUIOptions);
        }

        /// <summary>
        /// 扫描程序集中所有的Swagger分组
        /// </summary>
        /// <returns>分组名</returns>
        private static string[] ScanAssemblyGroups()
        {
            var controllerTypes = App.Assemblies.SelectMany(a => a.GetTypes().Where(u => App.IsControllerType(u)));
            var controllerActionTypes = App.Assemblies
                .SelectMany(a => a.GetTypes()
                    .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                        .Where(m => App.IsControllerActionMethod(m))));

            var swaggerGroups = controllerTypes.SelectMany(u => GetControllerTypeSwaggerGroups(u))
                    .Union(
                        controllerActionTypes.SelectMany(u => GetControllerActionSwaggerGroups(u))
                     )
                    .Where(u => !string.IsNullOrEmpty(u))
                    .Distinct();

            return swaggerGroups.ToArray();
        }

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
                Title = groupOptions?.Title ?? string.Join(' ', group.CamelCaseSplitString()),
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

        /// <summary>
        /// Swagger分组切换接口显示列表
        /// </summary>
        /// <param name="currentGroup"></param>
        /// <param name="apiDescription"></param>
        /// <returns></returns>
        private static bool SwaggerGroupSwitchPredicate(string currentGroup, ApiDescription apiDescription)
        {
            if (!apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
            var methodSwaggerGroups = GetControllerActionSwaggerGroups(methodInfo);

            return methodSwaggerGroups.Contains(currentGroup);
        }

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
                var thisType = typeof(SwaggerDocBuilder);
                var thisAssembly = thisType.Assembly;
                swaggerUIOptions.IndexStream = () => thisAssembly.GetManifestResourceStream($"{thisType.Namespace}.Assets.MiniProfilerIndex.html");
            }
        }

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

        private static readonly ConcurrentDictionary<Type, IEnumerable<string>> _controllerTypeSwaggerGroupsCache;

        /// <summary>
        /// 获取控制器类型 Swagger 接口文档分组
        /// </summary>
        /// <param name="controllerType">控制器类型</param>
        /// <returns>string[]</returns>
        private static string[] GetControllerTypeSwaggerGroups(Type controllerType)
        {
            var isCached = _controllerTypeSwaggerGroupsCache.TryGetValue(controllerType, out IEnumerable<string> swaggerGroups);
            if (isCached) return swaggerGroups.ToArray();

            swaggerGroups = GetControllerTypeSwaggerGroupsCore(controllerType);

            _controllerTypeSwaggerGroupsCache.TryAdd(controllerType, swaggerGroups);
            return swaggerGroups?.ToArray();
        }

        private static string[] GetControllerTypeSwaggerGroupsCore(Type controllerType)
        {
            // 如果不是控制器类型，返回 null
            if (!App.IsControllerType(controllerType)) return default;

            var defaultSwaggerGroups = new string[] { App.Settings.SwaggerDocOptions.DefaultGroupName };

            if (!controllerType.IsDefined(typeof(MirrorSettingsAttribute), true))
                return defaultSwaggerGroups;

            var mirrorControllerAttribute = controllerType.GetCustomAttribute<MirrorSettingsAttribute>(true);

            return mirrorControllerAttribute.SwaggerGroups == null || !mirrorControllerAttribute.SwaggerGroups.Any()
                ? defaultSwaggerGroups
                : mirrorControllerAttribute.SwaggerGroups;
        }

        private static readonly ConcurrentDictionary<MethodInfo, IEnumerable<string>> _controllerActionSwaggerGroupsCache;

        /// <summary>
        /// 获取控制器 Action Swagger 接口文档分组
        /// </summary>
        /// <param name="method">控制器Action</param>
        /// <returns>string[]</returns>
        private static string[] GetControllerActionSwaggerGroups(MethodInfo method)
        {
            var isCached = _controllerActionSwaggerGroupsCache.TryGetValue(method, out IEnumerable<string> swaggerGroups);
            if (isCached) return swaggerGroups.ToArray();

            swaggerGroups = GetControllerActionSwaggerGroupsCore(method);
            _controllerActionSwaggerGroupsCache.TryAdd(method, swaggerGroups);
            return swaggerGroups?.ToArray();
        }

        private static string[] GetControllerActionSwaggerGroupsCore(MethodInfo controllerAction)
        {
            // 如果不是控制器Action类型，返回 null
            if (!App.IsControllerActionMethod(controllerAction)) return default;

            if (!controllerAction.IsDefined(typeof(MirrorSettingsAttribute), true))
                return GetControllerTypeSwaggerGroups(controllerAction.DeclaringType);

            var mirrorActionAttribute = controllerAction.GetCustomAttribute<MirrorSettingsAttribute>(true);

            return mirrorActionAttribute.SwaggerGroups == null || !mirrorActionAttribute.SwaggerGroups.Any()
                ? GetControllerTypeSwaggerGroups(controllerAction.DeclaringType)
                : mirrorActionAttribute.SwaggerGroups;
        }
    }
}