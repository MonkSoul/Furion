using Fur.AppCore.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Extensions
{
    /// <summary>
    /// <see cref="IQueryable{T}"/> 分页拓展类
    /// </summary>
    [NonInflated]
    public static class IQueryablePagedExtensions
    {
        /// <summary>
        /// 分页拓展
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entities">实体集合</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns><see cref="PagedList{TEntity}"/></returns>
        public static PagedList<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> entities, int pageIndex = 1, int pageSize = 20)
        {
            var totalCount = entities.Count();
            var items = entities.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PagedList<TEntity>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasNextPages = pageIndex < totalPages,
                HasPrevPages = pageIndex - 1 > 0
            };
        }

        /// <summary>
        /// 分页拓展
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entities">实体集合</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns><see cref="PagedList{TEntity}"/></returns>
        public static async Task<PagedList<TEntity>> ToPagedListAsync<TEntity>(this IQueryable<TEntity> entities, int pageIndex = 1, int pageSize = 20)
        {
            var totalCount = await entities.CountAsync();
            var items = await entities.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PagedList<TEntity>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasNextPages = pageIndex < totalPages,
                HasPrevPages = pageIndex - 1 > 0
            };
        }
    }
}