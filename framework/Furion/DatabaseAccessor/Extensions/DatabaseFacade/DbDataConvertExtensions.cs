// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Furion.Extensions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库数据转换拓展
/// </summary>
[SuppressSniffer]
public static class DbDataConvertExtensions
{
    /// <summary>
    /// 将 DataTable 转 List 集合
    /// </summary>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <param name="dataTable">DataTable</param>
    /// <returns>List{T}</returns>
    public static List<T> ToList<T>(this DataTable dataTable)
    {
        return dataTable.ToList(typeof(List<T>)) as List<T>;
    }

    /// <summary>
    /// 将 DataTable 转 List 集合
    /// </summary>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <param name="dataTable">DataTable</param>
    /// <returns>List{T}</returns>
    public static async Task<List<T>> ToListAsync<T>(this DataTable dataTable)
    {
        var list = await dataTable.ToListAsync(typeof(List<T>));
        return list as List<T>;
    }

    /// <summary>
    /// 将 DataSet 转 元组
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <param name="dataSet">DataSet</param>
    /// <returns>元组类型</returns>
    public static List<T1> ToList<T1>(this DataSet dataSet)
    {
        var tuple = dataSet.ToList(typeof(List<T1>));
        return tuple[0] as List<T1>;
    }

    /// <summary>
    /// 将 DataSet 转 元组
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <param name="dataSet">DataSet</param>
    /// <returns>元组类型</returns>
    public static (List<T1> list1, List<T2> list2) ToList<T1, T2>(this DataSet dataSet)
    {
        var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>));
        return (tuple[0] as List<T1>, tuple[1] as List<T2>);
    }

    /// <summary>
    /// 将 DataSet 转 元组
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <param name="dataSet">DataSet</param>
    /// <returns>元组类型</returns>
    public static (List<T1> list1, List<T2> list2, List<T3> list3) ToList<T1, T2, T3>(this DataSet dataSet)
    {
        var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>), typeof(List<T3>));
        return (tuple[0] as List<T1>, tuple[1] as List<T2>, tuple[2] as List<T3>);
    }

    /// <summary>
    /// 将 DataSet 转 元组
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <param name="dataSet">DataSet</param>
    /// <returns>元组类型</returns>
    public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) ToList<T1, T2, T3, T4>(this DataSet dataSet)
    {
        var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>), typeof(List<T3>), typeof(List<T4>));
        return (tuple[0] as List<T1>, tuple[1] as List<T2>, tuple[2] as List<T3>, tuple[3] as List<T4>);
    }

    /// <summary>
    /// 将 DataSet 转 元组
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <param name="dataSet">DataSet</param>
    /// <returns>元组类型</returns>
    public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5) ToList<T1, T2, T3, T4, T5>(this DataSet dataSet)
    {
        var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>), typeof(List<T3>), typeof(List<T4>), typeof(List<T5>));
        return (tuple[0] as List<T1>, tuple[1] as List<T2>, tuple[2] as List<T3>, tuple[3] as List<T4>, tuple[4] as List<T5>);
    }

    /// <summary>
    /// 将 DataSet 转 元组
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <typeparam name="T6">元组元素类型</typeparam>
    /// <param name="dataSet">DataSet</param>
    /// <returns>元组类型</returns>
    public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6) ToList<T1, T2, T3, T4, T5, T6>(this DataSet dataSet)
    {
        var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>), typeof(List<T3>), typeof(List<T4>), typeof(List<T5>), typeof(List<T6>));
        return (tuple[0] as List<T1>, tuple[1] as List<T2>, tuple[2] as List<T3>, tuple[3] as List<T4>, tuple[4] as List<T5>, tuple[5] as List<T6>);
    }

    /// <summary>
    /// 将 DataSet 转 元组
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <typeparam name="T6">元组元素类型</typeparam>
    /// <typeparam name="T7">元组元素类型</typeparam>
    /// <param name="dataSet">DataSet</param>
    /// <returns>元组类型</returns>
    public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7) ToList<T1, T2, T3, T4, T5, T6, T7>(this DataSet dataSet)
    {
        var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>), typeof(List<T3>), typeof(List<T4>), typeof(List<T5>), typeof(List<T6>), typeof(List<T7>));
        return (tuple[0] as List<T1>, tuple[1] as List<T2>, tuple[2] as List<T3>, tuple[3] as List<T4>, tuple[4] as List<T5>, tuple[5] as List<T6>, tuple[6] as List<T7>);
    }

    /// <summary>
    /// 将 DataSet 转 元组
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <typeparam name="T6">元组元素类型</typeparam>
    /// <typeparam name="T7">元组元素类型</typeparam>
    /// <typeparam name="T8">元组元素类型</typeparam>
    /// <param name="dataSet">DataSet</param>
    /// <returns>元组类型</returns>
    public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8) ToList<T1, T2, T3, T4, T5, T6, T7, T8>(this DataSet dataSet)
    {
        var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>), typeof(List<T3>), typeof(List<T4>), typeof(List<T5>), typeof(List<T6>), typeof(List<T7>), typeof(List<T8>));
        return (tuple[0] as List<T1>, tuple[1] as List<T2>, tuple[2] as List<T3>, tuple[3] as List<T4>, tuple[4] as List<T5>, tuple[5] as List<T6>, tuple[6] as List<T7>, tuple[7] as List<T8>);
    }

    /// <summary>
    /// 将 DataSet 转 特定类型
    /// </summary>
    /// <param name="dataSet">DataSet</param>
    /// <param name="returnTypes">特定类型集合</param>
    /// <returns>List{object}</returns>
    public static List<object> ToList(this DataSet dataSet, params Type[] returnTypes)
    {
        // 获取所有的 DataTable
        var dataTables = dataSet.Tables;

        // 处理元组类型
        if (returnTypes.Length == 1 && returnTypes[0].IsValueType)
        {
            returnTypes = returnTypes[0].GenericTypeArguments;
        }

        // 处理不传入 returnTypes 类型
        if (returnTypes == null || returnTypes.Length == 0)
        {
            returnTypes = Enumerable.Range(0, dataTables.Count).Select(u => typeof(List<object>)).ToArray();
        }

        // 处理 8 个结果集
        if (returnTypes.Length >= 8)
        {
            return new List<object>
                {
                    dataTables[0].ToList(returnTypes[0]),
                    dataTables[1].ToList(returnTypes[1]),
                    dataTables[2].ToList(returnTypes[2]),
                    dataTables[3].ToList(returnTypes[3]),
                    dataTables[4].ToList(returnTypes[4]),
                    dataTables[5].ToList(returnTypes[5]),
                    dataTables[6].ToList(returnTypes[6]),
                    dataTables[7].ToList(returnTypes[7])
                };
        }
        // 处理 7 个结果集
        else if (returnTypes.Length == 7)
        {
            return new List<object>
                {
                    dataTables[0].ToList(returnTypes[0]),
                    dataTables[1].ToList(returnTypes[1]),
                    dataTables[2].ToList(returnTypes[2]),
                    dataTables[3].ToList(returnTypes[3]),
                    dataTables[4].ToList(returnTypes[4]),
                    dataTables[5].ToList(returnTypes[5]),
                    dataTables[6].ToList(returnTypes[6])
                };
        }
        // 处理 6 个结果集
        else if (returnTypes.Length == 6)
        {
            return new List<object>
                {
                    dataTables[0].ToList(returnTypes[0]),
                    dataTables[1].ToList(returnTypes[1]),
                    dataTables[2].ToList(returnTypes[2]),
                    dataTables[3].ToList(returnTypes[3]),
                    dataTables[4].ToList(returnTypes[4]),
                    dataTables[5].ToList(returnTypes[5])
                };
        }
        // 处理 5 个结果集
        else if (returnTypes.Length == 5)
        {
            return new List<object>
                {
                    dataTables[0].ToList(returnTypes[0]),
                    dataTables[1].ToList(returnTypes[1]),
                    dataTables[2].ToList(returnTypes[2]),
                    dataTables[3].ToList(returnTypes[3]),
                    dataTables[4].ToList(returnTypes[4])
                };
        }
        // 处理 4 个结果集
        else if (returnTypes.Length == 4)
        {
            return new List<object>
                {
                    dataTables[0].ToList(returnTypes[0]),
                    dataTables[1].ToList(returnTypes[1]),
                    dataTables[2].ToList(returnTypes[2]),
                    dataTables[3].ToList(returnTypes[3])
                };
        }
        // 处理 3 个结果集
        else if (returnTypes.Length == 3)
        {
            return new List<object>
                {
                    dataTables[0].ToList(returnTypes[0]),
                    dataTables[1].ToList(returnTypes[1]),
                    dataTables[2].ToList(returnTypes[2])
                };
        }
        // 处理 2 个结果集
        else if (returnTypes.Length == 2)
        {
            return new List<object>
                {
                    dataTables[0].ToList(returnTypes[0]),
                    dataTables[1].ToList(returnTypes[1])
                };
        }
        // 处理 1 个结果集
        else
        {
            return new List<object>
                {
                    dataTables[0].ToList(returnTypes[0])
                };
        }
    }

    /// <summary>
    /// 将 DataSet 转 特定类型
    /// </summary>
    /// <param name="dataSet">DataSet</param>
    /// <param name="returnTypes">特定类型集合</param>
    /// <returns>object</returns>
    public static Task<List<object>> ToListAsync(this DataSet dataSet, params Type[] returnTypes)
    {
        return Task.FromResult(dataSet.ToList(returnTypes));
    }

    /// <summary>
    /// 将 DataTable 转 特定类型
    /// </summary>
    /// <param name="dataTable">DataTable</param>
    /// <param name="returnType">返回值类型</param>
    /// <returns>object</returns>
    public static object ToList(this DataTable dataTable, Type returnType)
    {
        var isGenericType = returnType.IsGenericType;
        // 获取类型真实返回类型
        var underlyingType = isGenericType ? returnType.GenericTypeArguments.First() : (returnType.IsArray ? returnType.GetElementType() : returnType);

        var resultType = typeof(List<>).MakeGenericType(underlyingType);
        var list = Activator.CreateInstance(resultType);
        var addMethod = resultType.GetMethod("Add");

        // 将 DataTable 转为行集合
        var dataRows = dataTable.AsEnumerable();

        // 如果是基元类型
        if (underlyingType.IsRichPrimitive())
        {
            // 遍历所有行
            foreach (var dataRow in dataRows)
            {
                // 只取第一列数据
                var firstColumnValue = dataRow[0];
                // 转换成目标类型数据
                var destValue = firstColumnValue?.ChangeType(underlyingType);
                // 添加到集合中
                _ = addMethod.Invoke(list, new[] { destValue });
            }
        }
        // 处理Object类型
        else if (underlyingType == typeof(object))
        {
            // 获取所有列名
            var columns = dataTable.Columns;

            // 遍历所有行
            foreach (var dataRow in dataRows)
            {
                var dic = new Dictionary<string, object>();
                foreach (DataColumn column in columns)
                {
                    dic.Add(column.ColumnName, dataRow[column]);
                }
                _ = addMethod.Invoke(list, new[] { dic });
            }
        }
        else
        {
            // 二次处理数组类型
            var isArrayType = underlyingType.IsArray;
            var actType = isArrayType ? underlyingType.GetElementType() : underlyingType;

            // 获取所有的数据列和类公开实例属性
            var dataColumns = dataTable.Columns;
            var properties = actType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //.Where(p => !p.IsDefined(typeof(NotMappedAttribute), true));  // sql 数据转换无需判断 [NotMapperd] 特性

            // 遍历所有行
            foreach (var dataRow in dataRows)
            {
                var model = Activator.CreateInstance(actType);

                // 遍历所有属性并一一赋值
                foreach (var property in properties)
                {
                    // 获取属性对应的真实列名
                    var columnName = property.Name;
                    if (property.IsDefined(typeof(ColumnAttribute), true))
                    {
                        var columnAttribute = property.GetCustomAttribute<ColumnAttribute>(true);
                        if (!string.IsNullOrWhiteSpace(columnAttribute.Name)) columnName = columnAttribute.Name;
                    }

                    // 如果 DataTable 不包含该列名，则跳过
                    if (!dataColumns.Contains(columnName))
                    {
                        var splitColumnName = string.Join('_', property.Name.SplitCamelCase());

                        // 继续检查带下划线的
                        if (dataColumns.Contains(splitColumnName)) columnName = splitColumnName;
                        // 检查下划线大写的
                        else if (dataColumns.Contains(splitColumnName.ToUpper())) columnName = splitColumnName.ToUpper();
                        // 检查下划线小写的
                        else if (dataColumns.Contains(splitColumnName.ToLower())) columnName = splitColumnName.ToLower();
                        // 检查全大写的
                        else if (dataColumns.Contains(columnName.ToUpper())) columnName = columnName.ToUpper();
                        // 检查全小写的
                        else if (dataColumns.Contains(columnName.ToLower())) columnName = columnName.ToLower();
                        else continue;
                    }

                    // 获取列值
                    var columnValue = dataRow[columnName];
                    // 如果列值未空，则跳过
                    if (columnValue == DBNull.Value) continue;

                    // 转换成目标类型数据
                    var destValue = columnValue?.ChangeType(property.PropertyType);
                    property.SetValue(model, destValue);
                }

                // 处理数组类型
                object pams;
                if (isArrayType)
                {
                    var listType = typeof(List<>).MakeGenericType(actType);
                    var listArray = Activator.CreateInstance(listType);
                    listType.GetMethod("Add").Invoke(listArray, new[] { model });
                    pams = ((dynamic)listArray).ToArray();
                }
                else pams = model;

                // 添加到集合中
                _ = addMethod.Invoke(list, new[] { pams });
            }
        }

        return list;
    }

    /// <summary>
    /// 将 DataTable 转 特定类型
    /// </summary>
    /// <param name="dataTable">DataTable</param>
    /// <param name="returnType">返回值类型</param>
    /// <returns>object</returns>
    public static Task<object> ToListAsync(this DataTable dataTable, Type returnType)
    {
        return Task.FromResult(dataTable.ToList(returnType));
    }

    /// <summary>
    /// 将 DbDataReader 转 DataTable
    /// </summary>
    /// <param name="dataReader"></param>
    /// <returns></returns>
    public static DataTable ToDataTable(this DbDataReader dataReader)
    {
        var dataTable = new DataTable();

        // 创建列
        for (var i = 0; i < dataReader.FieldCount; i++)
        {
            var dataClumn = new DataColumn
            {
                DataType = dataReader.GetFieldType(i),
                ColumnName = dataReader.GetName(i)
            };

            dataTable.Columns.Add(dataClumn);
        }

        // 循环读取
        while (dataReader.Read())
        {
            // 创建行
            var dataRow = dataTable.NewRow();
            for (var i = 0; i < dataReader.FieldCount; i++)
            {
                dataRow[i] = dataReader[i];
            }

            dataTable.Rows.Add(dataRow);
        }

        return dataTable;
    }

    /// <summary>
    /// 将 DbDataReader 转 DataSet
    /// </summary>
    /// <param name="dataReader"></param>
    /// <returns></returns>
    public static DataSet ToDataSet(this DbDataReader dataReader)
    {
        var dataSet = new DataSet();

        do
        {
            // 获取元数据
            var schemaTable = dataReader.GetSchemaTable();
            var dataTable = new DataTable();

            if (schemaTable != null)
            {
                for (var i = 0; i < schemaTable.Rows.Count; i++)
                {
                    var dataRow = schemaTable.Rows[i];

                    var columnName = (string)dataRow["ColumnName"];
                    var column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                    dataTable.Columns.Add(column);
                }

                dataSet.Tables.Add(dataTable);

                // 循环读取
                while (dataReader.Read())
                {
                    var dataRow = dataTable.NewRow();

                    for (var i = 0; i < dataReader.FieldCount; i++)
                    {
                        dataRow[i] = dataReader.GetValue(i);
                    }

                    dataTable.Rows.Add(dataRow);
                }
            }
            else
            {
                var column = new DataColumn("RecordsAffected");
                dataTable.Columns.Add(column);
                dataSet.Tables.Add(dataTable);

                var dataRow = dataTable.NewRow();
                dataRow[0] = dataReader.RecordsAffected;
                dataTable.Rows.Add(dataRow);
            }
        }

        // 读取下一个结果
        while (dataReader.NextResult());

        return dataSet;
    }

    /// <summary>
    /// 处理元组类型返回值
    /// </summary>
    /// <param name="dataSet">数据集</param>
    /// <param name="tupleType">返回值类型</param>
    /// <returns></returns>
    internal static object ToValueTuple(this DataSet dataSet, Type tupleType)
    {
        // 获取元组最底层类型
        var underlyingTypes = tupleType.GetGenericArguments().Select(u => u.IsGenericType ? u.GetGenericArguments().First() : u);

        var toListMethod = typeof(DbDataConvertExtensions)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .First(u => u.Name == "ToList" && u.IsGenericMethod && u.GetGenericArguments().Length == tupleType.GetGenericArguments().Length)
            .MakeGenericMethod(underlyingTypes.ToArray());

        return toListMethod.Invoke(null, new[] { dataSet });
    }
}