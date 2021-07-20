// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

namespace MongoDB.Driver
{
    /// <summary>
    /// MongoDB 仓储
    /// </summary>
    public class MongoDBRepository : IMongoDBRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mongoClient"></param>
        public MongoDBRepository(IMongoClient mongoClient)
        {
            DynamicContext = Context = (MongoClient)mongoClient;
        }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public virtual MongoClient Context { get; }

        /// <summary>
        /// 动态数据库上下文
        /// </summary>
        public virtual dynamic DynamicContext { get; }

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="name"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public virtual IMongoDatabase GetDatabase(string name, MongoDatabaseSettings settings = null)
        {
            return Context.GetDatabase(name, settings);
        }
    }
}