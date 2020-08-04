using System;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 抛异常设置类
    /// </summary>

    public static class Oops
    {
        /// <summary>
        /// 有bug
        /// </summary>
        /// <param name="exceptionCode">异常编码</param>
        /// <param name="exception">异常类型</param>
        /// <returns></returns>
        public static Exception Set(int exceptionCode, Type exception = null, int statusCode = 500)
           => new Exception($"##{exceptionCode};{((exception ?? typeof(Exception)).FullName)};{statusCode}##");
    }
}