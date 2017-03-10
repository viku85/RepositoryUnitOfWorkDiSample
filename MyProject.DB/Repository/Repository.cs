using Microsoft.EntityFrameworkCore;
using MyProject.Interface.Repository;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MyProject.DB.Repository
{
    public class Repository<TModel, TContext>
        : IRepository<TModel> where TModel : class
        where TContext : DbContext
    {
        /// <summary>
        /// DB context
        /// </summary>
        protected readonly TContext DbContext;

        protected Repository(TContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>Total rows count.</value>
        public virtual int Count
        {
            get { return DbContext.Set<TModel>().Count(); }
        }

        /// <summary>
        /// Alls this instance.
        /// </summary>
        /// <returns>All records from model</returns>
        public virtual IQueryable<TModel> All()
        {
            return DbContext.Set<TModel>();
        }

        /// <summary>
        /// Gets model by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>DB Model</returns>
        public virtual TModel GetById(object id)
        {
            return DbContext.Set<TModel>().Find(id);
        }

        /// <summary>
        /// Gets objects via optional filter, sort order, and includes.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>IQueryable for model entity</returns>
        public virtual IQueryable<TModel> Get(Expression<Func<TModel, bool>> filter = null,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null)
        {
            IQueryable<TModel> query = DbContext.Set<TModel>();

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
        public virtual IQueryable<TModel> Filter(Expression<Func<TModel, bool>> predicate)
        {
            return DbContext.Set<TModel>().Where(predicate).AsQueryable();
        }

        /// <summary>
        /// Gets objects from database with filtering and paging.
        /// </summary>
        /// <param name="filter">Specified filter.</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Page index.</param>
        /// <param name="size">Page size.</param>
        /// <returns>IQueryable for model entity</returns>
        public virtual IQueryable<TModel> Filter(Expression<Func<TModel, bool>> filter, out int total, int index = 0, int size = 50)
        {
            var skipCount = index * size;
            var resetSet = filter != null ? DbContext.Set<TModel>().Where(filter).AsQueryable()
                : DbContext.Set<TModel>().AsQueryable();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            total = resetSet.Count();
            return resetSet.AsQueryable();
        }

        /// <summary>
        /// Gets the object(s) is exists in database by specified filter.
        /// </summary>
        /// <param name="predicate">Specified filter expression</param>
        /// <returns><c>true</c> if contains the specified filter; otherwise, /c>.</returns>
        public bool Contains(Expression<Func<TModel, bool>> predicate)
        {
            return DbContext.Set<TModel>().Any(predicate);
        }

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate">Specified filter.</param>
        /// <returns>Search result</returns>
        public virtual TModel Find(Expression<Func<TModel, bool>> predicate)
        {
            return DbContext.Set<TModel>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="entity">A new object to create.</param>
        /// <returns>Created object</returns>
        public virtual void Create(TModel entity)
        {
            DbContext.Set<TModel>().Add(entity);
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
            DbContext.Set<TModel>().Remove(entity);
        }

        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate">Specify filter.</param>
        public virtual void Delete(Expression<Func<TModel, bool>> predicate)
        {
            var entitiesToDelete = Filter(predicate);
            foreach (var entity in entitiesToDelete)
            {
                DbContext.Set<TModel>().Remove(entity);
            }
        }
    }
}