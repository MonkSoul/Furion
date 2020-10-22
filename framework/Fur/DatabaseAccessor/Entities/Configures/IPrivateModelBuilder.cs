namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库模型构建器依赖（禁止直接继承）
    /// </summary>
    /// <remarks>
    /// 对应 <see cref="Microsoft.EntityFrameworkCore.DbContext.OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder)"/>
    /// </remarks>
    public interface IPrivateModelBuilder
    {
    }
}