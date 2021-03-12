namespace Furion.Snowflake
{
    /// <summary>
    /// 雪花 ID 生成器静态类
    /// </summary>
    public class IDGenerator
    {
        /// <summary>
        /// 私有化实例
        /// </summary>
        private static IIDGenerator instance = null;

        /// <summary>
        /// 获取单例实例
        /// </summary>
        public static IIDGenerator Instance => instance;

        /// <summary>
        /// 设置参数，建议程序初始化时执行一次
        /// </summary>
        /// <param name="options"></param>
        public static void SetIdGenerator(IDGeneratorOptions options)
        {
            instance = new DefaultIDGenerator(options);
        }

        /// <summary>
        /// 生成新的Id
        /// 调用本方法前，请确保调用了 SetIdGenerator 方法做初始化。
        /// 否则将会初始化一个WorkerId为1的对象。
        /// </summary>
        /// <returns></returns>
        public static long NextId()
        {
            if (instance == null)
            {
                instance = new DefaultIDGenerator(new IDGeneratorOptions() { WorkerId = 1 });
            }

            return instance.NewLong();
        }
    }
}