using Fur.DatabaseAccessor;

namespace Fur.Core
{
    public class V_Person : EntityNotKey
    {
        /// <summary>
        /// 配置视图名
        /// </summary>
        public V_Person() : base("V_Person")
        {
        }

        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }
    }
}