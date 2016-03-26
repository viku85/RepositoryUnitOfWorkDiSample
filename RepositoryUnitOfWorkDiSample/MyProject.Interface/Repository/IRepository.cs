using System;
using System.Linq;
using System.Linq.Expressions;

namespace MyProject.Interface.Repository
{
    /// <summary>
    /// Base repository interface for interaction with DB model
    /// </summary>
    /// <typeparam name="TModel">Entity Type</typeparam>
    public interface IRepository<TModel>
        where TModel : class
    {
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>Total rows count.</value>
        int Count { get; }

        /// <summary>
        /// Alls this instance.
        /// </summary>
        /// <returns>All records from model</returns>
        IQueryable<TModel> All();

        /// <summary>
        /// Gets model by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Entity</returns>
        TModel GetById(object id);

        /// <summary>
        /// Gets objects via optional filter, sort order, and includes.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>IQueryable for model entity</returns>
        IQueryable<TModel> Get(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>,
            IOrderedQueryable<TModel>> orderBy = null);

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
        IQueryable<TModel> Filter(Expression<Func<TModel, bool>> filter, out int total, int index = 0, int size = 50);

        /// <summary>
        /// Gets the object(s) is exists in database by specified filter.
        /// </summary>
        /// <param name="predicate">Specified filter expression</param>
        /// <returns><c>true</c> if contains the specified filter; otherwise, /c>.</returns>
        bool Contains(Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate">Specified filter.</param>
        /// <returns>Search result</returns>
        TModel Find(Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="entity">A new object to create.</param>
        /// <returns>Created object</returns>
        void Create(TModel entity);

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
    }
}