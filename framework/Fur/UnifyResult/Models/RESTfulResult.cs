using Fur.DependencyInjection;

namespace Fur.UnifyResult
{
    /// <summary>
    /// RESTful 风格结果集
    /// </summary>
    [SkipScan]
    public class RESTfulResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int? StatusCode { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 执行成功
        /// </summary>
        public bool Successed { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public object Errors { get; set; }
    }
}