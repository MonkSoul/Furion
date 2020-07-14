using System;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 抛异常设置类
    /// </summary>
    public static class Oops
    {
        #region 有bug + public static Exception Bug(string exceptionCode)
        /// <summary>
        /// 有bug
        /// </summary>
        /// <param name="exceptionCode">异常编码</param>
        /// <returns><see cref="Exception"/></returns>
        public static Exception Bug(string exceptionCode)
            => new Exception($"##{exceptionCode}##");
        #endregion

        #region 有bug + public static Exception Bug(int exceptionCode)
        /// <summary>
        /// 有bug
        /// </summary>
        /// <param name="exceptionCode">异常编码</param>
        /// <returns></returns>
        public static Exception Bug(int exceptionCode)
           => new Exception($"##{exceptionCode}##");
        #endregion
    }
}
