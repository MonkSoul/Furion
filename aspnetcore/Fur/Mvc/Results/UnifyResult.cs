namespace Fur.Mvc.Results
{
    /// <summary>
    /// 统一返回值
    /// </summary>
    public sealed class UnifyResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int? StatusCode { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public object Results { get; set; }

        /// <summary>
        /// 执行成功
        /// </summary>
        public bool Successed { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public object Errors { get; set; }

        /// <summary>
        /// 是否未授权请求
        /// </summary>
        public bool UnAuthorizedRequest { get; set; }
    }
}