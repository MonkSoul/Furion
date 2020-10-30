using Fur.DependencyInjection;

namespace Fur
{
    /// <summary>
    /// 官方包定义
    /// </summary>
    [SkipScan]
    internal static class AppExtra
    {
        /// <summary>
        /// Jwt 验证包
        /// </summary>
        internal const string AUTHENTICATION_JWTBEARER = "Fur.Extras.Authentication.JwtBearer";
    }
}