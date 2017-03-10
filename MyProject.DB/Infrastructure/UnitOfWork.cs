using Microsoft.EntityFrameworkCore;
using MyProject.Interface.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MyProject.DB.Infrastructure
{
    /// <summary>
    /// Unit of work implementation for having single instance of context and doing DB operation as transaction
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    public abstract class UnitOfWork<TContext>
        : IUnitOfWork
        where TContext : DbContext
    {
        private readonly TContext DbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected UnitOfWork(TContext context)
        {
            DbContext = context;
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        internal virtual TContext Context
        {
            get
            {
                return DbContext;
            }
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return Context.SaveChangesAsync();
        }

        #region " Dispose "

        /// <summary>
        /// To detect redundant calls
        /// </summary>
        private bool _disposedValue;

        /// <summary>
        /// Dispose the object
        /// </summary>
        /// <param name="disposing">IsDisposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (DbContext != null)
                    {
                        DbContext.Dispose();
                    }
                }
            }
            _disposedValue = true;
        }

        #region " IDisposable Support "

        /// <summary>
        /// Dispose the object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion " IDisposable Support "

        #endregion " Dispose "
    }
}