// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Microsoft.Data.SqlClient;
using System.Data;

namespace Fur.DatabaseAccessor
{
    internal static class DbHelpers
    {
        /// <summary>
        /// 创建数据库命令参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="dbParameterAttribute">参数特性</param>
        /// <returns>SqlParameter</returns>
        internal static SqlParameter CreateSqlParameter(string name, object value, DbParameterAttribute dbParameterAttribute)
        {
            // 设置参数方向
            var sqlParameter = new SqlParameter(name, value) { Direction = dbParameterAttribute.Direction };

            // 设置参数数据库类型
            if (dbParameterAttribute.DbType != null)
            {
                var type = dbParameterAttribute.DbType.GetType();
                if (type.IsEnum && typeof(SqlDbType).IsAssignableFrom(type))
                {
                    sqlParameter.SqlDbType = (SqlDbType)dbParameterAttribute.DbType;
                }
            }
            // 设置大小，解决NVarchar，Varchar 问题
            if (dbParameterAttribute.Size > 0)
            {
                sqlParameter.Size = dbParameterAttribute.Size;
            }

            return sqlParameter;
        }
    }
}