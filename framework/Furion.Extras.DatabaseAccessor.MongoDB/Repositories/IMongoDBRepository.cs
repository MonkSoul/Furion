// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using MongoDB.Bson;
using System.Linq.Expressions;

namespace MongoDB.Driver;

/// <summary>
/// MongoDB 仓储
/// </summary>
public partial interface IMongoDBRepository
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

    /// <summary>
    /// 切换仓储
    /// </summary>
    /// <typeparam name="TDocument">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <returns>仓储</returns>
    IMongoDBRepository<TDocument, TKey> Change<TDocument, TKey>()
        where TDocument : class, IMongoDBEntity<TKey>, new();
}

/// <summary>
/// MongoDB 泛型仓储
/// </summary>
/// <typeparam name="TDocument"></typeparam>
/// <typeparam name="TKey"></typeparam>
public partial interface IMongoDBRepository<TDocument, TKey>
    where TDocument : class, IMongoDBEntity<TKey>, new()
{
    /// <summary>
    /// 文档集合
    /// </summary>
    public IMongoCollection<TDocument> Entities { get; }

    /// <summary>
    /// 连接上下文
    /// </summary>
    MongoClient Context { get; }

    /// <summary>
    /// 动态连接上下文
    /// </summary>
    dynamic DynamicContext { get; }

    /// <summary>
    /// 判断是否存在
    /// </summary>
    /// <param name="predicate">条件</param>
    /// <returns></returns>
    bool Exists(Expression<Func<TDocument, bool>> predicate);

    /// <summary>
    /// 判断是否存在
    /// </summary>
    /// <param name="predicate">条件</param>
    /// <returns></returns>
    Task<bool> ExistsAsync(Expression<Func<TDocument, bool>> predicate);

    /// <summary>
    /// 获取记录数
    /// </summary>
    /// <returns></returns>
    long Count();

    /// <summary>
    /// 异步获取记录数
    /// </summary>
    /// <returns></returns>
    Task<long> CountAsync();

    /// <summary>
    /// 获取记录数
    /// </summary>
    /// <returns></returns>
    long Count(Expression<Func<TDocument, bool>> expression);

    /// <summary>
    /// 异步获取记录数
    /// </summary>
    /// <returns></returns>
    Task<long> CountAsync(Expression<Func<TDocument, bool>> expression);

    /// <summary>
    /// 获取记录数
    /// </summary>
    /// <returns></returns>
    long Count(FilterDefinition<TDocument> filter);

    /// <summary>
    /// 异步获取记录数
    /// </summary>
    /// <returns></returns>
    Task<long> CountAsync(FilterDefinition<TDocument> filter);

    /// <summary>
    /// 获取单个对象
    /// </summary>
    /// <param name="id">objectId</param>
    /// <returns></returns>
    TDocument Get(TKey id);

    /// <summary>
    /// 获取单个对象
    /// </summary>
    /// <param name="expression">筛选条件</param>
    /// <returns></returns>
    TDocument Get(Expression<Func<TDocument, bool>> expression);

    /// <summary>
    /// 获取单个对象
    /// </summary>
    /// <param name="filter">过滤器</param>
    /// <returns></returns>
    TDocument Get(FilterDefinition<TDocument> filter);

    /// <summary>
    /// 异步获取单个对象
    /// </summary>
    /// <param name="id">objectId</param>
    /// <returns></returns>
    Task<TDocument> GetAsync(TKey id);

    /// <summary>
    /// 异步获取单个对象
    /// </summary>
    /// <param name="expression">筛选条件</param>
    /// <returns></returns>
    Task<TDocument> GetAsync(Expression<Func<TDocument, bool>> expression);

    /// <summary>
    /// 异步获取单个对象
    /// </summary>
    /// <param name="filter">过滤器</param>
    /// <returns></returns>
    Task<TDocument> GetAsync(FilterDefinition<TDocument> filter);

    /// <summary>
    /// 插入
    /// </summary>
    /// <param name="value">对象</param>
    void Insert(TDocument value);

    /// <summary>
    /// 异步插入
    /// </summary>
    /// <param name="value">对象</param>
    /// <returns></returns>
    Task InsertAsync(TDocument value);

    /// <summary>
    /// 批量插入
    /// </summary>
    /// <param name="values">对象集合</param>
    void BatchInsert(IEnumerable<TDocument> values);

    /// <summary>
    /// 异步批量插入
    /// </summary>
    /// <param name="values">对象集合</param>
    /// <returns></returns>
    Task BatchInsertAsync(IEnumerable<TDocument> values);

    /// <summary>
    /// 局部更新
    /// </summary>
    /// <param name="id">记录ID</param>
    /// <param name="update">更新条件</param>
    /// <returns></returns>
    UpdateResult Update(TKey id, UpdateDefinition<TDocument> update);

    /// <summary>
    /// 局部更新
    /// </summary>
    /// <param name="id">记录ID</param>
    /// <param name="update">更新条件</param>
    /// <returns></returns>
    Task<UpdateResult> UpdateAsync(TKey id, UpdateDefinition<TDocument> update);

    /// <summary>
    /// 局部更新
    /// </summary>
    /// <param name="filter">过滤器</param>
    /// <param name="update">更新条件</param>
    /// <returns></returns>
    UpdateResult Update(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update);

    /// <summary>
    /// 异步局部更新（仅更新一条记录）
    /// </summary>
    /// <param name="filter">过滤器</param>
    /// <param name="update">更新条件</param>
    /// <returns></returns>
    Task<UpdateResult> UpdateAsync(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update);

    /// <summary>
    /// 局部更新（仅更新一条记录）
    /// </summary>
    /// <param name="expression">筛选条件</param>
    /// <param name="update">更新条件</param>
    /// <returns></returns>
    UpdateResult Update(Expression<Func<TDocument, bool>> expression,
        UpdateDefinition<TDocument> update);

    /// <summary>
    /// 异步局部更新（仅更新一条记录）
    /// </summary>
    /// <param name="expression">筛选条件</param>
    /// <param name="update">更新条件</param>
    /// <returns></returns>
    Task<UpdateResult> UpdateAsync<T>(Expression<Func<TDocument, bool>> expression,
        UpdateDefinition<TDocument> update);

    /// <summary>
    /// 局部更新（更新多条记录）
    /// </summary>
    /// <param name="expression">筛选条件</param>
    /// <param name="update">更新条件</param>
    /// <returns></returns>
    UpdateResult UpdateMany(Expression<Func<TDocument, bool>> expression,
        UpdateDefinition<TDocument> update);

    /// <summary>
    /// 异步局部更新（更新多条记录）
    /// </summary>
    /// <param name="expression">筛选条件</param>
    /// <param name="update">更新条件</param>
    /// <returns></returns>
    Task<UpdateResult> UpdateManyAsync(Expression<Func<TDocument, bool>> expression,
        UpdateDefinition<TDocument> update);

    /// <summary>
    /// 局部更新（仅更新一条记录）
    /// <para><![CDATA[expression 参数示例：x => x.Id == 1 && x.Age > 18 && x.Gender == 0]]></para>
    /// <para><![CDATA[entity 参数示例：y => new T{ RealName = "Ray", Gender = 1}]]></para>
    /// </summary>
    /// <param name="expression">筛选条件</param>
    /// <param name="entity">更新条件</param>
    /// <returns></returns>
    UpdateResult Update(Expression<Func<TDocument, bool>> expression,
        Expression<Action<TDocument>> entity);

    /// <summary>
    /// 异步局部更新（仅更新一条记录）
    /// <para><![CDATA[expression 参数示例：x => x.Id == 1 && x.Age > 18 && x.Gender == 0]]></para>
    /// <para><![CDATA[entity 参数示例：y => new T{ RealName = "Ray", Gender = 1}]]></para>
    /// </summary>
    /// <param name="expression">筛选条件</param>
    /// <param name="entity">更新条件</param>
    /// <returns></returns>
    Task<UpdateResult> UpdateAsync(Expression<Func<TDocument, bool>> expression,
        Expression<Action<TDocument>> entity);

    /// <summary>
    /// 覆盖更新
    /// </summary>
    /// <param name="value">对象</param>
    /// <returns></returns>
    ReplaceOneResult Update(TDocument value);

    /// <summary>
    /// 异步覆盖更新
    /// </summary>
    /// <param name="value">对象</param>
    /// <returns></returns>
    Task<ReplaceOneResult> UpdateAsync(TDocument value);

    /// <summary>
    /// 删除指定对象
    /// </summary>
    /// <param name="id">对象Id</param>
    /// <returns></returns>
    DeleteResult Delete(TKey id);

    /// <summary>
    /// 异步删除指定对象
    /// </summary>
    /// <param name="id">对象Id</param>
    /// <returns></returns>
    Task<DeleteResult> DeleteAsync(TKey id);

    /// <summary>
    /// 删除指定对象
    /// </summary>
    /// <param name="expression">查询条件</param>
    /// <returns></returns>
    DeleteResult Delete(Expression<Func<TDocument, bool>> expression);

    /// <summary>
    /// 异步删除指定对象
    /// </summary>
    /// <param name="expression">查询条件</param>
    /// <returns></returns>
    Task<DeleteResult> DeleteAsync(Expression<Func<TDocument, bool>> expression);

    /// <summary>
    /// 批量删除对象
    /// </summary>
    /// <param name="ids">ID集合</param>
    /// <returns></returns>
    DeleteResult BatchDelete(IEnumerable<ObjectId> ids);

    /// <summary>
    /// 异步批量删除对象
    /// </summary>
    /// <param name="ids">ID集合</param>
    /// <returns></returns>
    Task<DeleteResult> BatchDeleteAsync(IEnumerable<ObjectId> ids);

    /// <summary>
    /// 批量删除对象
    /// </summary>
    /// <param name="filter">过滤器</param>
    /// <returns></returns>
    DeleteResult BatchDelete(FilterDefinition<TDocument> filter);

    /// <summary>
    /// 异步批量删除对象
    /// </summary>
    /// <param name="filter">过滤器</param>
    /// <returns></returns>
    Task<DeleteResult> BatchDeleteAsync(FilterDefinition<TDocument> filter);

    /// <summary>
    /// 批量删除对象
    /// </summary>
    /// <param name="expression">筛选条件</param>
    /// <returns></returns>
    DeleteResult BatchDelete(Expression<Func<TDocument, bool>> expression);

    /// <summary>
    /// 异步批量删除对象
    /// </summary>
    /// <param name="expression">筛选条件</param>
    /// <returns></returns>
    Task<DeleteResult> BatchDeleteAsync(Expression<Func<TDocument, bool>> expression);

    /// <summary>
    /// 切换仓储
    /// </summary>
    /// <typeparam name="TChangeEntity">实体类型</typeparam>
    /// <typeparam name="TChangeKey">主键类型</typeparam>
    /// <returns>仓储</returns>
    IMongoDBRepository<TChangeEntity, TChangeKey> Change<TChangeEntity, TChangeKey>()
        where TChangeEntity : class, IMongoDBEntity<TChangeKey>, new()
        where TChangeKey : class;

    /// <summary>
    /// 根据表达式查询多条记录
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns></returns>
    IQueryable<TDocument> Where(Expression<Func<TDocument, bool>> predicate);

    /// <summary>
    /// 构建查询分析器
    /// </summary>
    /// <returns></returns>
    IQueryable<TDocument> AsQueryable();

    /// <summary>
    /// 构建查询分析器
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns></returns>
    IQueryable<TDocument> AsQueryable(Expression<Func<TDocument, bool>> predicate);

    /// <summary>
    /// 直接返回数据库结果
    /// </summary>
    /// <returns></returns>
    List<TDocument> AsEnumerable();

    /// <summary>
    /// 直接返回数据库结果
    /// </summary>
    /// <returns></returns>
    Task<List<TDocument>> AsAsyncEnumerable();

    /// <summary>
    /// 直接返回数据库结果
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns></returns>

    List<TDocument> AsEnumerable(Expression<Func<TDocument, bool>> predicate);

    /// <summary>
    /// 直接返回数据库结果
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns></returns>

    Task<List<TDocument>> AsAsyncEnumerable(Expression<Func<TDocument, bool>> predicate);
}

/// <summary>
/// MongoDB 泛型仓储
/// </summary>
/// <typeparam name="TDocument"></typeparam>
public partial interface IMongoDBRepository<TDocument> : IMongoDBRepository<TDocument, ObjectId>
    where TDocument : class, IMongoDBEntity<ObjectId>, new()
{
}