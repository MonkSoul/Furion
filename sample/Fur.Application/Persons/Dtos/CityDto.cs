using System.Collections.Generic;

namespace Fur.Application.Persons
{
    public class CityDto
    {
        /// <summary>
        /// 逐渐
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public ICollection<CityDto> Childrens { get; set; }
    }
}