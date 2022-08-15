// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Dapper;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Dapper 拓展类
/// </summary>
public static class DapperServiceCollectionExtensions
{
    /// <summary>
    /// 添加 Dapper 拓展
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="sqlProvider"> <see cref="Dapper.SqlProvider"/> 类型</param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddDapper(this IServiceCollection services, string connectionString, string sqlProvider, Action configure = default)
    {
        // 获取数据库类型
        var dbConnectionType = SqlProvider.GetDbConnectionType(sqlProvider);

        // 创建数据库连接对象
        services.AddScoped(u =>
        {
            var dbConnection = Activator.CreateInstance(dbConnectionType, new[] { connectionString }) as IDbConnection;
            if (dbConnection.State != ConnectionState.Open) dbConnection.Open();

            return dbConnection;
        });

        // 注册非泛型仓储
        services.AddScoped<IDapperRepository, DapperRepository>();

        // 注册 Dapper 仓储
        services.AddScoped(typeof(IDapperRepository<>), typeof(DapperRepository<>));

        // 添加 Dapper 其他初始配置，关联 https://gitee.com/dotnetchina/Furion/issues/I5AYFX
        configure?.Invoke();

        return services;
    }
}