using Fur.ApplicationBase;
using Fur.DatabaseVisitor.Entities;
using Fur.EmitExpression;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Fur.DatabaseVisitor.Contexts
{
    /// <summary>
    /// 框架自定义DbContext 状态器
    /// <para>作于标记作用。避免 <c>OnConfiguring</c> 和 <c>OnModelCreating</c> 重复初始化</para>
    /// </summary>
    internal static class FurDbContextOfTStatus
    {
        /// <summary>
        /// 是否已经调用过 <c>OnConfiguring</c>
        /// <para>默认：<c>false</c></para>
        /// </summary>
        private static bool isCallOnConfiguringed = false;

        /// <summary>
        /// 是否已经调用过 <c>OnModelCreating</c>
        /// <para>默认：<c>false</c></para>
        /// </summary>
        private static bool isCallOnModelCreatinged = false;

        /// <summary>
        /// EntityDelegate 私有字段
        /// </summary>
        private static Func<ModelBuilder, Type, EntityTypeBuilder> _entityDelegate = null;
        /// <summary>
        /// 模型构建器 <c>Entity</c> 委托
        /// <para>主要用来反射调用 <c>Entity</c> 方法</para>
        /// <para>参见：<see cref="ModelBuilder.Entity(Type)"/></para>
        /// </summary>
        internal static Func<ModelBuilder, Type, EntityTypeBuilder> EntityDelegate
        {
            get
            {
                if (_entityDelegate == null)
                {
                    var entityMethod = typeof(ModelBuilder).GetMethods()
                  .Where(u => u.Name == nameof(ModelBuilder.Entity) && u.GetParameters().Length == 1 &&
                               u.GetParameters().First().ParameterType == typeof(Type))
                  .FirstOrDefault();

                    _entityDelegate = (Func<ModelBuilder, Type, EntityTypeBuilder>)Delegate.CreateDelegate(typeof(Func<ModelBuilder, Type, EntityTypeBuilder>), entityMethod);
                }

                return _entityDelegate;
            }
        }

        /// <summary>
        /// HasDbFunctionDelegate 私有字段
        /// </summary>
        private static Func<ModelBuilder, MethodInfo, DbFunctionBuilder> _hasDbFunctionDelegate = null;
        /// <summary>
        /// 模型构建器 <c>HasDbFunction</c> 委托
        /// <para>主要用来反射调用 <c>HasDbFunction</c> 方法</para>
        /// <para>参见：<see cref="RelationalModelBuilderExtensions.HasDbFunction(ModelBuilder, MethodInfo)"/></para>
        /// </summary>
        internal static Func<ModelBuilder, MethodInfo, DbFunctionBuilder> HasDbFunctionDelegate
        {
            get
            {
                if (_hasDbFunctionDelegate == null)
                {
                    var relationalModelBuilderExtensionsType = typeof(RelationalModelBuilderExtensions);
                    var hasDbFunctionMethod = relationalModelBuilderExtensionsType.GetMethods()
                             .Where(u => u.Name == "HasDbFunction" && u.GetParameters().Length == 2 &&
                                          u.GetParameters().Last().ParameterType == typeof(MethodInfo))
                             .FirstOrDefault();

                    _hasDbFunctionDelegate = (Func<ModelBuilder, MethodInfo, DbFunctionBuilder>)Delegate.CreateDelegate(typeof(Func<ModelBuilder, MethodInfo, DbFunctionBuilder>), hasDbFunctionMethod);
                }

                return _hasDbFunctionDelegate;
            }
        }

        /// <summary>
        /// HasNoKeyDelegate 私有字段
        /// </summary>
        private static Func<EntityTypeBuilder, EntityTypeBuilder> _hasNoKeyDelegate = null;
        /// <summary>
        /// 实体类型构建器 <c>HasNoKey</c> 委托
        /// <para>主要用来反射调用 <c>HasNoKey</c> 方法</para>
        /// <para>参见：<see cref="EntityTypeBuilder.HasNoKey"/></para>
        /// </summary>
        internal static Func<EntityTypeBuilder, EntityTypeBuilder> HasNoKeyDelegate
        {
            get
            {
                if (_hasNoKeyDelegate == null)
                {
                    var hasNoKeyMethod = typeof(EntityTypeBuilder).GetMethod("HasNoKey");

                    _hasNoKeyDelegate = (Func<EntityTypeBuilder, EntityTypeBuilder>)Delegate.CreateDelegate(typeof(Func<EntityTypeBuilder, EntityTypeBuilder>), hasNoKeyMethod);
                }

                return _hasNoKeyDelegate;
            }
        }

        /// <summary>
        /// ToViewDelegate 私有字段
        /// </summary>
        private static Func<EntityTypeBuilder, string, EntityTypeBuilder> _toViewDelegate;
        /// <summary>
        /// 实体构建器 <c>ToView</c> 委托
        /// <para>主要用来反射调用 <c>ToView</c> 方法</para>
        /// <para>参见：<see cref="RelationalEntityTypeBuilderExtensions.ToView(EntityTypeBuilder, string)"/></para>
        /// </summary>
        internal static Func<EntityTypeBuilder, string, EntityTypeBuilder> ToViewDelegate
        {
            get
            {
                if (_toViewDelegate == null)
                {
                    var relationalEntityTypeBuilderExtensionsType = typeof(RelationalEntityTypeBuilderExtensions);
                    var toViewMethod = relationalEntityTypeBuilderExtensionsType.GetMethods()
                        .Where(u => u.Name == nameof(RelationalEntityTypeBuilderExtensions.ToView) && u.GetParameters().Length == 2 &&
                                     u.GetParameters().Last().ParameterType == typeof(string))
                        .FirstOrDefault();

                    _toViewDelegate = (Func<EntityTypeBuilder, string, EntityTypeBuilder>)Delegate.CreateDelegate(typeof(Func<EntityTypeBuilder, string, EntityTypeBuilder>), toViewMethod);
                }

                return _toViewDelegate;
            }
        }

        /// <summary>
        /// HasQueryFilterDelegate 私有字段
        /// </summary>
        private static Func<EntityTypeBuilder, LambdaExpression, EntityTypeBuilder> _hasQueryFilterDelegate = null;
        /// <summary>
        /// 实体构建器 <c>HasQueryFilter</c> 委托
        /// <para>主要用来反射调用 <c>HasQueryFilter</c> 方法</para>
        /// <para>参见：<see cref="EntityTypeBuilder.HasQueryFilter(LambdaExpression)"/></para>
        /// </summary>
        internal static Func<EntityTypeBuilder, LambdaExpression, EntityTypeBuilder> HasQueryFilterDelegate
        {
            get
            {
                if (_hasQueryFilterDelegate == null)
                {
                    var hasQueryFilterMethod = typeof(EntityTypeBuilder).GetMethod("HasQueryFilter");

                    _hasQueryFilterDelegate = (Func<EntityTypeBuilder, LambdaExpression, EntityTypeBuilder>)Delegate.CreateDelegate(typeof(Func<EntityTypeBuilder, LambdaExpression, EntityTypeBuilder>), hasQueryFilterMethod);
                }

                return _hasQueryFilterDelegate;
            }
        }

        /// <summary>
        /// EF Property属性方法
        /// <para>主要用来反射调用 <c>Property</c> 委托，用于 <c>Int32</c></para>
        /// <para>参见：<see cref="EF.Property"/></para>
        /// </summary>
        internal static MethodInfo EFPropertyGenericInt32Method = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(int));

        #region 检查 OnConfiguring 调用情况 + internal static bool CallOnConfiguringed()
        /// <summary>
        /// 检查 <c>OnConfiguring</c> 调用情况
        /// </summary>
        /// <returns>返回 <c>true</c> 表示已经被调用过了</returns>
        internal static bool CallOnConfiguringed()
        {
            if (!isCallOnConfiguringed)
            {
                isCallOnConfiguringed = true;
                return false;
            }
            return true;
        }
        #endregion

        #region 检查 OnModelCreating 调用情况 + internal static bool CallOnModelCreatinged()
        /// <summary>
        /// 检查 <c>OnModelCreating</c> 调用情况
        /// </summary>
        /// <returns>返回 <c>true</c> 表示已经被调用过了</returns>
        internal static bool CallOnModelCreatinged()
        {
            if (!isCallOnModelCreatinged)
            {
                isCallOnModelCreatinged = true;
                return false;
            }
            return true;
        }
        #endregion

        #region 创建 HasQueryFilter 表达式参数 + internal static LambdaExpression CreateHasQueryFilterExpression(Type entityType, string propertyName, int propertyValue)
        /// <summary>
        /// 创建 <c>HasQueryFilter</c> 表达式参数
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        /// <returns><see cref="LambdaExpression"/></returns>
        internal static LambdaExpression CreateHasQueryFilterExpression(Type entityType, string propertyName, int propertyValue)
        {
            var leftParameter = Expression.Parameter(entityType, "e");
            var constantKey = Expression.Constant(propertyName);
            var constantValue = Expression.Constant(propertyValue);

            var expressionBody = Expression.Equal(Expression.Call(EFPropertyGenericInt32Method, leftParameter, constantKey), constantValue);
            return Expression.Lambda(expressionBody, leftParameter);
        }
        #endregion

        #region 扫描数据库编译实体并创建模型实体 + internal static void ScanDbCompileEntityToCreateModelEntity(ModelBuilder modelBuilder, string tenantIdKey, int tenantId)
        /// <summary>
        /// 扫描数据库编译实体并创建模型实体
        /// <para>数据库编译实体包括：视图、函数、存储过程</para>
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="tenantIdKey">租户Id的键</param>
        /// <param name="tenantId">租户Id</param>
        internal static void ScanDbCompileEntityToCreateModelEntity(ModelBuilder modelBuilder, string tenantIdKey, int tenantId)
        {
            var viewTypes = ApplicationCore.ApplicationWrapper.PublicClassTypeWrappers
                .Where(u => typeof(DbView).IsAssignableFrom(u.Type) && u.CanBeNew);

            var dbFunctionMethods = ApplicationCore.ApplicationWrapper.PublicMethodWrappers
                .Where(u => u.IsStaticMethod && u.Method.IsDefined(typeof(DbFunctionAttribute)) && u.ThisDeclareType.IsAbstract && u.ThisDeclareType.IsSealed);

            foreach (var viewType in viewTypes)
            {
                var entityTypeBuilder = FurDbContextOfTStatus.EntityDelegate(modelBuilder, viewType.Type);
                entityTypeBuilder = FurDbContextOfTStatus.HasNoKeyDelegate(entityTypeBuilder);

                var viewInstance = ExpressionCreateObject.CreateInstance<IDbView>(viewType.Type);
                entityTypeBuilder = FurDbContextOfTStatus.ToViewDelegate(entityTypeBuilder, viewInstance.ViewName);

                var lambdaExpression = FurDbContextOfTStatus.CreateHasQueryFilterExpression(viewType.Type, tenantIdKey, tenantId);
                entityTypeBuilder = FurDbContextOfTStatus.HasQueryFilterDelegate(entityTypeBuilder, lambdaExpression);
            }

            foreach (var dbFunction in dbFunctionMethods)
            {
                FurDbContextOfTStatus.HasDbFunctionDelegate(modelBuilder, dbFunction.Method);
            }
        }
        #endregion
    }
}
