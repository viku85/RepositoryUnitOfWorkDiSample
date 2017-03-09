using MyProject.Interface.Infrastructure;
using System;
using System.Data.Entity;

namespace MyProject.DB.Infrastructure
{
    /// <summary>
    /// Unit of work implementation for having single instance of context and doing DB operation as transaction
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    public abstract class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        /// <summary>
        /// DB context
        /// </summary>
        private TContext _dbContext;

        /// <summary>
        /// The DB context factory
        /// </summary>
        private readonly IContextFactory<TContext> _dbContextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.
        /// </summary>
        /// <param name="dbContextFactory">The db context factory.</param>
        protected UnitOfWork(IContextFactory<TContext> dbContextFactory)
            : this(dbContextFactory.Create())
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected UnitOfWork(TContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        public virtual TContext Context
        {
            get
            {
                return _dbContext;
            }
        }

        /// <summary>
        /// Regenerates the context.
        /// </summary>
        /// <remarks>WARNING: Calling with dirty context will save changes automatically</remarks>
        public void RegenerateContext()
        {
            if (_dbContext != null)
            {
                Save();
            }
            _dbContext = _dbContextFactory.Create();
        }

        /// <summary>
        /// Saves Context changes.
        /// </summary>
        public void Save()
        {
            Context.SaveChanges();
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
                    if (_dbContext != null)
                    {
                        _dbContext.Dispose();
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