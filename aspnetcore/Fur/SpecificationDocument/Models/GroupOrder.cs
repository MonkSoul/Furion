namespace Fur.SpecificationDocument
{
    /// <summary>
    /// 分组排序
    /// </summary>
    internal sealed class GroupOrder
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GroupOrder()
        {
            Order = 0;
        }

        /// <summary>
        /// 分组名
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 分组排序
        /// </summary>
        public int Order { get; set; }
    }
}