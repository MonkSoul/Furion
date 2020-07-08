using Fur.Linq.Extensions;
using Microsoft.Data.SqlClient;
using System.Data.Common;

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
        internal static void FixedSqlParameters(ref DbCommand dbCommand, params object[] parameters)
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

        #endregion 修复和合并数据库命令参数 -/* internal static void FixedAndCombineSqlParameters(ref DbCommand dbCommand, params object[] parameters)
    }
}