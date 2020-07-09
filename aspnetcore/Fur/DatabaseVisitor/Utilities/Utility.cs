using Fur.Linq.Extensions;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Fur.DatabaseVisitor.Utilities
{
    /// <summary>
    /// 数据库操作层工具类
    /// </summary>
    internal static class Utility
    {
        #region 纠正 SqlParameter 参数 + internal static void RectifySqlParameters(ref DbCommand dbCommand, params object[] parameters)
        /// <summary>
        /// 纠正 <see cref="SqlParameter"/> 参数
        /// </summary>
        /// <param name="dbCommand"><see cref="DbCommand"/> 参数</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        internal static void RectifySqlParameters(ref DbCommand dbCommand, params object[] parameters)
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
    }
}