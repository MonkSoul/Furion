using Fur.DatabaseVisitor.Dependencies;
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
        DbContext DbContext { get; }
        DbSet<TEntity> Entity { get; }
        DatabaseFacade Database { get; }
        DbConnection DbConnection { get; }
        EntityEntry<TEntity> EntityEntry(TEntity entity);

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

        // 更新特定列
        EntityEntry<TEntity> UpdateIncludeProperties(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);
        Task<EntityEntry<TEntity>> UpdateIncludePropertiesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);
        EntityEntry<TEntity> UpdateIncludePropertiesSaveChanges(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);
        Task<EntityEntry<TEntity>> UpdateIncludePropertiesSaveChangesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        // 排除特定列
        EntityEntry<TEntity> UpdateExcludeProperties(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);
        Task<EntityEntry<TEntity>> UpdateExcludePropertiesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);
        EntityEntry<TEntity> UpdateExcludePropertiesSaveChanges(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);
        Task<EntityEntry<TEntity>> UpdateExcludePropertiesSaveChangesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

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
        IQueryable<TEntity> FromSqlRaw(string sql, object parameterModel);

        DataTable FromSqlQuery(string sql, params object[] parameters);
        Task<DataTable> FromSqlQueryAsync(string sql, params object[] parameters);
        DataTable FromSqlQuery(string sql, object parameterModel);
        Task<DataTable> FromSqlQueryAsync(string sql, object parameterModel);
        IEnumerable<T> FromSqlQuery<T>(string sql, params object[] parameters);
        Task<IEnumerable<T>> FromSqlQueryAsync<T>(string sql, params object[] parameters);
        IEnumerable<T> FromSqlQuery<T>(string sql, object parameterModel);
        Task<IEnumerable<T>> FromSqlQueryAsync<T>(string sql, object parameterModel);

        DataSet FromSqlDataSetQuery(string sql, params object[] parameters);
        Task<DataSet> FromSqlDataSetQueryAsync(string sql, params object[] parameters);
        DataSet FromSqlDataSetQuery(string sql, object parameterModel);
        Task<DataSet> FromSqlDataSetQueryAsync(string sql, object parameterModel);

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


        IEnumerable<T1> FromSqlDataSetQuery<T1>(string sql, object parameterModel);
        (IEnumerable<T1> data1, IEnumerable<T2> data2) FromSqlDataSetQuery<T1, T2>(string sql, object parameterModel);
        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) FromSqlDataSetQuery<T1, T2, T3>(string sql, object parameterModel);
        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) FromSqlDataSetQuery<T1, T2, T3, T4>(string sql, object parameterModel);
        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) FromSqlDataSetQuery<T1, T2, T3, T4, T5>(string sql, object parameterModel);
        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6>(string sql, object parameterModel);
        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string sql, object parameterModel);
        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, object parameterModel);
        object FromSqlDataSetQuery(string sql, object[] types, object parameterModel);

        Task<IEnumerable<T1>> FromSqlDataSetQueryAsync<T1>(string sql, object parameterModel);
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> FromSqlDataSetQueryAsync<T1, T2>(string sql, object parameterModel);
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> FromSqlDataSetQueryAsync<T1, T2, T3>(string sql, object parameterModel);
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> FromSqlDataSetQueryAsync<T1, T2, T3, T4>(string sql, object parameterModel);
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5>(string sql, object parameterModel);
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string sql, object parameterModel);
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, object parameterModel);
        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, object parameterModel);
        Task<object> FromSqlDataSetQueryAsync(string sql, object[] types, object parameterModel);

        // 存储过程
        DataTable FromSqlProcedureQuery(string name, params object[] parameters);
        Task<DataTable> FromSqlProcedureQueryAsync(string name, params object[] parameters);
        DataTable FromSqlProcedureQuery(string name, object parameterModel);
        Task<DataTable> FromSqlProcedureQueryAsync(string name, object parameterModel);
        IEnumerable<T> FromSqlProcedureQuery<T>(string name, params object[] parameters);
        Task<IEnumerable<T>> FromSqlProcedureQueryAsync<T>(string name, params object[] parameters);
        IEnumerable<T> FromSqlProcedureQuery<T>(string name, object parameterModel);
        Task<IEnumerable<T>> FromSqlProcedureQueryAsync<T>(string name, object parameterModel);

        DataSet FromSqlProcedureDataSetQuery(string name, params object[] parameters);
        Task<DataSet> FromSqlProcedureDataSetQueryAsync(string name, params object[] parameters);
        DataSet FromSqlProcedureDataSetQuery(string name, object parameterModel);
        Task<DataSet> FromSqlProcedureDataSetQueryAsync(string name, object parameterModel);


        IEnumerable<T1> FromSqlProcedureDataSetQuery<T1>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2) FromSqlProcedureDataSetQuery<T1, T2>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) FromSqlProcedureDataSetQuery<T1, T2, T3>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) FromSqlProcedureDataSetQuery<T1, T2, T3, T4>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string name, params object[] parameters);

        object FromSqlProcedureDataSetQuery(string name, object[] types, params object[] parameters);

        Task<IEnumerable<T1>> FromSqlProcedureDataSetQueryAsync<T1>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> FromSqlProcedureDataSetQueryAsync<T1, T2>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string name, params object[] parameters);

        Task<object> FromSqlProcedureDataSetQueryAsync(string name, object[] types, params object[] parameters);

        IEnumerable<T1> FromSqlProcedureDataSetQuery<T1>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2) FromSqlProcedureDataSetQuery<T1, T2>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) FromSqlProcedureDataSetQuery<T1, T2, T3>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) FromSqlProcedureDataSetQuery<T1, T2, T3, T4>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string name, object parameterModel);

        object FromSqlProcedureDataSetQuery(string name, object[] types, object parameterModel);

        Task<IEnumerable<T1>> FromSqlProcedureDataSetQueryAsync<T1>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> FromSqlProcedureDataSetQueryAsync<T1, T2>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string name, object parameterModel);

        Task<object> FromSqlProcedureDataSetQueryAsync(string name, object[] types, object parameterModel);

        TResult FromSqlScalarFunctionQuery<TResult>(string name, params object[] parameters) where TResult : struct;
        Task<TResult> FromSqlScalarFunctionQueryAsync<TResult>(string name, params object[] parameters) where TResult : struct;
        TResult FromSqlScalarFunctionQuery<TResult>(string name, object parameterModel) where TResult : struct;
        Task<TResult> FromSqlScalarFunctionQueryAsync<TResult>(string name, object parameterModel) where TResult : struct;


        DataTable FromSqlTableFunctionQuery(string name, params object[] parameters);
        Task<DataTable> FromSqlTableFunctionQueryAsync(string name, params object[] parameters);
        DataTable FromSqlTableFunctionQuery(string name, object parameterModel);
        Task<DataTable> FromSqlTableFunctionQueryAsync(string name, object parameterModel);
        IEnumerable<T> FromSqlTableFunctionQuery<T>(string name, params object[] parameters);
        Task<IEnumerable<T>> FromSqlTableFunctionQueryAsync<T>(string name, params object[] parameters);
        IEnumerable<T> FromSqlTableFunctionQuery<T>(string name, object parameterModel);
        Task<IEnumerable<T>> FromSqlTableFunctionQueryAsync<T>(string name, object parameterModel);
    }
}
