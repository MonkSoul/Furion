using Fur.DatabaseVisitor.Attributes;
using Fur.DatabaseVisitor.Enums;
using Fur.Linq.Extensions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace Fur.DatabaseVisitor.Helpers
{
    internal class Helper
    {

        #region 修复和合并数据库命令参数 -/* internal static void FixedAndCombineSqlParameters(ref DbCommand dbCommand, params object[] parameters)
        /// <summary>
        /// 修复和合并数据库命令参数
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <param name="parameters"></param>
        internal static void FixedAndCombineSqlParameters(ref DbCommand dbCommand, params object[] parameters)
        {
            if (parameters.IsNullOrEmpty()) return;

            foreach (SqlParameter parameter in parameters)
            {
                if (!parameter.ParameterName.Contains("@"))
                {
                    parameter.ParameterName = $"@{parameter.ParameterName}";
                }
                dbCommand.Parameters.Add(parameter);
            }
        }
        #endregion

        #region 组合存储过程/函数Sql语句 + internal static (string sql, SqlParameter[] parameters) CombineExecuteSql(ExcuteSqlOptions excuteSqlOptions, string name, object parameterModel = null) 
        /// <summary>
        /// 组合存储过程/函数Sql语句
        /// </summary>
        /// <typeparam name="TParameterModel"></typeparam>
        /// <param name="excuteSqlOptions"></param>
        /// <param name="name"></param>
        /// <param name="parameterModel"></param>
        /// <returns></returns>
        internal static (string sql, SqlParameter[] parameters) CombineExecuteSql(DbCanExecuteTypeOptions excuteSqlOptions, string name, object parameterModel = null)
        {
            var type = parameterModel?.GetType();
            var properities = type?.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var paramValues = new List<SqlParameter>();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"{(excuteSqlOptions == DbCanExecuteTypeOptions.DbProcedure ? "EXECUTE" : "SELECT")} {name}{(excuteSqlOptions == DbCanExecuteTypeOptions.DbProcedure ? "" : "(")}");

            for (int i = 0; i < properities?.Length; i++)
            {
                var property = properities[i];
                if (parameterModel != null)
                {
                    var value = property.GetValue(parameterModel);
                    paramValues.Add(new SqlParameter(property.Name, value ?? DBNull.Value));
                }

                if (excuteSqlOptions == DbCanExecuteTypeOptions.DbProcedure)
                {
                    if (!property.PropertyType.IsDefined(typeof(ParameterAttribute), false))
                    {
                        stringBuilder.Append($" @{property.Name},");
                    }
                    else
                    {
                        var parameterAttribute = property.GetCustomAttribute<ParameterAttribute>();
                        stringBuilder.Append($" @{parameterAttribute.Name}=@{property.Name},");
                    }
                }
                else
                {
                    stringBuilder.Append($" @{property.Name},");
                }
            }
            var sql = stringBuilder.ToString();
            if (sql.EndsWith(",")) sql = sql[0..^1];

            if (excuteSqlOptions == DbCanExecuteTypeOptions.DbFunction)
            {
                sql += (")");
            }

            return (sql, paramValues.ToArray());
        }
        #endregion

        /// <summary>
        /// 组合存储过程/函数Sql语句
        /// </summary>
        /// <param name="excuteSqlOptions"></param>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal static string CombineExecuteSql(DbCanExecuteTypeOptions excuteSqlOptions, string name, params object[] parameters)
        {
            var sqlParameters = (SqlParameter[])parameters;
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"{(excuteSqlOptions == DbCanExecuteTypeOptions.DbProcedure ? "EXECUTE" : "SELECT")} {name}{(excuteSqlOptions == DbCanExecuteTypeOptions.DbProcedure ? "" : "(")}");

            for (int i = 0; i < sqlParameters.Length; i++)
            {
                SqlParameter sqlParameter = sqlParameters[i];
                stringBuilder.Append($" @{(sqlParameter.ParameterName.Replace("@", ""))},");
            }

            var sql = stringBuilder.ToString();
            if (sql.EndsWith(",")) sql = sql[0..^1];

            if (excuteSqlOptions == DbCanExecuteTypeOptions.DbFunction)
            {
                sql += (")");
            }

            return sql;
        }
    }
}
