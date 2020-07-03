using System;
using System.Linq.Expressions;

namespace Fur.Linq.Builders
{
    public class LinqBuilderOfEFCore
    {
        #region [EF Core] 创建 Linq/Lambda 表达式 +/* public static Expression<Func<TSource, bool>> Create<TSource>(Expression<Func<TSource, bool>> expression)

        /// <summary>
        /// [EF Core] 创建 Linq/Lambda 表达式
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>新的表达式</returns>
        public static Expression<Func<TSource, bool>> Create<TSource>(Expression<Func<TSource, bool>> expression) => expression;

        #endregion [EF Core] 创建 Linq/Lambda 表达式 +/* public static Expression<Func<TSource, bool>> Create<TSource>(Expression<Func<TSource, bool>> expression)

        #region [EF Core] 创建 Linq/Lambda 表达式，支持索引器 +/* public static Expression<Func<TSource, int, bool>> Create<TSource>(Expression<Func<TSource, int, bool>> expression)

        /// <summary>
        /// [EF Core] 创建 Linq/Lambda 表达式，支持索引器
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>新的表达式</returns>
        public static Expression<Func<TSource, int, bool>> Create<TSource>(Expression<Func<TSource, int, bool>> expression) => expression;

        #endregion [EF Core] 创建 Linq/Lambda 表达式，支持索引器 +/* public static Expression<Func<TSource, int, bool>> Create<TSource>(Expression<Func<TSource, int, bool>> expression)

        #region [EF Core] 创建 And 表达式 +/*  public static Expression<Func<TSource, bool>> And<TSource>()

        /// <summary>
        /// [EF Core] 创建 And 表达式
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <returns>新的表达式</returns>
        public static Expression<Func<TSource, bool>> And<TSource>() => u => true;

        #endregion [EF Core] 创建 And 表达式 +/*  public static Expression<Func<TSource, bool>> And<TSource>()

        #region [EF Core] 创建 And 表达式，支持索引器 +/* public static Expression<Func<TSource, int, bool>> IndexAnd<TSource>()

        /// <summary>
        /// [EF Core] 创建 And 表达式，支持索引器
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <returns>新的表达式</returns>
        public static Expression<Func<TSource, int, bool>> IndexAnd<TSource>() => (u, i) => true;

        #endregion [EF Core] 创建 And 表达式，支持索引器 +/* public static Expression<Func<TSource, int, bool>> IndexAnd<TSource>()

        #region [EF Core] 创建 Or 表达式 +/* public static Expression<Func<TSource, bool>> Or<TSource>()

        /// <summary>
        /// [EF Core] 创建 Or 表达式
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <returns>新的表达式</returns>
        public static Expression<Func<TSource, bool>> Or<TSource>() => u => false;

        #endregion [EF Core] 创建 Or 表达式 +/* public static Expression<Func<TSource, bool>> Or<TSource>()

        #region [EF Core] 创建 Or 表达式，支持索引器 +/*  public static Expression<Func<TSource, int, bool>> IndexOr<TSource>()

        /// <summary>
        /// [EF Core] 创建 Or 表达式，支持索引器
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <returns>新的表达式</returns>
        public static Expression<Func<TSource, int, bool>> IndexOr<TSource>() => (u, i) => false;

        #endregion [EF Core] 创建 Or 表达式，支持索引器 +/*  public static Expression<Func<TSource, int, bool>> IndexOr<TSource>()
    }
}