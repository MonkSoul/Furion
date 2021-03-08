using Furion.DependencyInjection;

namespace Furion.DataValidation
{
    /// <summary>
    /// 验证失败模型
    /// </summary>
    [SkipScan]
    public sealed class ValidateFailedModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="field"></param>
        /// <param name="messages"></param>
        public ValidateFailedModel(string field, string[] messages)
        {
            Field = field;
            Messages = messages;
        }

        /// <summary>
        /// 出错字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 错误列表
        /// </summary>
        public string[] Messages { get; set; }
    }
}