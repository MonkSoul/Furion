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
        public int StatusCodes { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public int Content { get; set; }

        /// <summary>
        /// 执行成功
        /// </summary>
        public bool Successed { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public object Errors { get; set; }

        /// <summary>
        /// 批准
        /// </summary>
        public bool Authorized { get; set; }
    }
}