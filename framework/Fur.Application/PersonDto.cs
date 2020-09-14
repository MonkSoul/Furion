using System.ComponentModel.DataAnnotations;

namespace Fur.Application
{
    public class PersonDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32, MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
    }
}
