using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace Fur.DatabaseAccessor.Extensions.ModelCreating
{
    /// <summary>
    /// 实体构建器过滤器拓展
    /// </summary>
    public static class EntityTypeBuilderFilterExtensions
    {
        #region 注册租户Id过滤器 + public static EntityTypeBuilder<TEntity> HasTenantIdQueryFilter<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, ITenantProvider tenantProvider) where TEntity : class, IDbEntity, new()

        /// <summary>
        /// 注册租户过滤器
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        /// <param name="tenantProvider">租户提供器</param>
        /// <returns><see cref="EntityTypeBuilder{TEntity}"/></returns>
        public static EntityTypeBuilder<TEntity> HasTenantIdQueryFilter<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, ITenantProvider tenantProvider)
            where TEntity : class, IDbEntity, new()
        {
            if (tenantProvider != null)
            {
                entityTypeBuilder.HasQueryFilter(b => EF.Property<int>(b, nameof(DbEntityBase.TenantId)) == tenantProvider.GetTenantId());
            }
            return entityTypeBuilder;
        }

        #endregion 注册租户Id过滤器 + public static EntityTypeBuilder<TEntity> HasTenantIdQueryFilter<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, ITenantProvider tenantProvider) where TEntity : class, IDbEntity, new()

        #region 注册租户Id过滤器 + public static EntityTypeBuilder HasTenantIdQueryFilter(this EntityTypeBuilder entityTypeBuilder, LambdaExpression lambdaExpression)

        /// <summary>
        /// 注册租户Id过滤器
        /// </summary>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        /// <param name="lambdaExpression">过滤器表达式</param>
        /// <returns></returns>
        public static EntityTypeBuilder HasTenantIdQueryFilter(this EntityTypeBuilder entityTypeBuilder, LambdaExpression lambdaExpression)
        {
            entityTypeBuilder.HasQueryFilter(lambdaExpression);
            return entityTypeBuilder;
        }

        #endregion 注册租户Id过滤器 + public static EntityTypeBuilder HasTenantIdQueryFilter(this EntityTypeBuilder entityTypeBuilder, LambdaExpression lambdaExpression)
    }
}