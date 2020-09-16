using Fur.DatabaseAccessor;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fur.Core
{
    public class Person : Entity    // 继承自 Entity 抽象类
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Person()
        {
            CreatedTime = DateTime.Now;
            IsDeleted = false;
        }

        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(32)]
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