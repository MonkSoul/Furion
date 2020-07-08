using Fur.DatabaseVisitor.Attributes;
using Fur.Extensions;
using Mapster;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Extensions
{
    /// <summary>
    /// 数据库输入输出参数拓展
    /// </summary>
    public static class SqlInputOutputExtensions
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
            if (type == null || type.IsInterface || type.IsAbstract || type.IsGenericType) return paramValues.ToArray();

            var properities = type?.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (!properities.Any()) return paramValues.ToArray();

            for (int i = 0; i < properities?.Length; i++)
            {
                var property = properities[i];
                var value = property.GetValue(parameterModel) ?? DBNull.Value;

                if (property.PropertyType.IsDefined(typeof(DbParameterAttribute), false))
                {
                    var parameterAttribute = property.GetCustomAttribute<DbParameterAttribute>();
                    paramValues.Add(new SqlParameter(property.Name, value) { Direction = parameterAttribute.Direction });
                    continue;
                }

                paramValues.Add(new SqlParameter(property.Name, value));
            }
            return paramValues.ToArray();
        }
        #endregion

        #region 将 DataTable 转 IEnumerable{T} + public static IEnumerable<T> ToEnumerable<T>(this DataTable dataTable)
        /// <summary>
        /// 将 DataTable 转 <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="dataTable">DataTable</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T> ToEnumerable<T>(this DataTable dataTable)
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
        #endregion

        #region 将 DataTable 转 特定类型 + public static object ToEnumerable(this DataTable dataTable, Type type)
        /// <summary>
        /// 将 DataTable 转 特定类型
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <param name="type">类型</param>
        /// <returns>object</returns>
        public static object ToEnumerable(this DataTable dataTable, Type type)
        {
            var returnType = type.IsGenericType ? type.GenericTypeArguments.FirstOrDefault() : type;

            // 反射创建 List对象，并反射调用 Add方法
            var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(returnType));
            var addMethod = list.GetType().GetMethod("Add");

            var dataTableRows = dataTable.AsEnumerable().ToList();
            if (returnType.IsPrimitivePlusIncludeNullable())
            {
                dataTableRows.ForEach(row =>
                {
                    var rowValue = row[0];
                    addMethod.Invoke(list, new object[] { rowValue.Adapt(rowValue.GetType(), returnType) });
                });
            }
            else
            {
                var propertyInfos = returnType.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
                dataTableRows.ForEach(row =>
                {
                    var obj = Activator.CreateInstance(returnType);
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

            IEnumerable<object> results = returnType.IsPrimitivePlusIncludeNullable()
                ? list.Adapt<IEnumerable<object>>()
                : list as IEnumerable<object>;

            return type.IsGenericType ? results : results.FirstOrDefault();
        }
        #endregion

        #region 将 DataTable 转 IEnumerable{T} + public static Task<IEnumerable<T>> ToEnumerableAsync<T>(this DataTable dataTable)
        /// <summary>
        /// 将 DataTable 转 <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="dataTable">DataTable</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static Task<IEnumerable<T>> ToEnumerableAsync<T>(this DataTable dataTable)
        {
            return Task.FromResult(dataTable.ToEnumerable<T>());
        }
        #endregion

        #region 将异步 DataTable 转 IEnumerable{T} + public static async Task<IEnumerable<T>> ToEnumerableAsync<T>(this Task<DataTable> dataTable)
        /// <summary>
        /// 将异步 DataTable 转 <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="dataTable">DataTable</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static async Task<IEnumerable<T>> ToEnumerableAsync<T>(this Task<DataTable> dataTable)
        {
            var dataTableNoTask = await dataTable;
            return await dataTableNoTask.ToEnumerableAsync<T>();
        }
        #endregion

        #region 将 DataTable 转 特定类型 + public static Task<object> ToEnumerableAsync(this DataTable dataTable, Type type)
        /// <summary>
        /// 将 DataTable 转 特定类型
        /// </summary>
        /// <param name="dataTable"><see cref="DataTable"/></param>
        /// <param name="type">类型</param>
        /// <returns>object</returns>
        public static Task<object> ToEnumerableAsync(this DataTable dataTable, Type type)
        {
            return Task.FromResult(dataTable.ToEnumerable(type));
        }
        #endregion

        #region 将异步 DataTable 转 特定类型 + public static async Task<object> ToEnumerableAsync(this Task<DataTable> dataTable, Type type)
        /// <summary>
        /// 将异步 DataTable 转 特定类型
        /// </summary>
        /// <param name="dataTable"><see cref="DataTable"/></param>
        /// <param name="type">类型</param>
        /// <returns>object</returns>
        public static async Task<object> ToEnumerableAsync(this Task<DataTable> dataTable, Type type)
        {
            var dataTableNoTask = await dataTable;
            return await dataTableNoTask.ToEnumerableAsync(type);
        }
        #endregion
    }
}