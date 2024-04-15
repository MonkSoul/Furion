// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

namespace SqlSugar;

/// <summary>
/// SqlSugar 打印SQL语句参数格式化帮助类
/// 【使用方式】：在需要打印SQL语句的地方，如 Startup，将
/// App.PrintToMiniProfiler("SqlSugar1", "Info", sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
/// 替换为
/// App.PrintToMiniProfiler("SqlSugar", "Info", SqlProfiler.ParameterFormat(sql, pars));
/// </summary>
public class SqlProfiler
{
    /// <summary>
    /// 格式化参数拼接成完整的SQL语句
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="pars"></param>
    /// <returns></returns>
    public static string ParameterFormat(string sql, SugarParameter[] pars)
    {
        //var aa = pars.ToDictionary(it => it.ParameterName, it => it.Value);

        //应逆向替换，否则由于 SqlSugar 多个表的过滤器问题导致替换不完整  如 @TenantId1  @TenantId10
        for (var i = pars.Length - 1; i >= 0; i--)
        {
            if (pars[i].DbType == System.Data.DbType.String
                || pars[i].DbType == System.Data.DbType.DateTime
                || pars[i].DbType == System.Data.DbType.Date
                || pars[i].DbType == System.Data.DbType.Time
                || pars[i].DbType == System.Data.DbType.DateTime2
                || pars[i].DbType == System.Data.DbType.DateTimeOffset
                || pars[i].DbType == System.Data.DbType.Guid
                || pars[i].DbType == System.Data.DbType.VarNumeric
                || pars[i].DbType == System.Data.DbType.AnsiStringFixedLength
                || pars[i].DbType == System.Data.DbType.AnsiString
                || pars[i].DbType == System.Data.DbType.StringFixedLength)
            {
                sql = sql.Replace(pars[i].ParameterName, "'" + pars[i].Value?.ToString() + "'");
            }
            else if (pars[i].DbType == System.Data.DbType.Boolean)
            {
                sql = sql.Replace(pars[i].ParameterName, Convert.ToBoolean(pars[i].Value) ? "1" : "0");
            }
            else
            {
                sql = sql.Replace(pars[i].ParameterName, pars[i].Value?.ToString());
            }
        }

        return sql;
    }

    /// <summary>
    /// 格式化参数拼接成完整的SQL语句
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="pars"></param>
    /// <returns></returns>
    public static string ParameterFormat(string sql, object pars)
    {
        var param = (SugarParameter[])pars;
        return ParameterFormat(sql, param);
    }
}