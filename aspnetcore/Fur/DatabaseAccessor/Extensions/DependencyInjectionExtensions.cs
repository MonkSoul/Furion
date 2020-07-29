using Autofac;
using Fur.ApplicationBase;
using Fur.ApplicationBase.Attributes;
using Fur.DatabaseAccessor.Contexts.Options;
using Fur.DatabaseAccessor.Contexts.Pools;
using Fur.DatabaseAccessor.MultipleTenants.Options;
using Fur.DatabaseAccessor.Repositories;
using Fur.DatabaseAccessor.Repositories.MasterSlave;
using Fur.DatabaseAccessor.Repositories.Multiple;
using Fur.DatabaseAccessor.Tangent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fur.DatabaseAccessor.Extensions
{
    /// <summary>
    /// 数据库访问器依赖注入拓展类
    /// </summary>
    [NonWrapper]
    public static class DependencyInjectionExtensions
    {
        #region 注册上下文 + public static ContainerBuilder RegisterDbContexts<TDefaultDbContext>(this ContainerBuilder builder, Action<FurDbContextConfigureOptions> configureOptions = null, params Type[] dbContextTypes)
        /// <summary>
        /// 注册上下文
        /// <para>泛型参数为默认数据库上下文</para>
        /// </summary>
        /// <typeparam name="TDefaultDbContext">默认上下文</typeparam>
        /// <param name="builder">容器构建器</param>
        /// <param name="dbContextTypes">数据库上下文集合</param>
        /// <returns><see cref="ContainerBuilder"/></returns>
        public static ContainerBuilder RegisterDbContexts<TDefaultDbContext>(this ContainerBuilder builder, Action<FurDbContextConfigureOptions> configureOptions = null, params Type[] dbContextTypes)
            where TDefaultDbContext : DbContext
        {
            // 注册数据库上下文池
            builder.RegisterType<DbContextPool>()
                .As<IDbContextPool>()
                .InstancePerLifetimeScope();

            // 注册默认数据库上下文
            builder.RegisterType<TDefaultDbContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            // 载入配置
            var furDbContextInjectionOptions = new FurDbContextConfigureOptions();
            configureOptions?.Invoke(furDbContextInjectionOptions);

            var dbContextTypeList = dbContextTypes.Distinct().ToList();
            dbContextTypeList.Add(typeof(TDefaultDbContext));

            // 支持切面上下文
            if (furDbContextInjectionOptions.SupportTangent)
            {
                builder.RegisterGeneric(typeof(TangentDbContext<>))
                    .As(typeof(ITangentDbContext<>))
                    .InstancePerLifetimeScope();
            }

            // 注册多租户
            if (furDbContextInjectionOptions.MultipleTenantProvider != null)
            {
                var multipleTenantConfigureOptions = furDbContextInjectionOptions.MultipleTenantConfigureOptions;

                // 记录租户注册状态
                AppGlobal.MultipleTenantConfigureOptions = multipleTenantConfigureOptions;
                AppGlobal.SupportedMultipleTenant = multipleTenantConfigureOptions != FurMultipleTenantConfigureOptions.None;

                // 注册多租户数据库上下文
                dbContextTypeList.Add(furDbContextInjectionOptions.MultipleTenantDbContext);

                // 注册多租户提供器
                builder.RegisterType(furDbContextInjectionOptions.MultipleTenantProvider)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            }

            // 注册仓储
            builder.RegisterRepositories(furDbContextInjectionOptions.SupportMultipleDbContext, furDbContextInjectionOptions.SupportMasterSlaveDbContext);

            // 注册多数据库上下文
            builder.RegisterDbContexts(dbContextTypeList.ToArray());

            return builder;
        }
        #endregion

        #region 注册上下文 + private static ContainerBuilder RegisterDbContexts(this ContainerBuilder builder, params Type[] dbContextTypes)
        /// <summary>
        /// 注册上下文
        /// </summary>
        /// <param name="builder">容器构建器</param>
        /// <param name="dbContextTypes">数据库上下文集合</param>
        /// <returns><see cref="ContainerBuilder"/></returns>
        private static ContainerBuilder RegisterDbContexts(this ContainerBuilder builder, params Type[] dbContextTypes)
        {
            foreach (var dbContextType in dbContextTypes)
            {
                builder.RegisterType(dbContextType)
                    .Named<DbContext>(dbContextType.BaseType.GenericTypeArguments.Last().Name)
                    .InstancePerLifetimeScope();
            }
            return builder;
        }
        #endregion

        #region 注册仓储 + public static ContainerBuilder RegisterRepositories(this ContainerBuilder builder, bool supportMultiple = true, bool supportMasterSlave = true)
        /// <summary>
        /// 注册仓储
        /// </summary>
        /// <param name="builder">容器构建器</param>
        /// <param name="supportMultiple">支持多个数据库上下文，默认true：支持</param>
        /// <param name="supportMasterSlave">支持主从库数据库上下文，默认true：支持</param>
        /// <returns><see cref="ContainerBuilder"/></returns>
        private static ContainerBuilder RegisterRepositories(this ContainerBuilder builder, bool supportMultiple = true, bool supportMasterSlave = true)
        {
            // 注册泛型仓储
            builder.RegisterGeneric(typeof(Repositories.EFCoreRepository<>))
                .As(typeof(Repositories.IRepository<>))
                .InstancePerLifetimeScope();

            // 注册非泛型仓储
            builder.RegisterType<Repositories.EFCoreRepository>()
                .As<Repositories.IRepository>()
                .InstancePerLifetimeScope();

            // 支持多个数据库上下文
            if (supportMultiple)
            {
                builder.RegisterGeneric(typeof(Repositories.Multiple.EFCoreRepository<,>))
                    .As(typeof(Repositories.Multiple.IRepository<,>))
                    .InstancePerLifetimeScope();

                builder.RegisterGeneric(typeof(Repositories.Multiple.EFCoreRepository<>))
                    .As(typeof(Repositories.Multiple.IRepository<>))
                    .InstancePerLifetimeScope();
            }

            // 支持主从库数据库上下文
            if (supportMasterSlave)
            {
                builder.RegisterGeneric(typeof(EFCoreRepository<,,>))
                    .As(typeof(IRepository<,,>))
                    .InstancePerLifetimeScope();

                builder.RegisterGeneric(typeof(Repositories.MasterSlave.EFCoreRepository<,>))
                   .As(typeof(Repositories.MasterSlave.IRepository<,>))
                   .InstancePerLifetimeScope();
            }

            return builder;
        }
        #endregion
    }
}