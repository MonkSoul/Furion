using Fur.Options;

namespace Fur.Authorization
{
    /// <summary>
    /// Jwt
    /// </summary>
    [OptionsSettings("AppSettings:JWTSettings")]
    public sealed class JWTSettingsOptions : IAppOptions<JWTSettingsOptions>
    {
        /// <summary>
        /// 选项后期配置
        /// </summary>
        /// <param name="options"></param>
        public void PostConfigure(JWTSettingsOptions options)
        {
        }
    }
}