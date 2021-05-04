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