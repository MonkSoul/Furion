using Fur.ApplicationBase.Attributes;
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
    [NonWrapper]
    public static class SqlConvertExtensions
    {
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
                var columns = dataTable.Columns;

                var propertyInfos = returnType.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
                dataTableRows.ForEach(row =>
                {
                    var obj = Activator.CreateInstance<T>();

                    foreach (var p in propertyInfos)
                    {
                        if (p.IsDefined(typeof(NotMappedAttribute), false)) continue;

                        var columnName = p.Name;
                        if (p.IsDefined(typeof(ColumnAttribute), false)) columnName = p.GetCustomAttribute<ColumnAttribute>().Name;
                        if (!columns.Contains(columnName)) continue;

                        var rowValue = row[columnName];
                        if (dataTable.Columns.IndexOf(columnName) != -1 && rowValue != DBNull.Value)
                        {
                            p.SetPropertyValue(obj, rowValue);
                        }
                    }

                    list.Add(obj);
                });
            }
            return list;
        }

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
                var columns = dataTable.Columns;
                var propertyInfos = genericType.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
                dataTableRows.ForEach(row =>
                {
                    var obj = Activator.CreateInstance(genericType);
                    foreach (var p in propertyInfos)
                    {
                        if (p.IsDefined(typeof(NotMappedAttribute), false)) continue;

                        var columnName = p.Name;
                        if (p.IsDefined(typeof(ColumnAttribute), false)) columnName = p.GetCustomAttribute<ColumnAttribute>().Name;
                        if (!columns.Contains(columnName)) continue;

                        var rowValue = row[columnName];
                        if (dataTable.Columns.IndexOf(columnName) != -1 && rowValue != DBNull.Value)
                        {
                            p.SetPropertyValue(obj, rowValue);
                        }
                    }
                    addMethod.Invoke(list, new object[] { obj });
                });
            }

            IEnumerable<object> results = genericType.IsPrimitivePlusIncludeNullable()
                ? list.Adapt<IEnumerable<object>>()
                : list as IEnumerable<object>;

            return returnType.IsGenericType ? results : results.FirstOrDefault();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T1> ToList<T1>(this DataSet dataSet)
        {
            return dataSet.ToList(typeof(IEnumerable<T1>))
                .Adapt<IEnumerable<T1>>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2) ToList<T1, T2>(this DataSet dataSet)
        {
            return dataSet.ToList(typeof(IEnumerable<T1>), typeof(IEnumerable<T2>))
                .Adapt<(IEnumerable<T1>, IEnumerable<T2>)>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2, T3}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <typeparam name="T3">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2, T3}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) ToList<T1, T2, T3>(this DataSet dataSet)
        {
            return dataSet.ToList(typeof(IEnumerable<T1>), typeof(IEnumerable<T2>), typeof(IEnumerable<T3>))
                 .Adapt<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>)>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2, T3, T4}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <typeparam name="T3">返回值类型</typeparam>
        /// <typeparam name="T4">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) ToList<T1, T2, T3, T4>(this DataSet dataSet)
        {
            return dataSet.ToList(typeof(IEnumerable<T1>), typeof(IEnumerable<T2>), typeof(IEnumerable<T3>), typeof(IEnumerable<T4>))
                 .Adapt<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>)>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2, T3, T4, T5}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <typeparam name="T3">返回值类型</typeparam>
        /// <typeparam name="T4">返回值类型</typeparam>
        /// <typeparam name="T5">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) ToList<T1, T2, T3, T4, T5>(this DataSet dataSet)
        {
            return dataSet.ToList(typeof(IEnumerable<T1>), typeof(IEnumerable<T2>), typeof(IEnumerable<T3>), typeof(IEnumerable<T4>), typeof(IEnumerable<T5>))
                 .Adapt<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>)>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2, T3, T4, T5, T6}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <typeparam name="T3">返回值类型</typeparam>
        /// <typeparam name="T4">返回值类型</typeparam>
        /// <typeparam name="T5">返回值类型</typeparam>
        /// <typeparam name="T6">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) ToList<T1, T2, T3, T4, T5, T6>(this DataSet dataSet)
        {
            return dataSet.ToList(typeof(IEnumerable<T1>), typeof(IEnumerable<T2>), typeof(IEnumerable<T3>), typeof(IEnumerable<T4>), typeof(IEnumerable<T5>), typeof(IEnumerable<T6>))
                .Adapt<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>)>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <typeparam name="T3">返回值类型</typeparam>
        /// <typeparam name="T4">返回值类型</typeparam>
        /// <typeparam name="T5">返回值类型</typeparam>
        /// <typeparam name="T6">返回值类型</typeparam>
        /// <typeparam name="T7">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) ToList<T1, T2, T3, T4, T5, T6, T7>(this DataSet dataSet)
        {
            return dataSet.ToList(typeof(IEnumerable<T1>), typeof(IEnumerable<T2>), typeof(IEnumerable<T3>), typeof(IEnumerable<T4>), typeof(IEnumerable<T5>), typeof(IEnumerable<T6>), typeof(IEnumerable<T7>))
                .Adapt<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>)>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, T8}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <typeparam name="T3">返回值类型</typeparam>
        /// <typeparam name="T4">返回值类型</typeparam>
        /// <typeparam name="T5">返回值类型</typeparam>
        /// <typeparam name="T6">返回值类型</typeparam>
        /// <typeparam name="T7">返回值类型</typeparam>
        /// <typeparam name="T8">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, T8}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) ToList<T1, T2, T3, T4, T5, T6, T7, T8>(this DataSet dataSet)
        {
            return dataSet.ToList(typeof(IEnumerable<T1>), typeof(IEnumerable<T2>), typeof(IEnumerable<T3>), typeof(IEnumerable<T4>), typeof(IEnumerable<T5>), typeof(IEnumerable<T6>), typeof(IEnumerable<T7>), typeof(IEnumerable<T8>))
                .Adapt<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, IEnumerable<T8>)>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static async Task<IEnumerable<T1>> ToListAsync<T1>(this DataSet dataSet)
        {
            var result = await dataSet.ToListAsync(typeof(IEnumerable<T1>));
            return result.Adapt<IEnumerable<T1>>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> ToListAsync<T1, T2>(this DataSet dataSet)
        {
            var result = await dataSet.ToListAsync(typeof(IEnumerable<T1>), typeof(IEnumerable<T2>));
            return result.Adapt<(IEnumerable<T1>, IEnumerable<T2>)>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2, T3}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <typeparam name="T3">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2, T3}"/></returns>
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> ToListAsync<T1, T2, T3>(this DataSet dataSet)
        {
            var result = await dataSet.ToListAsync(typeof(IEnumerable<T1>), typeof(IEnumerable<T2>), typeof(IEnumerable<T3>));
            return result.Adapt<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>)>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2, T3, T4}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <typeparam name="T3">返回值类型</typeparam>
        /// <typeparam name="T4">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4}"/></returns>
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> ToListAsync<T1, T2, T3, T4>(this DataSet dataSet)
        {
            var result = await dataSet.ToListAsync(typeof(IEnumerable<T1>), typeof(IEnumerable<T2>), typeof(IEnumerable<T3>), typeof(IEnumerable<T4>));
            return result.Adapt<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>)>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2, T3, T4, T5}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <typeparam name="T3">返回值类型</typeparam>
        /// <typeparam name="T4">返回值类型</typeparam>
        /// <typeparam name="T5">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5}"/></returns>
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> ToListAsync<T1, T2, T3, T4, T5>(this DataSet dataSet)
        {
            var result = await dataSet.ToListAsync(typeof(IEnumerable<T1>), typeof(IEnumerable<T2>), typeof(IEnumerable<T3>), typeof(IEnumerable<T4>), typeof(IEnumerable<T5>));
            return result.Adapt<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>)>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2, T3, T4, T5, T6}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <typeparam name="T3">返回值类型</typeparam>
        /// <typeparam name="T4">返回值类型</typeparam>
        /// <typeparam name="T5">返回值类型</typeparam>
        /// <typeparam name="T6">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6}"/></returns>
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> ToListAsync<T1, T2, T3, T4, T5, T6>(this DataSet dataSet)
        {
            var result = await dataSet.ToListAsync(typeof(IEnumerable<T1>), typeof(IEnumerable<T2>), typeof(IEnumerable<T3>), typeof(IEnumerable<T4>), typeof(IEnumerable<T5>), typeof(IEnumerable<T6>));
            return result.Adapt<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>)>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <typeparam name="T3">返回值类型</typeparam>
        /// <typeparam name="T4">返回值类型</typeparam>
        /// <typeparam name="T5">返回值类型</typeparam>
        /// <typeparam name="T6">返回值类型</typeparam>
        /// <typeparam name="T7">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7}"/></returns>
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> ToListAsync<T1, T2, T3, T4, T5, T6, T7>(this DataSet dataSet)
        {
            var result = await dataSet.ToListAsync(typeof(IEnumerable<T1>), typeof(IEnumerable<T2>), typeof(IEnumerable<T3>), typeof(IEnumerable<T4>), typeof(IEnumerable<T5>), typeof(IEnumerable<T6>), typeof(IEnumerable<T7>));
            return result.Adapt<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>)>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, T8}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <typeparam name="T3">返回值类型</typeparam>
        /// <typeparam name="T4">返回值类型</typeparam>
        /// <typeparam name="T5">返回值类型</typeparam>
        /// <typeparam name="T6">返回值类型</typeparam>
        /// <typeparam name="T7">返回值类型</typeparam>
        /// <typeparam name="T8">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, T8}"/></returns>
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> ToListAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DataSet dataSet)
        {
            var result = await dataSet.ToListAsync(typeof(IEnumerable<T1>), typeof(IEnumerable<T2>), typeof(IEnumerable<T3>), typeof(IEnumerable<T4>), typeof(IEnumerable<T5>), typeof(IEnumerable<T6>), typeof(IEnumerable<T7>), typeof(IEnumerable<T8>));
            return result.Adapt<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, IEnumerable<T8>)>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static async Task<IEnumerable<T1>> ToListAsync<T1>(this Task<DataSet> dataSet)
        {
            var dataSetNoTask = await dataSet;
            return await dataSetNoTask.ToListAsync<T1>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> ToListAsync<T1, T2>(this Task<DataSet> dataSet)
        {
            var dataSetNoTask = await dataSet;
            return await dataSetNoTask.ToListAsync<T1, T2>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2, T3}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <typeparam name="T3">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2, T3}"/></returns>
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> ToListAsync<T1, T2, T3>(this Task<DataSet> dataSet)
        {
            var dataSetNoTask = await dataSet;
            return await dataSetNoTask.ToListAsync<T1, T2, T3>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2, T3, T4}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <typeparam name="T3">返回值类型</typeparam>
        /// <typeparam name="T4">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4}"/></returns>
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> ToListAsync<T1, T2, T3, T4>(this Task<DataSet> dataSet)
        {
            var dataSetNoTask = await dataSet;
            return await dataSetNoTask.ToListAsync<T1, T2, T3, T4>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2, T3, T4, T5}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <typeparam name="T3">返回值类型</typeparam>
        /// <typeparam name="T4">返回值类型</typeparam>
        /// <typeparam name="T5">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5}"/></returns>
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> ToListAsync<T1, T2, T3, T4, T5>(this Task<DataSet> dataSet)
        {
            var dataSetNoTask = await dataSet;
            return await dataSetNoTask.ToListAsync<T1, T2, T3, T4, T5>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2, T3, T4, T5, T6}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <typeparam name="T3">返回值类型</typeparam>
        /// <typeparam name="T4">返回值类型</typeparam>
        /// <typeparam name="T5">返回值类型</typeparam>
        /// <typeparam name="T6">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6}"/></returns>
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> ToListAsync<T1, T2, T3, T4, T5, T6>(this Task<DataSet> dataSet)
        {
            var dataSetNoTask = await dataSet;
            return await dataSetNoTask.ToListAsync<T1, T2, T3, T4, T5, T6>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <typeparam name="T3">返回值类型</typeparam>
        /// <typeparam name="T4">返回值类型</typeparam>
        /// <typeparam name="T5">返回值类型</typeparam>
        /// <typeparam name="T6">返回值类型</typeparam>
        /// <typeparam name="T7">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7}"/></returns>
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> ToListAsync<T1, T2, T3, T4, T5, T6, T7>(this Task<DataSet> dataSet)
        {
            var dataSetNoTask = await dataSet;
            return await dataSetNoTask.ToListAsync<T1, T2, T3, T4, T5, T6, T7>();
        }

        /// <summary>
        /// 将 DataSet 转 <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, T8}"/>
        /// </summary>
        /// <typeparam name="T1">返回值类型</typeparam>
        /// <typeparam name="T2">返回值类型</typeparam>
        /// <typeparam name="T3">返回值类型</typeparam>
        /// <typeparam name="T4">返回值类型</typeparam>
        /// <typeparam name="T5">返回值类型</typeparam>
        /// <typeparam name="T6">返回值类型</typeparam>
        /// <typeparam name="T7">返回值类型</typeparam>
        /// <typeparam name="T8">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, T8}"/></returns>
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> ToListAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this Task<DataSet> dataSet)
        {
            var dataSetNoTask = await dataSet;
            return await dataSetNoTask.ToListAsync<T1, T2, T3, T4, T5, T6, T7, T8>();
        }

        /// <summary>
        /// 将 Dataset 数据集转元组对象
        /// </summary>
        /// <param name="dataset">数据集</param>
        /// <param name="returnTypes">结果集类型数组</param>
        /// <returns>object</returns>
        public static object ToList(this DataSet dataset, params Type[] returnTypes)
        {
            if (dataset.Tables.Count >= 8)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]), dataset.Tables[2].ToList(returnTypes[2]), dataset.Tables[3].ToList(returnTypes[3]), dataset.Tables[4].ToList(returnTypes[4]), dataset.Tables[5].ToList(returnTypes[5]), dataset.Tables[6].ToList(returnTypes[6]), dataset.Tables[7].ToList(returnTypes[7]));
            }
            else if (dataset.Tables.Count == 7)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]), dataset.Tables[2].ToList(returnTypes[2]), dataset.Tables[3].ToList(returnTypes[3]), dataset.Tables[4].ToList(returnTypes[4]), dataset.Tables[5].ToList(returnTypes[5]), dataset.Tables[6].ToList(returnTypes[6]));
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]), dataset.Tables[2].ToList(returnTypes[2]), dataset.Tables[3].ToList(returnTypes[3]), dataset.Tables[4].ToList(returnTypes[4]), dataset.Tables[5].ToList(returnTypes[5]));
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]), dataset.Tables[2].ToList(returnTypes[2]), dataset.Tables[3].ToList(returnTypes[3]), dataset.Tables[4].ToList(returnTypes[4]));
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]), dataset.Tables[2].ToList(returnTypes[2]), dataset.Tables[3].ToList(returnTypes[3]));
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]), dataset.Tables[2].ToList(returnTypes[2]));
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]));
            }
            else if (dataset.Tables.Count == 1)
            {
                return dataset.Tables[0].ToList(returnTypes[0]);
            }
            return default;
        }

        /// <summary>
        /// 将 Dataset 数据集转元组对象
        /// </summary>
        /// <param name="dataset">数据集</param>
        /// <param name="returnTypes">结果集类型数组</param>
        /// <returns>object</returns>
        public static Task<object> ToListAsync(this DataSet dataset, params Type[] returnTypes)
        {
            return Task.FromResult(dataset.ToList(returnTypes));
        }

        /// <summary>
        /// 将 Dataset 数据集转元组对象
        /// </summary>
        /// <param name="dataset">数据集</param>
        /// <param name="returnTypes">结果集类型数组</param>
        /// <returns>object</returns>
        public static async Task<object> ToListAsync(this Task<DataSet> dataset, params Type[] returnTypes)
        {
            var dataSetNoTask = await dataset;
            return await dataSetNoTask.ToListAsync(returnTypes);
        }
    }
}