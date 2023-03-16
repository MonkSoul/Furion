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