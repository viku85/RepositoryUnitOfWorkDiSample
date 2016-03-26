using System.Data.Entity;

namespace MyProject.Interface.Infrastructure
{
    /// <summary>
    /// Context factory for creation of context
    /// </summary>
    /// <typeparam name="TContext">Database context</typeparam>
    public interface IContextFactory<out TContext>
        where TContext : DbContext
    {
        /// <summary>
        /// Creates new database context
        /// </summary>
        /// <returns>New Database context</returns>
        TContext Create();
    }
}