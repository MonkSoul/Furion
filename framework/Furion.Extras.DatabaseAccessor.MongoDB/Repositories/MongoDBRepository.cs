// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.7.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

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