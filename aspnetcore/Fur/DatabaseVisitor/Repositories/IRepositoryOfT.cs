using Fur.DatabaseVisitor.Dependencies;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IEntity, new()
    {
        public DbContext DbContext { get; }
        public DbSet<TEntity> Entity { get; }
        public DatabaseFacade Database { get; }
        public DbConnection DbConnection { get; }

        // 新增操作
        EntityEntry<TEntity> Insert(TEntity entity);
        void Insert(params TEntity[] entities);
        void Insert(IEnumerable<TEntity> entities);
        ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity);
        Task InsertAsync(params TEntity[] entities);
        Task InsertAsync(IEnumerable<TEntity> entities);
        EntityEntry<TEntity> InsertSaveChanges(TEntity entity);
        void InsertSaveChanges(params TEntity[] entities);
        void InsertSaveChanges(IEnumerable<TEntity> entities);
        ValueTask<EntityEntry<TEntity>> InsertSaveChangesAsync(TEntity entity);
        Task InsertSaveChangesAsync(params TEntity[] entities);
        Task InsertSaveChangesAsync(IEnumerable<TEntity> entities);

        // 更新操作
        EntityEntry<TEntity> Update(TEntity entity);
        void Update(params TEntity[] entities);
        void Update(IEnumerable<TEntity> entities);
        Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity);
        Task UpdateAsync(params TEntity[] entities);
        Task UpdateAsync(IEnumerable<TEntity> entities);
        EntityEntry<TEntity> UpdateSaveChanges(TEntity entity);
        void UpdateSaveChanges(params TEntity[] entities);
        void UpdateSaveChanges(IEnumerable<TEntity> entities);
        Task<EntityEntry<TEntity>> UpdateSaveChangesAsync(TEntity entity);
        Task UpdateSaveChangesAsync(params TEntity[] entities);
        Task UpdateSaveChangesAsync(IEnumerable<TEntity> entities);

        // 删除操作
        EntityEntry<TEntity> Delete(TEntity entity);
        void Delete(params TEntity[] entities);
        void Delete(IEnumerable<TEntity> entities);
        Task<EntityEntry<TEntity>> DeleteAsync(TEntity entity);
        Task DeleteAsync(params TEntity[] entities);
        Task DeleteAsync(IEnumerable<TEntity> entities);
        EntityEntry<TEntity> DeleteSaveChanges(TEntity entity);
        void DeleteSaveChanges(params TEntity[] entities);
        void DeleteSaveChanges(IEnumerable<TEntity> entities);
        Task<EntityEntry<TEntity>> DeleteSaveChangesAsync(TEntity entity);
        Task DeleteSaveChangesAsync(params TEntity[] entities);
        Task DeleteSaveChangesAsync(IEnumerable<TEntity> entities);
        EntityEntry<TEntity> Delete(object id);
        Task<EntityEntry<TEntity>> DeleteAsync(object id);
        EntityEntry<TEntity> DeleteSaveChanges(object id);
        Task<EntityEntry<TEntity>> DeleteSaveChangesAsync(object id);

        // 查询一条
        TEntity Find(object id);
        ValueTask<TEntity> FindAsync(object id);
        TEntity Single();
        TEntity Single(bool noTracking);
        Task<TEntity> SingleAsync();
        Task<TEntity> SingleAsync(bool noTracking);
        TEntity Single(Expression<Func<TEntity, bool>> expression);
        TEntity Single(Expression<Func<TEntity, bool>> expression, bool noTracking);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression, bool noTracking);
        TEntity SingleOrDefault();
        TEntity SingleOrDefault(bool noTracking);
        Task<TEntity> SingleOrDefaultAsync();
        Task<TEntity> SingleOrDefaultAsync(bool noTracking);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking);

        TEntity First();
        TEntity First(bool noTracking);
        Task<TEntity> FirstAsync();
        Task<TEntity> FirstAsync(bool noTracking);
        TEntity First(Expression<Func<TEntity, bool>> expression);
        TEntity First(Expression<Func<TEntity, bool>> expression, bool noTracking);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, bool noTracking);
        TEntity FirstOrDefault();
        TEntity FirstOrDefault(bool noTracking);
        Task<TEntity> FirstOrDefaultAsync();
        Task<TEntity> FirstOrDefaultAsync(bool noTracking);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking);

        // 查询多条
        IQueryable<TEntity> Get(bool noTracking = false, bool ignoreQueryFilters = false);
        Task<List<TEntity>> GetAsync(bool noTracking = false, bool ignoreQueryFilters = false);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = null, bool noTracking = false, bool ignoreQueryFilters = false);
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression = null, bool noTracking = false, bool ignoreQueryFilters = false);

        // 保存操作
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess);

        // 原始Sql查询
        IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters);
        IQueryable<TEntity> FromSqlRaw<TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;

        DataTable FromSqlQuery(string sql, params object[] parameters);
        Task<DataTable> FromSqlQueryAsync(string sql, params object[] parameters);
        DataTable FromSqlQuery<TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        Task<DataTable> FromSqlQueryAsync<TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        IEnumerable<T> FromSqlQuery<T>(string sql, params object[] parameters);
        Task<IEnumerable<T>> FromSqlQueryAsync<T>(string sql, params object[] parameters);
        IEnumerable<T> FromSqlQuery<T, TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        Task<IEnumerable<T>> FromSqlQueryAsync<T, TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;

        DataSet FromSqlDataSetQuery(string sql, params object[] parameters);
        Task<DataSet> FromSqlDataSetQueryAsync(string sql, params object[] parameters);
        DataSet FromSqlDataSetQuery<TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        Task<DataSet> FromSqlDataSetQueryAsync<TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;

        IEnumerable<T1> FromSqlDataSetQuery<T1>(string sql, params object[] parameters);
        (IEnumerable<T1> data1, IEnumerable<T2> data2) FromSqlDataSetQuery<T1, T2>(string sql, params object[] parameters);
        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) FromSqlDataSetQuery<T1, T2, T3>(string sql, params object[] parameters);
        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) FromSqlDataSetQuery<T1, T2, T3, T4>(string sql, params object[] parameters);
        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) FromSqlDataSetQuery<T1, T2, T3, T4, T5>(string sql, params object[] parameters);
        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6>(string sql, params object[] parameters);
        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string sql, params object[] parameters);
        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, params object[] parameters);
        object FromSqlDataSetQuery(string sql, object[] types, params object[] parameters);

        Task<IEnumerable<T1>> FromSqlDataSetQueryAsync<T1>(string sql, params object[] parameters);
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> FromSqlDataSetQueryAsync<T1, T2>(string sql, params object[] parameters);
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> FromSqlDataSetQueryAsync<T1, T2, T3>(string sql, params object[] parameters);
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> FromSqlDataSetQueryAsync<T1, T2, T3, T4>(string sql, params object[] parameters);
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5>(string sql, params object[] parameters);
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string sql, params object[] parameters);
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, params object[] parameters);
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, params object[] parameters);
        Task<object> FromSqlDataSetQueryAsync(string sql, object[] types, params object[] parameters);


        IEnumerable<T1> FromSqlDataSetQuery<T1, TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        (IEnumerable<T1> data1, IEnumerable<T2> data2) FromSqlDataSetQuery<T1, T2, TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) FromSqlDataSetQuery<T1, T2, T3, TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) FromSqlDataSetQuery<T1, T2, T3, T4, TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) FromSqlDataSetQuery<T1, T2, T3, T4, T5, TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8, TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        object FromSqlDataSetQuery<TParameterModel>(string sql, object[] types, TParameterModel parameterModel) where TParameterModel : class;

        Task<IEnumerable<T1>> FromSqlDataSetQueryAsync<T1, TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> FromSqlDataSetQueryAsync<T1, T2, TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> FromSqlDataSetQueryAsync<T1, T2, T3, TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8, TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class;
        Task<object> FromSqlDataSetQueryAsync<TParameterModel>(string sql, object[] types, TParameterModel parameterModel) where TParameterModel : class;

        // 存储过程
        DataTable FromSqlProcedureQuery(string name, params object[] parameters);
        Task<DataTable> FromSqlProcedureQueryAsync(string name, params object[] parameters);
        DataTable FromSqlProcedureQuery<TParameterModel>(string name, TParameterModel parameterModel) where TParameterModel : class;
        Task<DataTable> FromSqlProcedureQueryAsync<TParameterModel>(string name, TParameterModel parameterModel) where TParameterModel : class;
        IEnumerable<T> FromSqlProcedureQuery<T>(string name, params object[] parameters);
        Task<IEnumerable<T>> FromSqlProcedureQueryAsync<T>(string name, params object[] parameters);
        IEnumerable<T> FromSqlProcedureQuery<T, TParameterModel>(string name, TParameterModel parameterModel) where TParameterModel : class;
        Task<IEnumerable<T>> FromSqlProcedureQueryAsync<T, TParameterModel>(string name, TParameterModel parameterModel) where TParameterModel : class;
    }
}
