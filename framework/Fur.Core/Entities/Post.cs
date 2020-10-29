using Fur.DatabaseAccessor;
using System;
using System.Collections.Generic;

namespace Fur.Core
{
    public class Post : Entity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Post()
        {
            CreatedTime = DateTimeOffset.UtcNow;
            IsDeleted = false;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Person 集合
        /// </summary>
        public ICollection<Person> Persons { get; set; }
    }
}