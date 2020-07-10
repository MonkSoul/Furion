using Autofac;
using Autofac.Extensions.DependencyInjection;
using Fur.DatabaseVisitor.Contexts;
using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Extensions;
using Fur.DatabaseVisitor.Options;
using Fur.DatabaseVisitor.Provider;
using Fur.DatabaseVisitor.TenantSaaS;
using Fur.DependencyInjection.Lifetimes;
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
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity>, IScopedLifetimeOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        private readonly IMaintenanceProvider _maintenanceProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITenantProvider _tenantProvider;
        private readonly IDbContextPool _dbContextPool;

        public EFCoreRepositoryOfT(DbContext dbContext
            , IServiceProvider serviceProvider
            , ITenantProvider tenantProvider
            , IDbContextPool dbContextPool)
        {
            DbContext = dbContext;
            Entity = DbContext.Set<TEntity>();
            _tenantProvider = tenantProvider;
            _dbContextPool = dbContextPool;

            _dbContextPool.SaveDbContext(DbContext);

            _serviceProvider = serviceProvider;
            var autofacContainer = _serviceProvider.GetAutofacRoot();
            if (autofacContainer.IsRegistered<IMaintenanceProvider>())
            {
                _maintenanceProvider = autofacContainer.Resolve<IMaintenanceProvider>();
            }
        }

        public virtual DbContext DbContext { get; }
        public virtual DbSet<TEntity> Entity { get; }

        public virtual IQueryable<TEntity> DerailEntity => DerailEntity;
        public virtual DatabaseFacade Database => DbContext.Database;
        public virtual DbConnection DbConnection => DbContext.Database.GetDbConnection();

        public virtual int TenantId => _tenantProvider.GetTenantId();





        // 处理新增时候，创建时间/等字段
        private void SetInsertMaintenanceFields(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                var entityEntry = EntityEntry(entity);

                var createdTimeProperty = EntityEntryProperty(entityEntry, _maintenanceProvider?.GetCreatedTimePropertyName() ?? nameof(DbEntityBase.CreatedTime));
                if (createdTimeProperty != null)
                {
                    createdTimeProperty.CurrentValue = DateTime.Now;
                }

                var tenantIdProperty = EntityEntryProperty(entityEntry, nameof(DbEntity.TenantId));
                if (tenantIdProperty != null)
                {
                    tenantIdProperty.CurrentValue = _tenantProvider.GetTenantId();
                }
            }
        }

        // 新增更新时候，更新时间/创建时间等字段
        private EntityEntry<TEntity>[] SetUpdateMaintenanceFields(Action updateHandler, params TEntity[] entities)
        {
            var entityEntries = new List<EntityEntry<TEntity>>();
            foreach (var entity in entities)
            {
                var entityEntry = EntityEntry(entity);
                entityEntries.Add(entityEntry);

                var updatedTimeProperty = EntityEntryProperty(entityEntry, _maintenanceProvider?.GetUpdatedTimePropertyName() ?? nameof(DbEntityBase.UpdatedTime));
                if (updatedTimeProperty != null && !updatedTimeProperty.IsModified)
                {
                    updatedTimeProperty.CurrentValue = DateTime.Now;
                    updatedTimeProperty.IsModified = true;
                }
                updateHandler?.Invoke();
                var createdTimeProperty = EntityEntryProperty(entityEntry, _maintenanceProvider?.GetCreatedTimePropertyName() ?? nameof(DbEntityBase.CreatedTime));
                if (createdTimeProperty != null)
                {
                    createdTimeProperty.IsModified = false;
                }

                var tenantIdProperty = EntityEntryProperty(entityEntry, nameof(DbEntity.TenantId));
                if (tenantIdProperty != null)
                {
                    tenantIdProperty.IsModified = false;
                }
            }
            return entityEntries.ToArray();
        }

        public virtual DataTable FromSqlProcedureQuery(string name, params object[] parameters)
        {
            return Database.SqlProcedureExecute(name, parameters);
        }

        public virtual Task<DataTable> FromSqlProcedureQueryAsync(string name, params object[] parameters)
        {
            return Database.SqlProcedureExecuteAsync(name, parameters);
        }

        public virtual DataTable FromSqlProcedureQuery(string name, object parameterModel)
        {
            return Database.SqlProcedureExecute(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<DataTable> FromSqlProcedureQueryAsync(string name, object parameterModel)
        {
            return Database.SqlProcedureExecuteAsync(name, parameterModel.ToSqlParameters());
        }

        public virtual IEnumerable<T> FromSqlProcedureQuery<T>(string name, params object[] parameters)
        {
            return Database.SqlProcedureExecute<T>(name, parameters);
        }

        public virtual Task<IEnumerable<T>> FromSqlProcedureQueryAsync<T>(string name, params object[] parameters)
        {
            return Database.SqlProcedureExecuteAsync<T>(name, parameters);
        }

        public virtual IEnumerable<T> FromSqlProcedureQuery<T>(string name, object parameterModel)
        {
            return Database.SqlProcedureExecute<T>(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<IEnumerable<T>> FromSqlProcedureQueryAsync<T>(string name, object parameterModel)
        {
            return Database.SqlProcedureExecuteAsync<T>(name, parameterModel.ToSqlParameters());
        }

        public virtual DataSet FromSqlProcedureDataSetQuery(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute(name, parameters);
        }

        public virtual Task<DataSet> FromSqlProcedureDataSetQueryAsync(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync(name, parameters);
        }

        public virtual DataSet FromSqlProcedureDataSetQuery(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute(name, parameterModel.ToString());
        }

        public virtual Task<DataSet> FromSqlProcedureDataSetQueryAsync(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync(name, parameterModel.ToSqlParameters());
        }


        public virtual (Dictionary<string, object> outputValues, object returnValue) FromSqlProcedureJustExecute(string name, params object[] parameters)
        {
            return Database.SqlProcedureJustExecute(name, parameters);
        }

        public virtual Task<(Dictionary<string, object> outputValues, object returnValue)> FromSqlProcedureJustExecuteAsync(string name, params object[] parameters)
        {
            return Database.SqlProcedureJustExecuteAsync(name, parameters);
        }

        public virtual (Dictionary<string, object> outputValues, object returnValue) FromSqlProcedureJustExecute(string name, object parameterModel)
        {
            return Database.SqlProcedureJustExecute(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<(Dictionary<string, object> outputValues, object returnValue)> FromSqlProcedureJustExecuteAsync(string name, object parameterModel)
        {
            return Database.SqlProcedureJustExecuteAsync(name, parameterModel.ToSqlParameters());
        }


        private IQueryable<TEntity> GetQueryConditionCombine(Expression<Func<TEntity, bool>> expression = null, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> entities = noTracking ? DerailEntity : Entity;
            if (ignoreQueryFilters) entities = entities.IgnoreQueryFilters();
            if (expression != null) entities = entities.Where(expression);

            return entities;
        }

        public virtual IEnumerable<T1> FromSqlProcedureDataSetQuery<T1>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute<T1>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) FromSqlProcedureDataSetQuery<T1, T2>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) FromSqlProcedureDataSetQuery<T1, T2, T3>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) FromSqlProcedureDataSetQuery<T1, T2, T3, T4>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4, T5>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6, T7>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6, T7, T8>(name, parameters);
        }

        public virtual object FromSqlProcedureDataSetQuery(string name, Type[] types, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute(name, types, parameters);
        }

        public virtual Task<IEnumerable<T1>> FromSqlProcedureDataSetQueryAsync<T1>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> FromSqlProcedureDataSetQueryAsync<T1, T2>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7, T8>(name, parameters);
        }

        public virtual Task<object> FromSqlProcedureDataSetQueryAsync(string name, Type[] types, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync(name, types, parameters);
        }

        public virtual IEnumerable<T1> FromSqlProcedureDataSetQuery<T1>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute<T1>(name, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) FromSqlProcedureDataSetQuery<T1, T2>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2>(name, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) FromSqlProcedureDataSetQuery<T1, T2, T3>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3>(name, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) FromSqlProcedureDataSetQuery<T1, T2, T3, T4>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4>(name, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4, T5>(name, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6>(name, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6, T7>(name, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6, T7, T8>(name, parameterModel.ToSqlParameters());
        }

        public virtual object FromSqlProcedureDataSetQuery(string name, Type[] types, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute(name, types, parameterModel).ToSqlParameters();
        }

        public virtual Task<IEnumerable<T1>> FromSqlProcedureDataSetQueryAsync<T1>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1>(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> FromSqlProcedureDataSetQueryAsync<T1, T2>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2>(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3>(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4>(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5>(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6>(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7>(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7, T8>(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<object> FromSqlProcedureDataSetQueryAsync(string name, Type[] types, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync(name, types, parameterModel.ToSqlParameters());
        }

        // 标量函数
        public virtual TResult FromSqlScalarFunctionQuery<TResult>(string name, params object[] parameters) where TResult : struct
        {
            return Database.SqlFunctionExecute<TResult>(DbFunctionTypeOptions.Scalar, name, parameters).FirstOrDefault();
        }

        public virtual async Task<TResult> FromSqlScalarFunctionQueryAsync<TResult>(string name, params object[] parameters) where TResult : struct
        {
            var results = await Database.SqlFunctionExecuteAsync<TResult>(DbFunctionTypeOptions.Scalar, name, parameters);
            return results.FirstOrDefault();
        }

        public virtual TResult FromSqlScalarFunctionQuery<TResult>(string name, object parameterModel) where TResult : struct
        {
            return Database.SqlFunctionExecute<TResult>(DbFunctionTypeOptions.Scalar, name, parameterModel).FirstOrDefault();
        }

        public virtual async Task<TResult> FromSqlScalarFunctionQueryAsync<TResult>(string name, object parameterModel) where TResult : struct
        {
            var results = await Database.SqlFunctionExecuteAsync<TResult>(DbFunctionTypeOptions.Scalar, name, parameterModel);
            return results.FirstOrDefault();
        }

        public virtual DataTable FromSqlTableFunctionQuery(string name, params object[] parameters)
        {
            return Database.SqlFunctionExecute(DbFunctionTypeOptions.Table, name, parameters);
        }

        public virtual Task<DataTable> FromSqlTableFunctionQueryAsync(string name, params object[] parameters)
        {
            return Database.SqlFunctionExecuteAsync(DbFunctionTypeOptions.Table, name, parameters);
        }

        public virtual DataTable FromSqlTableFunctionQuery(string name, object parameterModel)
        {
            return Database.SqlFunctionExecute(DbFunctionTypeOptions.Table, name, parameterModel);
        }

        public virtual Task<DataTable> FromSqlTableFunctionQueryAsync(string name, object parameterModel)
        {
            return Database.SqlFunctionExecuteAsync(DbFunctionTypeOptions.Table, name, parameterModel);
        }

        public virtual IEnumerable<T> FromSqlTableFunctionQuery<T>(string name, params object[] parameters)
        {
            return Database.SqlFunctionExecute<T>(DbFunctionTypeOptions.Table, name, parameters);
        }

        public virtual Task<IEnumerable<T>> FromSqlTableFunctionQueryAsync<T>(string name, params object[] parameters)
        {
            return Database.SqlFunctionExecuteAsync<T>(DbFunctionTypeOptions.Table, name, parameters);
        }

        public virtual IEnumerable<T> FromSqlTableFunctionQuery<T>(string name, object parameterModel)
        {
            return Database.SqlFunctionExecute<T>(DbFunctionTypeOptions.Table, name, parameterModel);
        }

        public virtual Task<IEnumerable<T>> FromSqlTableFunctionQueryAsync<T>(string name, object parameterModel)
        {
            return Database.SqlFunctionExecuteAsync<T>(DbFunctionTypeOptions.Table, name, parameterModel);
        }
    }
}