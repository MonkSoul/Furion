namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public class PageModel : BaseParams
    {
        /// <summary>
        /// 
        /// </summary>
        public PageModel() { }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }
    }

    /// <summary>
    /// 分页基础参数，如需新增请继承此类
    /// </summary>
    public class BaseParams
    {
        private const int _maxPageSize = 200;
        private int _pageSize = 15;

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
        }

        /// <summary>
        /// 检索条件
        /// </summary>
        public string SearchText { get; set; }
    }
}
