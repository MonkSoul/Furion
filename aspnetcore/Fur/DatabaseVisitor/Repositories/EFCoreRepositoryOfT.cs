using Autofac;
using Autofac.Extensions.DependencyInjection;
using Fur.DatabaseVisitor.Dependencies;
using Fur.DatabaseVisitor.Enums;
using Fur.DatabaseVisitor.Extensions;
using Fur.DatabaseVisitor.Page;
using Fur.DatabaseVisitor.Provider;
using Fur.DatabaseVisitor.TenantSaaS;
using Fur.DependencyInjection.Lifetimes;
using Fur.Extensions;
using Fur.Linq.Extensions;
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
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity>, IScopedLifetimeOfT<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IMaintenanceProvider _maintenanceProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITenantProvider _tenantProvider;
        public EFCoreRepositoryOfT(DbContext dbContext
            , IServiceProvider serviceProvider
            , ITenantProvider tenantProvider)
        {
            DbContext = dbContext;
            Entity = DbContext.Set<TEntity>();
            _tenantProvider = tenantProvider;

            _serviceProvider = serviceProvider;
            var autofacContainer = _serviceProvider.GetAutofacRoot();
            if (autofacContainer.IsRegistered<IMaintenanceProvider>())
            {
                _maintenanceProvider = autofacContainer.Resolve<IMaintenanceProvider>();
            }
        }

        public virtual DbContext DbContext { get; }
        public virtual DbSet<TEntity> Entity { get; }
        public virtual DatabaseFacade Database => DbContext.Database;
        public virtual DbConnection DbConnection => DbContext.Database.GetDbConnection();

        public virtual void Attach(TEntity entity)
        {
            if (EntityEntry(entity).State == EntityState.Detached)
            {
                DbContext.Attach(entity);
            }
        }

        public virtual EntityEntry<TEntity> EntityEntry(TEntity entity) => DbContext.Entry(entity);

        public EntityEntry<TEntity> Delete(TEntity entity)
        {
            return Entity.Remove(entity);
        }

        // 处理新增时候，创建时间/等字段
        private void InsertInvokeDefendPropertyHandler(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                var entityEntry = EntityEntry(entity);
                var createdTimeProperty = entityEntry.Property(_maintenanceProvider?.GetInsertedTimeName() ?? nameof(EntityBase<int>.CreatedTime));
                if (createdTimeProperty != null)
                {
                    createdTimeProperty.CurrentValue = DateTime.Now;
                }

                var tenantIdProperty = entityEntry.Property(nameof(Entity<int>.TenantId));
                if (tenantIdProperty != null)
                {
                    tenantIdProperty.CurrentValue = _tenantProvider.GetTenantId();
                }
            }
        }

        // 新增更新时候，更新时间/创建时间等字段
        private EntityEntry<TEntity>[] UpdateInvokeDefendPropertyHandler(Action updateHandler, params TEntity[] entities)
        {
            var entityEntries = new List<EntityEntry<TEntity>>();
            foreach (var entity in entities)
            {
                var entityEntry = EntityEntry(entity);
                entityEntries.Add(entityEntry);

                var updatedTimeProperty = entityEntry.Property(_maintenanceProvider?.GetUpdatedTimeName() ?? nameof(EntityBase<int>.UpdatedTime));
                if (updatedTimeProperty != null && !updatedTimeProperty.IsModified)
                {
                    updatedTimeProperty.CurrentValue = DateTime.Now;
                    updatedTimeProperty.IsModified = true;
                }
                updateHandler?.Invoke();
                var createdTimeProperty = entityEntry.Property(_maintenanceProvider?.GetInsertedTimeName() ?? nameof(EntityBase<int>.CreatedTime));
                if (createdTimeProperty != null)
                {
                    createdTimeProperty.IsModified = false;
                }

                var tenantIdProperty = entityEntry.Property(nameof(Entity<int>.TenantId));
                if (tenantIdProperty != null)
                {
                    tenantIdProperty.IsModified = false;
                }
            }
            return entityEntries.ToArray();
        }

        // 新增操作
        public virtual EntityEntry<TEntity> Insert(TEntity entity)
        {
            InsertInvokeDefendPropertyHandler(entity);
            return Entity.Add(entity);
        }

        public virtual void Insert(params TEntity[] entities)
        {
            InsertInvokeDefendPropertyHandler(entities);
            Entity.AddRange(entities);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            InsertInvokeDefendPropertyHandler(entities.ToArray());
            Entity.AddRange(entities);
        }

        public virtual ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity)
        {
            InsertInvokeDefendPropertyHandler(entity);
            return Entity.AddAsync(entity);
        }

        public virtual Task InsertAsync(params TEntity[] entities)
        {
            InsertInvokeDefendPropertyHandler(entities);
            return Entity.AddRangeAsync();
        }

        public virtual Task InsertAsync(IEnumerable<TEntity> entities)
        {
            InsertInvokeDefendPropertyHandler(entities.ToArray());
            return Entity.AddRangeAsync();
        }

        public virtual EntityEntry<TEntity> InsertSaveChanges(TEntity entity)
        {
            var trackEntity = Insert(entity);
            SaveChanges();
            return trackEntity;
        }

        public virtual void InsertSaveChanges(params TEntity[] entities)
        {
            Insert(entities);
            SaveChanges();
        }

        public virtual void InsertSaveChanges(IEnumerable<TEntity> entities)
        {
            Insert(entities);
            SaveChanges();
        }

        public virtual async ValueTask<EntityEntry<TEntity>> InsertSaveChangesAsync(TEntity entity)
        {
            var trackEntity = await InsertAsync(entity);
            await SaveChangesAsync();
            return trackEntity;
        }

        public virtual async Task InsertSaveChangesAsync(params TEntity[] entities)
        {
            await InsertAsync(entities);
            await SaveChangesAsync();
            await Task.CompletedTask;
        }

        public virtual async Task InsertSaveChangesAsync(IEnumerable<TEntity> entities)
        {
            await InsertAsync(entities);
            await SaveChangesAsync();
            await Task.CompletedTask;
        }

        // 更新操作
        public virtual EntityEntry<TEntity> Update(TEntity entity)
        {
            return UpdateInvokeDefendPropertyHandler(() =>
             {
                 Entity.Update(entity);
             }, entity).First();
        }

        public virtual void Update(params TEntity[] entities)
        {
            UpdateInvokeDefendPropertyHandler(() =>
            {
                Entity.UpdateRange(entities);
            }, entities);
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            UpdateInvokeDefendPropertyHandler(() =>
            {
                Entity.UpdateRange(entities);
            }, entities.ToArray());
        }

        public virtual Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity)
        {
            var entityEntry = UpdateInvokeDefendPropertyHandler(() =>
             {
                 Entity.Update(entity);
             }, entity).First();
            return Task.FromResult(entityEntry);
        }

        public virtual Task UpdateAsync(params TEntity[] entities)
        {
            UpdateInvokeDefendPropertyHandler(() =>
            {
                Entity.UpdateRange(entities);
            }, entities);
            return Task.CompletedTask;
        }

        public virtual Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            UpdateInvokeDefendPropertyHandler(() =>
            {
                Entity.UpdateRange(entities);
            }, entities.ToArray());
            return Task.CompletedTask;
        }

        public virtual EntityEntry<TEntity> UpdateSaveChanges(TEntity entity)
        {
            var trackEntity = Update(entity);
            SaveChanges();
            return trackEntity;
        }

        public virtual void UpdateSaveChanges(params TEntity[] entities)
        {
            Update(entities);
            SaveChanges();
        }

        public virtual void UpdateSaveChanges(IEnumerable<TEntity> entities)
        {
            Update(entities);
            SaveChanges();
        }

        public virtual async Task<EntityEntry<TEntity>> UpdateSaveChangesAsync(TEntity entity)
        {
            var trackEntities = await UpdateAsync(entity);
            await SaveChangesAsync();
            return trackEntities;
        }

        public virtual async Task UpdateSaveChangesAsync(params TEntity[] entities)
        {
            await UpdateAsync(entities);
            await SaveChangesAsync();
        }

        public virtual async Task UpdateSaveChangesAsync(IEnumerable<TEntity> entities)
        {
            await UpdateAsync(entities);
            await SaveChangesAsync();
        }

        // 更新指定列
        public virtual EntityEntry<TEntity> UpdateIncludeProperties(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = EntityEntry(entity);
            Attach(entity);

            foreach (var expression in propertyExpressions)
            {
                entityEntry.Property(expression).IsModified = true;
            }

            UpdateInvokeDefendPropertyHandler(null, entity);
            return entityEntry;
        }

        public virtual Task<EntityEntry<TEntity>> UpdateIncludePropertiesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = EntityEntry(entity);
            Attach(entity);
            foreach (var expression in propertyExpressions)
            {
                entityEntry.Property(expression).IsModified = true;
            }

            UpdateInvokeDefendPropertyHandler(null, entity);
            return Task.FromResult(entityEntry);
        }

        public virtual EntityEntry<TEntity> UpdateIncludePropertiesSaveChanges(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = UpdateIncludeProperties(entity, propertyExpressions);
            SaveChanges();
            return entityEntry;
        }

        public virtual async Task<EntityEntry<TEntity>> UpdateIncludePropertiesSaveChangesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = await UpdateIncludePropertiesAsync(entity, propertyExpressions);
            await SaveChangesAsync();
            return entityEntry;
        }

        public virtual void UpdateIncludeProperties(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            foreach (var entity in entities)
            {
                UpdateIncludeProperties(entity, propertyExpressions);
            }
        }

        public virtual async Task UpdateIncludePropertiesAsync(IAsyncEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            await foreach (var entity in entities)
            {
                await UpdateIncludePropertiesAsync(entity, propertyExpressions);
            }
        }

        public virtual void UpdateIncludePropertiesSaveChanges(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            UpdateIncludeProperties(entities, propertyExpressions);
            SaveChanges();
        }

        public virtual async Task UpdateIncludePropertiesSaveChangesAsync(IAsyncEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            await UpdateIncludePropertiesAsync(entities, propertyExpressions);
            await SaveChangesAsync();
        }

        // 排除指定列
        public virtual EntityEntry<TEntity> UpdateExcludeProperties(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = EntityEntry(entity);
            Attach(entity);
            entityEntry.State = EntityState.Modified;
            foreach (var expression in propertyExpressions)
            {
                entityEntry.Property(expression).IsModified = false;
            }

            UpdateInvokeDefendPropertyHandler(null, entity);
            return entityEntry;
        }

        public virtual Task<EntityEntry<TEntity>> UpdateExcludePropertiesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = EntityEntry(entity);
            Attach(entity);
            entityEntry.State = EntityState.Modified;
            foreach (var expression in propertyExpressions)
            {
                entityEntry.Property(expression).IsModified = false;
            }

            UpdateInvokeDefendPropertyHandler(null, entity);
            return Task.FromResult(entityEntry);
        }

        public virtual EntityEntry<TEntity> UpdateExcludePropertiesSaveChanges(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = UpdateExcludeProperties(entity, propertyExpressions);
            SaveChanges();
            return entityEntry;
        }

        public virtual async Task<EntityEntry<TEntity>> UpdateExcludePropertiesSaveChangesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = await UpdateExcludePropertiesAsync(entity, propertyExpressions);
            await SaveChangesAsync();
            return entityEntry;
        }

        public virtual void UpdateExcludeProperties(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            foreach (var entity in entities)
            {
                UpdateExcludeProperties(entity, propertyExpressions);
            }
        }

        public virtual async Task UpdateExcludePropertiesAsync(IAsyncEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            await foreach (var entity in entities)
            {
                await UpdateExcludePropertiesAsync(entity, propertyExpressions);
            }
        }

        public virtual void UpdateExcludePropertiesSaveChanges(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            UpdateExcludeProperties(entities, propertyExpressions);
            SaveChanges();
        }

        public virtual async Task UpdateExcludePropertiesSaveChangesAsync(IAsyncEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            await UpdateExcludePropertiesAsync(entities, propertyExpressions);
            await SaveChangesAsync();
        }

        // 新增或更新
        public virtual EntityEntry<TEntity> InsertOrUpdate(TEntity entity)
        {
            if (!IsKeySet(entity))
            {
                return Insert(entity);
            }
            else
            {
                return Update(entity);
            }
        }

        public virtual async Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity)
        {
            if (!IsKeySet(entity))
            {
                var entityEntry = await InsertAsync(entity);
                return entityEntry;
            }
            else
            {
                return await UpdateAsync(entity);
            }
        }

        public virtual EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity)
        {
            var entityEntry = InsertOrUpdate(entity);
            SaveChanges();
            return entityEntry;
        }

        public virtual async Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity)
        {
            var entityEntry = await InsertOrUpdateAsync(entity);
            await SaveChangesAsync();
            return entityEntry;
        }

        public virtual EntityEntry<TEntity> InsertOrUpdate(TEntity entity, DbUpdateTypeOptions dbUpdateTypeOptions, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            if (!IsKeySet(entity))
            {
                return Insert(entity);
            }
            else
            {
                if (dbUpdateTypeOptions == DbUpdateTypeOptions.IncludeProperties)
                {
                    return UpdateIncludeProperties(entity, propertyExpressions);
                }
                else
                {
                    return UpdateExcludeProperties(entity, propertyExpressions);
                }
            }
        }

        public virtual async Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity, DbUpdateTypeOptions dbUpdateTypeOptions, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            if (!IsKeySet(entity))
            {
                var entityEntry = await InsertAsync(entity);
                return entityEntry;
            }
            else
            {
                if (dbUpdateTypeOptions == DbUpdateTypeOptions.IncludeProperties)
                {
                    return await UpdateIncludePropertiesAsync(entity, propertyExpressions);
                }
                else
                {
                    return await UpdateExcludePropertiesAsync(entity, propertyExpressions);
                }
            }
        }

        public virtual EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity, DbUpdateTypeOptions dbUpdateTypeOptions, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = InsertOrUpdate(entity, dbUpdateTypeOptions, propertyExpressions);
            SaveChanges();
            return entityEntry;
        }

        public virtual async Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity, DbUpdateTypeOptions dbUpdateTypeOptions, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = await InsertOrUpdateAsync(entity, dbUpdateTypeOptions, propertyExpressions);
            await SaveChangesAsync();
            return entityEntry;
        }

        // 删除功能
        public virtual void Delete(params TEntity[] entities)
        {
            Entity.RemoveRange(entities);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            Entity.RemoveRange(entities);
        }

        public virtual EntityEntry<TEntity> Delete(object id)
        {
            var entity = Entity.Find(id);
            return Delete(entity);
        }

        public virtual Task<EntityEntry<TEntity>> DeleteAsync(TEntity entity)
        {
            return Task.FromResult(Entity.Remove(entity));
        }

        public virtual Task DeleteAsync(params TEntity[] entities)
        {
            Entity.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            Entity.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public virtual async Task<EntityEntry<TEntity>> DeleteAsync(object id)
        {
            var entity = await Entity.FindAsync(id);
            return await DeleteAsync(entity);
        }

        public virtual EntityEntry<TEntity> DeleteSaveChanges(TEntity entity)
        {
            var trackEntity = Delete(entity);
            SaveChanges();
            return trackEntity;
        }

        public virtual void DeleteSaveChanges(params TEntity[] entities)
        {
            Delete(entities);
            SaveChanges();
        }

        public virtual void DeleteSaveChanges(IEnumerable<TEntity> entities)
        {
            Delete(entities);
            SaveChanges();
        }

        public virtual EntityEntry<TEntity> DeleteSaveChanges(object id)
        {
            var trackEntity = Delete(id);
            SaveChanges();
            return trackEntity;
        }

        public virtual async Task<EntityEntry<TEntity>> DeleteSaveChangesAsync(TEntity entity)
        {
            var trackEntity = await DeleteAsync(entity);
            await SaveChangesAsync();
            return trackEntity;
        }

        public virtual async Task DeleteSaveChangesAsync(params TEntity[] entities)
        {
            await DeleteAsync(entities);
            await SaveChangesAsync();
        }

        public virtual async Task DeleteSaveChangesAsync(IEnumerable<TEntity> entities)
        {
            await DeleteAsync(entities);
            await SaveChangesAsync();
        }

        public virtual async Task<EntityEntry<TEntity>> DeleteSaveChangesAsync(object id)
        {
            var trackEntity = await DeleteAsync(id);
            await SaveChangesAsync();
            return trackEntity;
        }

        public virtual EntityEntry<TEntity> FakeDelete(TEntity entity, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue)
        {
            Attach(entity);
            var propertyName = fakeDeleteProperty.GetExpressionPropertyName();
            entity.SetProperyValue(propertyName, flagValue);

            return UpdateIncludeProperties(entity, fakeDeleteProperty);
        }

        public virtual void FakeDelete(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue)
        {
            foreach (var entity in entities)
            {
                FakeDelete(entity, fakeDeleteProperty, flagValue);
            }
        }

        public virtual Task<EntityEntry<TEntity>> FakeDeleteAsync(TEntity entity, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue)
        {
            Attach(entity);
            var propertyName = fakeDeleteProperty.GetExpressionPropertyName();
            entity.SetProperyValue(propertyName, flagValue);

            return UpdateIncludePropertiesAsync(entity, fakeDeleteProperty);
        }

        public virtual async Task FakeDeleteAsync(IAsyncEnumerable<TEntity> entities, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue)
        {
            await foreach (var entity in entities)
            {
                FakeDelete(entity, fakeDeleteProperty, flagValue);
            }
        }

        public virtual EntityEntry<TEntity> FakeDeleteSaveChanges(TEntity entity, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue)
        {
            var entityEntry = FakeDelete(entity, fakeDeleteProperty, flagValue);
            SaveChanges();
            return entityEntry;
        }

        public virtual void FakeDeleteSaveChanges(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue)
        {
            foreach (var entity in entities)
            {
                FakeDelete(entity, fakeDeleteProperty, flagValue);
            }
            SaveChanges();
        }

        public virtual async Task<EntityEntry<TEntity>> FakeDeleteSaveChangesAsync(TEntity entity, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue)
        {
            var entityEntry = await FakeDeleteAsync(entity, fakeDeleteProperty, flagValue);
            await SaveChangesAsync();
            return entityEntry;
        }

        public virtual async Task FakeDeleteSaveChangesAsync(IAsyncEnumerable<TEntity> entities, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue)
        {
            await foreach (var entity in entities)
            {
                FakeDelete(entity, fakeDeleteProperty, flagValue);
            }
            await SaveChangesAsync();
        }

        public virtual EntityEntry<TEntity> FakeDelete(object id, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue)
        {
            var entity = Find(id);
            return FakeDelete(entity, fakeDeleteProperty, flagValue);
        }

        public virtual async Task<EntityEntry<TEntity>> FakeDeleteAsync(object id, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue)
        {
            var entity = await FindAsync(id);
            return await FakeDeleteAsync(entity, fakeDeleteProperty, flagValue);
        }

        public virtual EntityEntry<TEntity> FakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue)
        {
            var entityEntry = FakeDelete(id, fakeDeleteProperty, flagValue);
            SaveChanges();
            return entityEntry;
        }

        public virtual async Task<EntityEntry<TEntity>> FakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue)
        {
            var entityEntry = await FakeDeleteAsync(id, fakeDeleteProperty, flagValue);
            await SaveChangesAsync();
            return entityEntry;
        }

        // 查询一条

        public virtual TEntity Find(object id)
        {
            return Entity.Find(id);
        }

        public virtual ValueTask<TEntity> FindAsync(object id)
        {
            return Entity.FindAsync(id);
        }

        public virtual TEntity Single()
        {
            return Entity.Single();
        }

        public virtual Task<TEntity> SingleAsync()
        {
            return Entity.SingleAsync();
        }

        public virtual TEntity Single(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.Single(expression);
        }

        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.SingleAsync(expression);
        }

        public virtual TEntity SingleOrDefault()
        {
            return Entity.SingleOrDefault();
        }

        public virtual Task<TEntity> SingleOrDefaultAsync()
        {
            return Entity.SingleOrDefaultAsync();
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.SingleOrDefault(expression);
        }

        public virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.SingleOrDefaultAsync(expression);
        }

        public virtual TEntity Single(bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().Single();
            else return Single();
        }

        public virtual Task<TEntity> SingleAsync(bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().SingleAsync();
            else return SingleAsync();
        }

        public virtual TEntity Single(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().Single(expression);
            else return Single(expression);
        }

        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().SingleAsync(expression);
            else return SingleAsync(expression);
        }

        public virtual TEntity SingleOrDefault(bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().SingleOrDefault();
            else return SingleOrDefault();
        }

        public virtual Task<TEntity> SingleOrDefaultAsync(bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().SingleOrDefaultAsync();
            else return SingleOrDefaultAsync();
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().SingleOrDefault(expression);
            else return SingleOrDefault(expression);
        }

        public virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().SingleOrDefaultAsync(expression);
            else return SingleOrDefaultAsync(expression);
        }

        public virtual TEntity First()
        {
            return Entity.First();
        }

        public virtual Task<TEntity> FirstAsync()
        {
            return Entity.FirstAsync();
        }

        public virtual TEntity First(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.First(expression);
        }

        public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.FirstAsync(expression);
        }

        public virtual TEntity FirstOrDefault()
        {
            return Entity.FirstOrDefault();
        }

        public virtual Task<TEntity> FirstOrDefaultAsync()
        {
            return Entity.FirstOrDefaultAsync();
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.FirstOrDefault(expression);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.FirstOrDefaultAsync(expression);
        }

        public virtual TEntity First(bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().First();
            else return First();
        }

        public virtual Task<TEntity> FirstAsync(bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().FirstAsync();
            else return FirstAsync();
        }

        public virtual TEntity First(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().First(expression);
            else return First(expression);
        }

        public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().FirstAsync(expression);
            else return FirstAsync(expression);
        }

        public virtual TEntity FirstOrDefault(bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().FirstOrDefault();
            else return FirstOrDefault();
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().FirstOrDefaultAsync();
            else return FirstOrDefaultAsync();
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().FirstOrDefault(expression);
            else return FirstOrDefault(expression);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().FirstOrDefaultAsync(expression);
            else return FirstOrDefaultAsync(expression);
        }

        // 获取所有
        public virtual IQueryable<TEntity> Get(bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return GetQueryConditionCombine(null, noTracking, ignoreQueryFilters);
        }

        public virtual Task<List<TEntity>> GetAsync(bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = GetQueryConditionCombine(null, noTracking, ignoreQueryFilters);
            return query.ToListAsync();
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return GetQueryConditionCombine(expression, noTracking, ignoreQueryFilters);
        }

        public virtual Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = GetQueryConditionCombine(expression, noTracking, ignoreQueryFilters);
            return query.ToListAsync();
        }

        public virtual IPagedListOfT<TEntity> GetPage(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = GetQueryConditionCombine(null, noTracking, ignoreQueryFilters);
            return query.ToPagedList(pageIndex, pageSize);
        }

        public virtual Task<IPagedListOfT<TEntity>> GetPageAsync(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = GetQueryConditionCombine(null, noTracking, ignoreQueryFilters);
            return query.ToPagedListAsync(pageIndex, pageSize);
        }

        public virtual IPagedListOfT<TEntity> GetPage(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = GetQueryConditionCombine(expression, noTracking, ignoreQueryFilters);
            return query.ToPagedList(pageIndex, pageSize);
        }

        public virtual Task<IPagedListOfT<TEntity>> GetPageAsync(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = GetQueryConditionCombine(expression, noTracking, ignoreQueryFilters);
            return query.ToPagedListAsync(pageIndex, pageSize);
        }

        public virtual int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public virtual int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return DbContext.SaveChanges(acceptAllChangesOnSuccess);
        }

        public virtual Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        public virtual Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess)
        {
            return DbContext.SaveChangesAsync(acceptAllChangesOnSuccess);
        }

        public virtual IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters)
        {
            return Entity.FromSqlRaw(sql, parameters);
        }

        public virtual IQueryable<TEntity> FromSqlRaw(string sql, object parameterModel)
        {
            return Entity.FromSqlRaw(sql, parameterModel.ToSqlParameters());
        }

        public virtual DataTable FromSqlQuery(string sql, params object[] parameters)
        {
            return Database.SqlQuery(sql, parameters);
        }

        public virtual Task<DataTable> FromSqlQueryAsync(string sql, params object[] parameters)
        {
            return Database.SqlQueryAsync(sql, parameters);
        }

        public virtual IEnumerable<T> FromSqlQuery<T>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<T>(sql, parameters);
        }

        public virtual Task<IEnumerable<T>> FromSqlQueryAsync<T>(string sql, params object[] parameters)
        {
            return Database.SqlQueryAsync<T>(sql, parameters);
        }

        public virtual object FromSqlQuery(string sql, object obj, params object[] parameters)
        {
            return Database.SqlQuery(sql, obj, parameters);
        }

        public virtual Task<object> FromSqlQueryAsync(string sql, object obj, params object[] parameters)
        {
            return Database.SqlQueryAsync(sql, obj, parameters);
        }

        public virtual DataTable FromSqlQuery(string sql, object parameterModel)
        {
            return FromSqlQuery(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<DataTable> FromSqlQueryAsync(string sql, object parameterModel)
        {
            return FromSqlQueryAsync(sql, parameterModel.ToSqlParameters());
        }

        public virtual IEnumerable<T> FromSqlQuery<T>(string sql, object parameterModel)
        {
            return FromSqlQuery<T>(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<IEnumerable<T>> FromSqlQueryAsync<T>(string sql, object parameterModel)
        {
            return FromSqlQueryAsync<T>(sql, parameterModel.ToSqlParameters());
        }

        public virtual DataSet FromSqlDataSetQuery(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery(sql, parameters);
        }

        public virtual Task<DataSet> FromSqlDataSetQueryAsync(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync(sql, parameters);
        }

        public virtual DataSet FromSqlDataSetQuery(string sql, object parameterModel)
        {
            return FromSqlDataSetQuery(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<DataSet> FromSqlDataSetQueryAsync(string sql, object parameterModel)
        {
            return FromSqlDataSetQueryAsync(sql, parameterModel.ToSqlParameters());
        }

        public virtual IEnumerable<T1> FromSqlDataSetQuery<T1>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1>(sql, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) FromSqlDataSetQuery<T1, T2>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2>(sql, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) FromSqlDataSetQuery<T1, T2, T3>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2, T3>(sql, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) FromSqlDataSetQuery<T1, T2, T3, T4>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2, T3, T4>(sql, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) FromSqlDataSetQuery<T1, T2, T3, T4, T5>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2, T3, T4, T5>(sql, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2, T3, T4, T5, T6>(sql, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(sql, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(sql, parameters);
        }

        public virtual object FromSqlDataSetQuery(string sql, object[] types, params object[] parameters)
        {
            return Database.SqlDataSetQuery(sql, types, parameters);
        }

        public virtual Task<IEnumerable<T1>> FromSqlDataSetQueryAsync<T1>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1>(sql, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> FromSqlDataSetQueryAsync<T1, T2>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2>(sql, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> FromSqlDataSetQueryAsync<T1, T2, T3>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2, T3>(sql, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> FromSqlDataSetQueryAsync<T1, T2, T3, T4>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2, T3, T4>(sql, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2, T3, T4, T5>(sql, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(sql, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(sql, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(sql, parameters);
        }

        public virtual Task<object> FromSqlDataSetQueryAsync(string sql, object[] types, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync(sql, types, parameters);
        }

        public virtual IEnumerable<T1> FromSqlDataSetQuery<T1>(string sql, object parameterModel)
        {
            return FromSqlDataSetQuery<T1>(sql, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) FromSqlDataSetQuery<T1, T2>(string sql, object parameterModel)
        {
            return FromSqlDataSetQuery<T1, T2>(sql, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) FromSqlDataSetQuery<T1, T2, T3>(string sql, object parameterModel)
        {
            return FromSqlDataSetQuery<T1, T2, T3>(sql, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) FromSqlDataSetQuery<T1, T2, T3, T4>(string sql, object parameterModel)
        {
            return FromSqlDataSetQuery<T1, T2, T3, T4>(sql, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) FromSqlDataSetQuery<T1, T2, T3, T4, T5>(string sql, object parameterModel)
        {
            return FromSqlDataSetQuery<T1, T2, T3, T4, T5>(sql, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6>(string sql, object parameterModel)
        {
            return FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6>(sql, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string sql, object parameterModel)
        {
            return FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(sql, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, object parameterModel)
        {
            return FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(sql, parameterModel.ToSqlParameters());
        }

        public virtual object FromSqlDataSetQuery(string sql, object[] types, object parameterModel)
        {
            return FromSqlDataSetQuery(sql, types, parameterModel.ToSqlParameters());
        }

        public virtual Task<IEnumerable<T1>> FromSqlDataSetQueryAsync<T1>(string sql, object parameterModel)
        {
            return FromSqlDataSetQueryAsync<T1>(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> FromSqlDataSetQueryAsync<T1, T2>(string sql, object parameterModel)
        {
            return FromSqlDataSetQueryAsync<T1, T2>(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> FromSqlDataSetQueryAsync<T1, T2, T3>(string sql, object parameterModel)
        {
            return FromSqlDataSetQueryAsync<T1, T2, T3>(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> FromSqlDataSetQueryAsync<T1, T2, T3, T4>(string sql, object parameterModel)
        {
            return FromSqlDataSetQueryAsync<T1, T2, T3, T4>(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5>(string sql, object parameterModel)
        {
            return FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5>(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string sql, object parameterModel)
        {
            return FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, object parameterModel)
        {
            return FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, object parameterModel)
        {
            return FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<object> FromSqlDataSetQueryAsync(string sql, object[] types, object parameterModel)
        {
            return FromSqlDataSetQueryAsync(sql, types, parameterModel.ToSqlParameters());
        }

        public virtual DataTable FromSqlProcedureQuery(string name, params object[] parameters)
        {
            return Database.SqlProcedureQuery(name, parameters);
        }

        public virtual Task<DataTable> FromSqlProcedureQueryAsync(string name, params object[] parameters)
        {
            return Database.SqlProcedureQueryAsync(name, parameters);
        }

        public virtual DataTable FromSqlProcedureQuery(string name, object parameterModel)
        {
            return Database.SqlProcedureQuery(name, parameterModel);
        }

        public virtual Task<DataTable> FromSqlProcedureQueryAsync(string name, object parameterModel)
        {
            return Database.SqlProcedureQueryAsync(name, parameterModel);
        }

        public virtual IEnumerable<T> FromSqlProcedureQuery<T>(string name, params object[] parameters)
        {
            return Database.SqlProcedureQuery<T>(name, parameters);
        }

        public virtual Task<IEnumerable<T>> FromSqlProcedureQueryAsync<T>(string name, params object[] parameters)
        {
            return Database.SqlProcedureQueryAsync<T>(name, parameters);
        }

        public virtual IEnumerable<T> FromSqlProcedureQuery<T>(string name, object parameterModel)
        {
            return Database.SqlProcedureQuery<T>(name, parameterModel);
        }

        public virtual Task<IEnumerable<T>> FromSqlProcedureQueryAsync<T>(string name, object parameterModel)
        {
            return Database.SqlProcedureQueryAsync<T>(name, parameterModel);
        }

        public virtual DataSet FromSqlProcedureDataSetQuery(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQuery(name, parameters);
        }

        public virtual Task<DataSet> FromSqlProcedureDataSetQueryAsync(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQueryAsync(name, parameters);
        }

        public virtual DataSet FromSqlProcedureDataSetQuery(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetQuery(name, parameterModel);
        }

        public virtual Task<DataSet> FromSqlProcedureDataSetQueryAsync(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetQueryAsync(name, parameterModel);
        }

        private IQueryable<TEntity> GetQueryConditionCombine(Expression<Func<TEntity, bool>> expression = null, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> entities = Entity;
            if (noTracking) entities = entities.AsNoTracking();
            if (ignoreQueryFilters) entities = entities.IgnoreQueryFilters();
            if (expression != null) entities = entities.Where(expression);

            return entities;
        }

        public virtual IEnumerable<T1> FromSqlProcedureDataSetQuery<T1>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQuery<T1>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) FromSqlProcedureDataSetQuery<T1, T2>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQuery<T1, T2>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) FromSqlProcedureDataSetQuery<T1, T2, T3>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQuery<T1, T2, T3>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) FromSqlProcedureDataSetQuery<T1, T2, T3, T4>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQuery<T1, T2, T3, T4>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQuery<T1, T2, T3, T4, T5>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(name, parameters);
        }

        public virtual object FromSqlProcedureDataSetQuery(string name, object[] types, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQuery(name, types, parameters);
        }

        public virtual Task<IEnumerable<T1>> FromSqlProcedureDataSetQueryAsync<T1>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQueryAsync<T1>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> FromSqlProcedureDataSetQueryAsync<T1, T2>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQueryAsync<T1, T2>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQueryAsync<T1, T2, T3>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQueryAsync<T1, T2, T3, T4>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(name, parameters);
        }

        public virtual Task<object> FromSqlProcedureDataSetQueryAsync(string name, object[] types, params object[] parameters)
        {
            return Database.SqlProcedureDataSetQueryAsync(name, types, parameters);
        }

        public virtual IEnumerable<T1> FromSqlProcedureDataSetQuery<T1>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetQuery<T1>(name, parameterModel);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) FromSqlProcedureDataSetQuery<T1, T2>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetQuery<T1, T2>(name, parameterModel);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) FromSqlProcedureDataSetQuery<T1, T2, T3>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetQuery<T1, T2, T3>(name, parameterModel);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) FromSqlProcedureDataSetQuery<T1, T2, T3, T4>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetQuery<T1, T2, T3, T4>(name, parameterModel);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetQuery<T1, T2, T3, T4, T5>(name, parameterModel);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6>(name, parameterModel);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(name, parameterModel);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) FromSqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(name, parameterModel);
        }

        public virtual object FromSqlProcedureDataSetQuery(string name, object[] types, object parameterModel)
        {
            return Database.SqlProcedureDataSetQuery(name, types, parameterModel);
        }

        public virtual Task<IEnumerable<T1>> FromSqlProcedureDataSetQueryAsync<T1>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetQueryAsync<T1>(name, parameterModel);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> FromSqlProcedureDataSetQueryAsync<T1, T2>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetQueryAsync<T1, T2>(name, parameterModel);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetQueryAsync<T1, T2, T3>(name, parameterModel);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetQueryAsync<T1, T2, T3, T4>(name, parameterModel);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5>(name, parameterModel);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(name, parameterModel);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(name, parameterModel);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> FromSqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(name, parameterModel);
        }

        public virtual Task<object> FromSqlProcedureDataSetQueryAsync(string name, object[] types, object parameterModel)
        {
            return Database.SqlProcedureDataSetQueryAsync(name, types, parameterModel);
        }

        // 标量函数
        public virtual TResult FromSqlScalarFunctionQuery<TResult>(string name, params object[] parameters) where TResult : struct
        {
            return Database.SqlFunctionQuery<TResult>(DbCanExecuteTypeOptions.DbScalarFunction, name, parameters).FirstOrDefault();
        }

        public virtual async Task<TResult> FromSqlScalarFunctionQueryAsync<TResult>(string name, params object[] parameters) where TResult : struct
        {
            var results = await Database.SqlFunctionQueryAsync<TResult>(DbCanExecuteTypeOptions.DbScalarFunction, name, parameters);
            return results.FirstOrDefault();
        }

        public virtual TResult FromSqlScalarFunctionQuery<TResult>(string name, object parameterModel) where TResult : struct
        {
            return Database.SqlFunctionQuery<TResult>(DbCanExecuteTypeOptions.DbScalarFunction, name, parameterModel).FirstOrDefault();
        }

        public virtual async Task<TResult> FromSqlScalarFunctionQueryAsync<TResult>(string name, object parameterModel) where TResult : struct
        {
            var results = await Database.SqlFunctionQueryAsync<TResult>(DbCanExecuteTypeOptions.DbScalarFunction, name, parameterModel);
            return results.FirstOrDefault();
        }

        public virtual DataTable FromSqlTableFunctionQuery(string name, params object[] parameters)
        {
            return Database.SqlFunctionQuery(DbCanExecuteTypeOptions.DbTableFunction, name, parameters);
        }

        public virtual Task<DataTable> FromSqlTableFunctionQueryAsync(string name, params object[] parameters)
        {
            return Database.SqlFunctionQueryAsync(DbCanExecuteTypeOptions.DbTableFunction, name, parameters);
        }

        public virtual DataTable FromSqlTableFunctionQuery(string name, object parameterModel)
        {
            return Database.SqlFunctionQuery(DbCanExecuteTypeOptions.DbTableFunction, name, parameterModel);
        }

        public virtual Task<DataTable> FromSqlTableFunctionQueryAsync(string name, object parameterModel)
        {
            return Database.SqlFunctionQueryAsync(DbCanExecuteTypeOptions.DbTableFunction, name, parameterModel);
        }

        public virtual IEnumerable<T> FromSqlTableFunctionQuery<T>(string name, params object[] parameters)
        {
            return Database.SqlFunctionQuery<T>(DbCanExecuteTypeOptions.DbTableFunction, name, parameters);
        }

        public virtual Task<IEnumerable<T>> FromSqlTableFunctionQueryAsync<T>(string name, params object[] parameters)
        {
            return Database.SqlFunctionQueryAsync<T>(DbCanExecuteTypeOptions.DbTableFunction, name, parameters);
        }

        public virtual IEnumerable<T> FromSqlTableFunctionQuery<T>(string name, object parameterModel)
        {
            return Database.SqlFunctionQuery<T>(DbCanExecuteTypeOptions.DbTableFunction, name, parameterModel);
        }

        public virtual Task<IEnumerable<T>> FromSqlTableFunctionQueryAsync<T>(string name, object parameterModel)
        {
            return Database.SqlFunctionQueryAsync<T>(DbCanExecuteTypeOptions.DbTableFunction, name, parameterModel);
        }

        public virtual bool Exists(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entity.Any() : Entity.Any(expression);
        }

        public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entity.AnyAsync() : Entity.AnyAsync(expression);
        }

        public virtual int Count(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entity.Count() : Entity.Count(expression);
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entity.CountAsync() : Entity.CountAsync(expression);
        }

        public virtual TEntity Max()
        {
            return Entity.Max();
        }

        public virtual TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return Entity.Max(expression);
        }

        public virtual Task<TEntity> MaxAsync()
        {
            return Entity.MaxAsync();
        }

        public virtual Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return Entity.MaxAsync(expression);
        }

        public virtual TEntity Min()
        {
            return Entity.Min();
        }

        public virtual TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return Entity.Min(expression);
        }

        public virtual Task<TEntity> MinAsync()
        {
            return Entity.MinAsync();
        }

        public virtual Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return Entity.MinAsync(expression);
        }

        public virtual bool IsKeySet(TEntity entity)
        {
            return EntityEntry(entity).IsKeySet;
        }
    }
}