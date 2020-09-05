// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur 
// 开源协议：Apache-2.0（https://gitee.com/monksoul/Fur/blob/alpha/LICENSE）

using Fur.ConfigurableOptions;
using Microsoft.Extensions.Configuration;

namespace Fur.Authorization
{
    /// <summary>
    /// Jwt
    /// </summary>
    [OptionsSettings("AppSettings:JWTSettings")]
    public sealed class JWTSettingsOptions : IConfigurableOptions<JWTSettingsOptions>
    {
        /// <summary>
        /// 选项后期配置
        /// </summary>
        /// <param name="options"></param>
        public void PostConfigure(JWTSettingsOptions options, IConfiguration configuration)
        {
        }
    }
}