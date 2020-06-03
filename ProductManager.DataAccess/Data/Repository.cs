using Microsoft.EntityFrameworkCore;
using ProductManager.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.DataAccess.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase, IEntity

    {
        /// <summary>
        /// DB context
        /// </summary>
        protected DbContext dbContext;
        protected readonly DbSet<TEntity> dbset;

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            //dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            this.dbset = dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>Total rows count.</value>
        public virtual int Count
        {
            get { return dbContext.Set<TEntity>().Count(); }
        }

        /// <summary>
        /// Alls this instance.
        /// </summary>
        /// <returns>All records from model</returns>
        public virtual IQueryable<TEntity> All()
        {
            return dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Gets model by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>DB Model</returns>
        public virtual ValueTask<TEntity> GetByIdAsync(object id)
        {
            return dbContext.Set<TEntity>().FindAsync(id);

        }

        /// <summary>
        /// Gets objects via optional filter, sort order, and includes.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>IQueryable for model entity</returns>
        public virtual IQueryable<TEntity> GetEntity(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return orderBy != null ? orderBy(query).AsQueryable() : query.AsQueryable();
        }

        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">Specified filter</param>
        /// <returns>IQueryable for model entity</returns>
        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return dbContext.Set<TEntity>().Where(predicate).AsQueryable();
        }

        /// <summary>
        /// Gets objects from database with filtering and paging.
        /// </summary>
        /// <param name="filter">Specified filter.</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Page index.</param>
        /// <param name="size">Page size.</param>
        /// <returns>IQueryable for model entity</returns>
        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50)
        {
            var skipCount = index * size;
            var resetSet = filter != null ? dbContext.Set<TEntity>().Where(filter).AsQueryable() :
                dbContext.Set<TEntity>().AsQueryable();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            total = resetSet.Count();
            return resetSet.AsQueryable();
        }

        /// <summary>
        /// Gets the object(s) is exists in database by specified filter.
        /// </summary>
        /// <param name="predicate">Specified filter expression</param>
        /// <returns><c>true</c> if contains the specified filter; otherwise, /c>.</returns>
        public async Task<bool> ContainAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbContext.Set<TEntity>().AnyAsync(predicate).ConfigureAwait(false);
        }

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate">Specified filter.</param>
        /// <returns>Search result</returns>
        public virtual async Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return  dbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="entity">A new object to create.</param>
        /// <returns>Created object</returns>
        public virtual async Task CreateAsync(TEntity entity) => await dbContext.Set<TEntity>().AddAsync(entity).ConfigureAwait(false);

        public virtual async Task UpdateAsync(TEntity entity) => await Task.Run(() => dbContext.Set<TEntity>().Update(entity)).ConfigureAwait(false);

        /// <summary>
        /// Deletes the object by primary key
        /// </summary>
        /// <param name="id">Object key</param>
        public virtual async Task DeleteAsync(object id)
        {
            var entityToDelete = await GetByIdAsync(id).ConfigureAwait(false);
            await Delete(entityToDelete).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="entity">Specified a existing object to delete.</param>
        public virtual async Task Delete(TEntity entity) => await Task.Run(() =>
          dbContext.Set<TEntity>().Remove(entity)).ConfigureAwait(false);

        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate">Specify filter.</param>
        public virtual async Task Delete(Expression<Func<TEntity, bool>> predicate)
        {
            await Task.Run(() => {
                var entitiesToDelete = Filter(predicate);
                foreach (var entity in entitiesToDelete)
                {
                    dbContext.Set<TEntity>().Remove(entity);
                }
            }).ConfigureAwait(false);

        }

        public async Task<int> SaveAsyc()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}