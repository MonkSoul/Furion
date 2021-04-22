namespace Furion.DistributedIDGenerator
{
    /// <summary>
    /// 分布式 ID 生成器
    /// </summary>
    public interface IDistributedIDGenerator
    {
        /// <summary>
        /// 生成逻辑
        /// </summary>
        /// <param name="idGeneratorOptions"></param>
        /// <returns></returns>
        object Create(object idGeneratorOptions = default);
    }
}