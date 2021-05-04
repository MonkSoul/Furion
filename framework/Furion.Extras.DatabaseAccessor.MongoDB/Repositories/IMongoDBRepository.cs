namespace MongoDB.Driver
{
    /// <summary>
    /// MongoDB 仓储
    /// </summary>
    public interface IMongoDBRepository
    {
        /// <summary>
        /// 连接上下文
        /// </summary>
        MongoClient Context { get; }

        /// <summary>
        /// 动态连接上下文
        /// </summary>
        dynamic DynamicContext { get; }

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="name"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        IMongoDatabase GetDatabase(string name, MongoDatabaseSettings settings = null);
    }
}