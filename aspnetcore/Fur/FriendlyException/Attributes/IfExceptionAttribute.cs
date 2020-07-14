using System;

namespace Fur.FriendlyException.Attributes
{
    /// <summary>
    /// 异常特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class IfExceptionAttribute : Attribute
    {
        #region 构造函数 + public IfExceptionAttribute(string exceptionCode, string exceptionMessage)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exceptionCode">异常编码</param>
        /// <param name="exceptionMessage">异常信息</param>
        public IfExceptionAttribute(string exceptionCode, string exceptionMessage)
        {
            this.ExceptionCode = exceptionCode;
            this.ExceptionMessage = exceptionMessage;
        }
        #endregion

        #region 构造函数 + public IfExceptionAttribute(int exceptionCode, string exceptionMessage)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exceptionCode">异常编码</param>
        /// <param name="exceptionMessage">异常信息</param>
        public IfExceptionAttribute(int exceptionCode, string exceptionMessage)
        {
            this.ExceptionCode = exceptionCode;
            this.ExceptionMessage = exceptionMessage;
        }
        #endregion

        /// <summary>
        /// 异常类型
        /// </summary>
        public Type Exception { get; set; } = typeof(Exception);

        /// <summary>
        /// 异常编码
        /// </summary>
        public object ExceptionCode { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string ExceptionMessage { get; set; }
    }
}
