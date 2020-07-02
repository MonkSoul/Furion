using Fur.Extensions;
using Fur.Linq.Extensions;
using Mapster;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Extensions
{
    public static class SqlModelExtensions
    {
        #region 类转SqlParameter参数 +/* public static SqlParameter[] ToSqlParameters<TParameterModel>(this TParameterModel parameterModel) where TParameterModel : class
        /// <summary>
        /// 类转SqlParameter参数
        /// </summary>
        /// <typeparam name="TParameterModel">类泛型类型</typeparam>
        /// <param name="parameterModel">类泛型类型值</param>
        /// <returns>SqlParameter[]</returns>
        public static SqlParameter[] ToSqlParameters<TParameterModel>(this TParameterModel parameterModel) where TParameterModel : class
        {
            var type = parameterModel.GetType();
            var properities = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var paramValues = new List<SqlParameter>();

            for (int i = 0; i < properities?.Length; i++)
            {
                var property = properities[i];
                if (parameterModel != null)
                {
                    var value = property.GetValue(parameterModel);
                    paramValues.Add(new SqlParameter(property.Name, value ?? DBNull.Value));
                }
            }
            return paramValues.ToArray();
        }
        #endregion

        #region DataTable转对象集合 +/* public static IEnumerable<T> ToEnumerable<T>(this DataTable dataTable)
        /// <summary>
        /// DataTable转对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToEnumerable<T>(this DataTable dataTable)
        {
            var list = new List<T>();
            var dataTables = dataTable.AsEnumerable().ToList();
            var returnType = typeof(T);

            if (returnType.IsValueType || typeof(string).Equals(returnType))
            {
                dataTables.ForEach(row => list.Add(row[0].Adapt<T>()));
            }
            else
            {
                var propertyInfos = returnType.GetProperties().ToList();
                dataTables.ForEach(row =>
                {
                    var obj = Activator.CreateInstance<T>();
                    propertyInfos.ForEach(p =>
                    {
                        var columnName = p.Name;
                        if (p.IsDefined(typeof(ColumnAttribute))) columnName = p.GetCustomAttribute<ColumnAttribute>().Name;

                        if (dataTable.Columns.IndexOf(columnName) != -1 && row[columnName] != DBNull.Value)
                        {
                            p.SetPropertyValue(obj, row[columnName]);
                        }
                    });
                    list.Add(obj);
                });
            }
            return list;
        }
        #endregion

        #region DataTable转对象集合 + public static object ToEnumerable(this DataTable dataTable, object obj)
        /// <summary>
        /// DataTable转对象集合
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object ToEnumerable(this DataTable dataTable, object obj)
        {
            var type = obj as Type;
            var returnType = type.IsGenericType ? type.GenericTypeArguments.FirstOrDefault() : type;

            var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(returnType));
            var dataTables = dataTable.AsEnumerable().ToList();

            if (returnType.IsValueType || typeof(string).Equals(returnType))
            {
                dataTables.ForEach(row =>
                {
                    list.GetType().GetMethod("Add").Invoke(list, new object[] { row[0].Adapt(row[0].GetType(), returnType) });
                });
            }
            else
            {
                var propertyInfos = returnType.GetProperties().ToList();
                dataTables.ForEach(row =>
                {
                    var obj = Activator.CreateInstance(returnType);
                    propertyInfos.ForEach(p =>
                    {
                        var columnName = p.Name;
                        if (p.IsDefined(typeof(ColumnAttribute))) columnName = p.GetCustomAttribute<ColumnAttribute>().Name;

                        if (dataTable.Columns.IndexOf(columnName) != -1 && row[columnName] != DBNull.Value)
                        {
                            p.SetPropertyValue(obj, row[columnName]);
                        }
                    });
                    list.GetType().GetMethod("Add").Invoke(list, new object[] { obj });
                });
            }
            var results = list as IEnumerable<object>;
            return type.IsGenericType ? results : results.FirstOrDefault();
        }
        #endregion

        public static Task<IEnumerable<T>> ToEnumerableAsync<T>(this DataTable dataTable)
        {
            return Task.FromResult(dataTable.ToEnumerable<T>());
        }

        public static async Task<IEnumerable<T>> ToEnumerableAsync<T>(this Task<DataTable> dataTable)
        {
            var dataTableNoTask = await dataTable;
            return await dataTableNoTask.ToEnumerableAsync<T>();
        }

        public static Task<object> ToEnumerableAsync(this DataTable dataTable, object obj)
        {
            return Task.FromResult(dataTable.ToEnumerable(obj));
        }

        public static async Task<object> ToEnumerableAsync(this Task<DataTable> dataTable, object obj)
        {
            var dataTableNoTask = await dataTable;
            return await dataTableNoTask.ToEnumerableAsync(obj);
        }
    }
}
