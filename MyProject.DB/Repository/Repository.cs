using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyProject.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyProject.DB.Repository
{
    /// <summary>
    /// Repository base class for generic methods across all individual repository.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    public class Repository<TModel, TContext>
        : IRepository<TModel>
        where TModel : class
        where TContext : DbContext
    {
        #region " Variables "

        /// <summary>
        /// DB context
        /// </summary>
        protected readonly TContext DbContext;

        /// <summary>
        /// The model
        /// </summary>
        protected readonly DbSet<TModel> Model;

        #endregion " Variables "

        protected Repository(TContext dbContext)
        {
            DbContext = dbContext;
            Model = DbContext.Set<TModel>();
        }

        #region " Properties "

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>Total rows count.</value>
        public virtual int Count
        {
            get { return Model.Count(); }
        }

        /// <summary>
        /// Gets the count asynchronously.
        /// </summary>
        /// <value>Total rows count as Task.</value>
        public virtual Task<int> CountAsync
        {
            get { return Model.CountAsync(); }
        }

        #endregion " Properties "

        #region " Create / Update / Delete Queries. "

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="entity">A new object to create.</param>
        /// <returns>Created object</returns>
        public virtual EntityEntry<TModel> Create(TModel entity)
        {
            return Model.Add(entity);
        }

        /// <summary>
        /// Create a new objects to database.
        /// </summary>
        /// <param name="entities">New objects to create in DB.</param>
        public virtual void Create(IEnumerable<TModel> entities)
        {
            Model.AddRange(entities);
        }

        /// <summary>
        /// Create a new model to database asynchronously.
        /// </summary>
        /// <param name="entity">A new object to create.</param>
        /// <returns>Created object</returns>
        public virtual Task<EntityEntry<TModel>> CreateAsync(TModel entity)
        {
            return Model.AddAsync(entity);
        }

        /// <summary>
        /// Create a new model to database asynchronously.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>The status of task.</returns>
        public virtual Task CreateAsync(IEnumerable<TModel> entities)
        {
            return Model.AddRangeAsync(entities);
        }

        /// <summary>
        /// Deletes the object by primary key
        /// </summary>
        /// <param name="id">Object key</param>
        public virtual void Delete(object id)
        {
            var entityToDelete = GetById(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="entity">Specified a existing object to delete.</param>
        public virtual void Delete(TModel entity)
        {
            Model.Remove(entity);
        }

        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate">Specify filter.</param>
        public virtual void Delete(Expression<Func<TModel, bool>> predicate)
        {
            Model.RemoveRange(Filter(predicate));
        }

        /// <summary>
        /// Deletes the object by primary key
        /// </summary>
        /// <param name="id">Object key</param>
        /// <returns>Task for deleting record.</returns>
        public virtual async Task DeleteAsync(object id)
        {
            var entityToDelete = await GetByIdAsync(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Updated model.</returns>
        public EntityEntry<TModel> Update(TModel model)
        {
            return Model.Update(model);
        }

        /// <summary>
        /// Updates the specified models.
        /// </summary>
        /// <param name="models">The models.</param>
        public void Update(IEnumerable<TModel> models)
        {
            Model.UpdateRange(models);
        }

        #endregion " Create / Update / Delete Queries. "

        #region " Select Statements "

        /// <summary>
        /// Alls this instance.
        /// </summary>
        /// <returns>All records from model</returns>
        public virtual IQueryable<TModel> All()
        {
            return Model;
        }

        /// <summary>
        /// Check for any records available in model.
        /// </summary>
        /// <returns><c>true</c>if, model has record.</c></returns>
        public virtual bool Any()
        {
            return Model.Any();
        }

        /// <summary>
        /// Asynchronous checks if model had any records available.
        /// </summary>
        /// <returns><c>true</c>if, model has record.</returns>
        public virtual Task<bool> AnyAsync()
        {
            return Model.AnyAsync();
        }

        /// <summary>
        /// Checks the existence of record by specified filter.
        /// </summary>
        /// <param name="predicate">Specified filter expression</param>
        /// <returns><c>true</c> if contains the record of specified filter; otherwise,<c>true</c>.</returns>
        public bool Contains(Expression<Func<TModel, bool>> predicate)
        {
            return Model.Any(predicate);
        }

        /// <summary>
        /// Checks the existence of record by specified filter asynchronously.
        /// </summary>
        /// <param name="predicate">Specified filter expression</param>
        /// <returns><c>true</c> if contains the record of specified filter; otherwise,<c>true</c>.</returns>
        public Task<bool> ContainsAsync(Expression<Func<TModel, bool>> predicate)
        {
            return Model.AnyAsync(predicate);
        }

        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">Specified filter</param>
        /// <returns>IQueryable for model entity</returns>
        public virtual IQueryable<TModel> Filter(Expression<Func<TModel, bool>> predicate)
        {
            return predicate != null ? Model.Where(predicate) : Model;
        }

        /// <summary>
        /// Gets objects from database with filtering and paging.
        /// </summary>
        /// <param name="filter">Specified filter.</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Page index.</param>
        /// <param name="size">Page size.</param>
        /// <returns>IQueryable for model entity</returns>
        public virtual IQueryable<TModel> Filter(
            Expression<Func<TModel, bool>> filter,
            out int total,
            int index = 0,
            int size = 50)
        {
            var skipCount = index * size;
            var resetSet = Filter(filter);
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            total = resetSet.Count();
            return resetSet;
        }

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate">Specified filter.</param>
        /// <returns>Search result</returns>
        public virtual TModel Find(Expression<Func<TModel, bool>> predicate)
        {
            return Model.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Find object by specified expression asynchronously.
        /// </summary>
        /// <param name="predicate">Specified filter.</param>
        /// <returns>Search result</returns>
        public virtual Task<TModel> FindAsync(Expression<Func<TModel, bool>> predicate)
        {
            return Model.FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Returns the first element of a sequence, or a default value if sequence contains no elements
        /// after applying required filters with projection.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="filter">The filter for query.</param>
        /// <param name="values">The expected values.</param>
        /// <returns>Object as per projected expression.</returns>
        public virtual TResult FirstOrDefault<TResult>(
            Expression<Func<TModel, bool>> filter,
            Expression<Func<TModel, TResult>> values)
        {
            var query = All();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Select(values).FirstOrDefault();
        }

        /// <summary>
        /// Returns the first element of a sequence, or a default value if sequence contains no elements
        /// after applying required filters with projection.
        /// </summary>
        /// <returns>First or default item.</returns>
        public virtual TModel FirstOrDefault()
        {
            return Model.FirstOrDefault();
        }

        /// <summary>
        /// Returns the first element of a sequence, or a default value if sequence contains no elements
        /// after applying required filters with projection in asynchronous manner.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="filter">The filter for query.</param>
        /// <param name="values">The expected values.</param>
        /// <returns>Object as per projected expression.</returns>
        public virtual Task<TResult> FirstOrDefaultAsync<TResult>(
            Expression<Func<TModel, bool>> filter,
            Expression<Func<TModel, TResult>> values)
        {
            var query = All();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Select(values).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns the first element of a sequence, or a default value if sequence contains no elements
        /// after applying required filters with projection in asynchronous manner.
        /// </summary>
        /// <returns>First or default item.</returns>
        public virtual Task<TModel> FirstOrDefaultAsync()
        {
            return Model.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets objects via optional filter, sort order, and includes.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>IQueryable for model entity</returns>
        public virtual IQueryable<TModel> Get(
            Expression<Func<TModel, bool>> filter = null,
            Func<IQueryable<TModel>,
            IOrderedQueryable<TModel>> orderBy = null)
        {
            var query = All();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            // TODO: Check for order by as orderBy(query).AsQueryable()
            return orderBy != null ? orderBy(query) : query;
        }

        /// <summary>
        /// Gets model by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>DB Model</returns>
        public virtual TModel GetById(object id)
        {
            return Model.Find(id);
        }

        /// <summary>
        /// Gets model by id asynchronously.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Task for getting DB Model</returns>
        public virtual Task<TModel> GetByIdAsync(object id)
        {
            return Model.FindAsync(id);
        }

        #endregion " Select Statements "
    }
}