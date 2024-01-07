// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB.Driver;

/// <summary>
/// 带实体主键的父 MongoDbEntity
/// </summary>
public interface IMongoDBEntity<TKey>
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [BsonId]
    TKey Id { get; set; }
}