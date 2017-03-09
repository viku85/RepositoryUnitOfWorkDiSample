using MyProject.Interface.Infrastructure;
using System.Data.Entity;

namespace MyProject.DB.Infrastructure
{
    /// <summary>
    /// Context Factory for project
    /// </summary>
    public sealed class MyProjectContextFactory
        : IContextFactory<DbContext>
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyProjectContextFactory"/> class.
        /// </summary>
        /// <param name="connectionStringOrName">Name of the connection string or actual connection string.</param>
        public MyProjectContextFactory(string connectionStringOrName)
        {
            _connectionString = connectionStringOrName;
        }

        /// <summary>
        /// Creates new database context.
        /// </summary>
        /// <returns>DbContext: <see cref="MyProjectContext"/></returns>
        public DbContext Create()
        {
            return new MyProjectContext(_connectionString);
        }
    }
}