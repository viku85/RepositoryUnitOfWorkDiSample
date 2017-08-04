using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyProject.Interface.Repository
{
    /// <summary>
    /// Base repository interface for interaction with DB model
    /// </summary>
    /// <typeparam name="TModel">Entity Type</typeparam>
    public interface IRepository<TModel>
        where TModel : class
    {
        #region " Properties "

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>Total rows count.</value>
        int Count { get; }

        /// <summary>
        /// Gets the count asynchronously.
        /// </summary>
        /// <value>Total rows count as Task.</value>
        Task<int> CountAsync { get; }

        #endregion " Properties "

        #region " Create / Update / Delete Queries. "

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="entity">A new object to create.</param>
        /// <returns>Created object</returns>
        EntityEntry<TModel> Create(TModel entity);

        /// <summary>
        /// Create a new objects to database.
        /// </summary>
        /// <param name="entities">New objects to create in DB.</param>
        void Create(IEnumerable<TModel> entities);

        /// <summary>
        /// Create a new model to database asynchronously.
        /// </summary>
        /// <param name="entity">A new object to create.</param>
        /// <returns>Created object</returns>
        Task<EntityEntry<TModel>> CreateAsync(TModel entity);

        /// <summary>
        /// Create a new model to database asynchronously.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>The status of task.</returns>
        Task CreateAsync(IEnumerable<TModel> entities);

        /// <summary>
        /// Deletes the object by primary key
        /// </summary>
        /// <param name="id">Object key</param>
        void Delete(object id);

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="entity">Specified a existing object to delete.</param>
        void Delete(TModel entity);

        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate">Specify filter.</param>
        void Delete(Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Deletes the object by primary key
        /// </summary>
        /// <param name="id">Object key</param>
        /// <returns>Task for deleting record.</returns>
        Task DeleteAsync(object id);

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Updated model.</returns>
        EntityEntry<TModel> Update(TModel model);

        /// <summary>
        /// Updates the specified models.
        /// </summary>
        /// <param name="models">The models.</param>
        void Update(IEnumerable<TModel> models);

        #endregion " Create / Update / Delete Queries. "

        #region " Select Statements "

        /// <summary>
        /// Alls this instance.
        /// </summary>
        /// <returns>All records from model</returns>
        IQueryable<TModel> All();

        /// <summary>
        /// Check for any records available in model.
        /// </summary>
        /// <returns><c>true</c>if, model has record.</c></returns>
        bool Any();

        /// <summary>
        /// Asynchronous checks if model had any records available.
        /// </summary>
        /// <returns><c>true</c>if, model has record.</returns>
        Task<bool> AnyAsync();

        /// <summary>
        /// Checks the existence of record by specified filter.
        /// </summary>
        /// <param name="predicate">Specified filter expression</param>
        /// <returns><c>true</c> if contains the record of specified filter; otherwise,<c>true</c>.</returns>
        bool Contains(Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Checks the existence of record by specified filter asynchronously.
        /// </summary>
        /// <param name="predicate">Specified filter expression</param>
        /// <returns><c>true</c> if contains the record of specified filter; otherwise,<c>true</c>.</returns>
        Task<bool> ContainsAsync(Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">Specified filter</param>
        /// <returns>IQueryable for model entity</returns>
        IQueryable<TModel> Filter(Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Gets objects from database with filtering and paging.
        /// </summary>
        /// <param name="filter">Specified filter.</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Page index.</param>
        /// <param name="size">Page size.</param>
        /// <returns>IQueryable for model entity</returns>
        IQueryable<TModel> Filter(
            Expression<Func<TModel, bool>> filter,
            out int total,
            int index = 0,
            int size = 50);

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate">Specified filter.</param>
        /// <returns>Search result</returns>
        TModel Find(Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Find object by specified expression asynchronously.
        /// </summary>
        /// <param name="predicate">Specified filter.</param>
        /// <returns>Search result</returns>
        Task<TModel> FindAsync(Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Returns the first element of a sequence, or a default value if sequence contains no elements
        /// after applying required filters with projection.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="filter">The filter for query.</param>
        /// <param name="values">The expected values.</param>
        /// <returns>Object as per projected expression.</returns>
        TResult FirstOrDefault<TResult>(
            Expression<Func<TModel, bool>> filter,
            Expression<Func<TModel, TResult>> values);

        /// <summary>
        /// Returns the first element of a sequence, or a default value if sequence contains no elements
        /// after applying required filters with projection.
        /// </summary>
        /// <returns>First or default item.</returns>
        TModel FirstOrDefault();

        /// <summary>
        /// Returns the first element of a sequence, or a default value if sequence contains no elements
        /// after applying required filters with projection in asynchronous manner.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="filter">The filter for query.</param>
        /// <param name="values">The expected values.</param>
        /// <returns>Object as per projected expression.</returns>
        Task<TResult> FirstOrDefaultAsync<TResult>(
            Expression<Func<TModel, bool>> filter,
            Expression<Func<TModel, TResult>> values);

        /// <summary>
        /// Returns the first element of a sequence, or a default value if sequence contains no elements
        /// after applying required filters with projection in asynchronous manner.
        /// </summary>
        /// <returns>First or default item.</returns>
        Task<TModel> FirstOrDefaultAsync();

        /// <summary>
        /// Gets objects via optional filter, sort order, and includes.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>IQueryable for model entity</returns>
        IQueryable<TModel> Get(
            Expression<Func<TModel, bool>> filter = null,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null);

        /// <summary>
        /// Gets model by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>DB Model</returns>
        TModel GetById(object id);

        /// <summary>
        /// Gets model by id asynchronously.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Task for getting DB Model</returns>
        Task<TModel> GetByIdAsync(object id);

        #endregion " Select Statements "
    }
}