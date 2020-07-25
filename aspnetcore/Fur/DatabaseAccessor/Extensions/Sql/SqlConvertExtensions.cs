using Fur.DatabaseAccessor.Attributes;
using Fur.TypeExtensions;
using Mapster;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Extensions.Sql
{
    /// <summary>
    /// Sql 参数及返回值 拓展类
    /// </summary>
    public static class SqlConvertExtensions
    {
        #region 将模型转换为 SqlParameter 数组 + public static SqlParameter[] ToSqlParameters(this object parameterModel)

        /// <summary>
        /// 将模型转换为 <see cref="SqlParameter"/> 数组
        /// </summary>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static SqlParameter[] ToSqlParameters(this object parameterModel)
        {
            var paramValues = new List<SqlParameter>();
            var type = parameterModel?.GetType();
            if (type == null || type.IsInterface || type.IsAbstract) return paramValues.ToArray();

            var properities = type?.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (!properities.Any()) return paramValues.ToArray();

            for (int i = 0; i < properities?.Length; i++)
            {
                var property = properities[i];
                var value = property.GetValue(parameterModel) ?? DBNull.Value;

                if (property.IsDefined(typeof(DbParameterAttribute), false))
                {
                    var parameterAttribute = property.GetCustomAttribute<DbParameterAttribute>();
                    paramValues.Add(new SqlParameter(property.Name, value) { Direction = parameterAttribute.Direction });
                    continue;
                }

                paramValues.Add(new SqlParameter(property.Name, value));
            }
            return paramValues.ToArray();
        }

        #endregion 将模型转换为 SqlParameter 数组 + public static SqlParameter[] ToSqlParameters(this object parameterModel)

        #region 将方法参数转为 SqlParameter 数组 + public static SqlParameter[] ToSqlParameters(this ParameterInfo[] parameterInfos, object[] arguments)

        /// <summary>
        /// 将方法参数转为 SqlParameter 数组
        /// </summary>
        /// <param name="parameterInfos">参数集合</param>
        /// <param name="arguments">参数值集合</param>
        /// <returns><see cref="SqlParameterCollection"/></returns>
        public static SqlParameter[] ToSqlParameters(this ParameterInfo[] parameterInfos, object[] arguments)
        {
            var paramValues = new List<SqlParameter>();
            var parameterLength = parameterInfos.Length;
            if (parameterLength == 0) return paramValues.ToArray();

            if (parameterLength > 1 || parameterInfos.First().ParameterType.IsPrimitivePlusIncludeNullable())
            {
                for (int i = 0; i < parameterLength; i++)
                {
                    paramValues.Add(new SqlParameter(parameterInfos[i].Name, arguments[i] ?? DBNull.Value));
                }

                return paramValues.ToArray();
            }
            else return arguments.FirstOrDefault().ToSqlParameters();
        }

        #endregion 将方法参数转为 SqlParameter 数组 + public static SqlParameter[] ToSqlParameters(this ParameterInfo[] parameterInfos, object[] arguments)


        #region 将 DataTable 转 IEnumerable{T} + public static IEnumerable<T> ToList<T>(this DataTable dataTable)

        /// <summary>
        /// 将 DataTable 转 <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="dataTable">DataTable</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T> ToList<T>(this DataTable dataTable)
        {
            var list = new List<T>();
            var dataTableRows = dataTable.AsEnumerable().ToList();
            var returnType = typeof(T);

            if (returnType.IsPrimitivePlusIncludeNullable()) dataTableRows.ForEach(row => list.Add(row[0].Adapt<T>()));
            else
            {
                var propertyInfos = returnType.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
                dataTableRows.ForEach(row =>
                {
                    var obj = Activator.CreateInstance<T>();
                    propertyInfos.ForEach(p =>
                    {
                        var columnName = p.Name;
                        if (p.IsDefined(typeof(ColumnAttribute))) columnName = p.GetCustomAttribute<ColumnAttribute>().Name;

                        var rowValue = row[columnName];
                        if (dataTable.Columns.IndexOf(columnName) != -1 && rowValue != DBNull.Value)
                        {
                            p.SetPropertyValue(obj, rowValue);
                        }
                    });
                    list.Add(obj);
                });
            }
            return list;
        }

        #endregion 将 DataTable 转 IEnumerable{T} + public static IEnumerable<T> ToList<T>(this DataTable dataTable)

        #region 将 DataTable 转 IEnumerable{T} + public static Task<IEnumerable<T>> ToListAsync<T>(this DataTable dataTable)

        /// <summary>
        /// 将 DataTable 转 <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="dataTable">DataTable</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static Task<IEnumerable<T>> ToListAsync<T>(this DataTable dataTable)
        {
            return Task.FromResult(dataTable.ToList<T>());
        }

        #endregion 将 DataTable 转 IEnumerable{T} + public static Task<IEnumerable<T>> ToListAsync<T>(this DataTable dataTable)

        #region 将 DataTable 转 IEnumerable{T} + public static async Task<IEnumerable<T>> ToListAsync<T>(this Task<DataTable> dataTable)

        /// <summary>
        /// 将 DataTable 转 <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="dataTable">DataTable</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static async Task<IEnumerable<T>> ToListAsync<T>(this Task<DataTable> dataTable)
        {
            var dataTableNoTask = await dataTable;
            return await dataTableNoTask.ToListAsync<T>();
        }

        #endregion 将 DataTable 转 IEnumerable{T} + public static async Task<IEnumerable<T>> ToListAsync<T>(this Task<DataTable> dataTable)

        #region 将 DataTable 转 特定类型 + public static Task<object> ToListAsync(this DataTable dataTable, Type returnType)

        /// <summary>
        /// 将 DataTable 转 特定类型
        /// </summary>
        /// <param name="dataTable"><see cref="DataTable"/></param>
        /// <param name="returnType">结果集类型</param>
        /// <returns>object</returns>
        public static Task<object> ToListAsync(this DataTable dataTable, Type returnType)
        {
            return Task.FromResult(dataTable.ToList(returnType));
        }

        #endregion 将 DataTable 转 特定类型 + public static Task<object> ToListAsync(this DataTable dataTable, Type returnType)

        #region 将 DataTable 转 特定类型 + public static async Task<object> ToListAsync(this Task<DataTable> dataTable, Type returnType)

        /// <summary>
        /// 将 DataTable 转 特定类型
        /// </summary>
        /// <param name="dataTable"><see cref="DataTable"/></param>
        /// <param name="returnType">结果集类型</param>
        /// <returns>object</returns>
        public static async Task<object> ToListAsync(this Task<DataTable> dataTable, Type returnType)
        {
            var dataTableNoTask = await dataTable;
            return await dataTableNoTask.ToListAsync(returnType);
        }

        #endregion 将 DataTable 转 特定类型 + public static async Task<object> ToListAsync(this Task<DataTable> dataTable, Type returnType)

        #region 将 DataTable 转 特定类型 + public static object ToList(this DataTable dataTable, Type returnType)

        /// <summary>
        /// 将 DataTable 转 特定类型
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <param name="returnType">类型</param>
        /// <returns>object</returns>
        public static object ToList(this DataTable dataTable, Type returnType)
        {
            var genericType = returnType.IsGenericType ? returnType.GenericTypeArguments.FirstOrDefault() : returnType;

            // 反射创建 List对象，并反射调用 Add方法
            var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType));
            var addMethod = list.GetType().GetMethod("Add");

            var dataTableRows = dataTable.AsEnumerable().ToList();
            if (genericType.IsPrimitivePlusIncludeNullable())
            {
                dataTableRows.ForEach(row =>
                {
                    var rowValue = row[0];
                    // 可能可空类型有问题
                    addMethod.Invoke(list, new object[] { rowValue.Adapt(rowValue.GetType(), genericType) });
                });
            }
            else
            {
                var propertyInfos = genericType.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
                dataTableRows.ForEach(row =>
                {
                    var obj = Activator.CreateInstance(genericType);
                    propertyInfos.ForEach(p =>
                    {
                        var columnName = p.Name;
                        if (p.IsDefined(typeof(ColumnAttribute))) columnName = p.GetCustomAttribute<ColumnAttribute>().Name;

                        var rowValue = row[columnName];
                        if (dataTable.Columns.IndexOf(columnName) != -1 && rowValue != DBNull.Value)
                        {
                            p.SetPropertyValue(obj, rowValue);
                        }
                    });
                    addMethod.Invoke(list, new object[] { obj });
                });
            }

            IEnumerable<object> results = genericType.IsPrimitivePlusIncludeNullable()
                ? list.Adapt<IEnumerable<object>>()
                : list as IEnumerable<object>;

            return returnType.IsGenericType ? results : results.FirstOrDefault();
        }

        #endregion 将 DataTable 转 特定类型 + public static object ToList(this DataTable dataTable, Type returnType)
    }
}