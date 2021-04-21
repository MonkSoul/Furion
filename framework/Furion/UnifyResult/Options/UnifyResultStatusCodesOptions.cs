using Furion.DependencyInjection;

namespace Furion.UnifyResult
{
    /// <summary>
    /// 规范化状态码选项
    /// </summary>
    [SkipScan]
    public sealed class UnifyResultStatusCodesOptions
    {
        /// <summary>
        /// 设置返回 200 状态码列表
        /// <para>默认：401，403，如果设置为 null，则标识所有状态码都返回 200 </para>
        /// </summary>
        public int[] Return200StatusCodes { get; set; } = new[] { 401, 403 };
    }
}