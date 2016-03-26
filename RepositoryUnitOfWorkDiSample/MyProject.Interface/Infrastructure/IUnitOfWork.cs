using System;

namespace MyProject.Interface.Infrastructure
{
    /// <summary>
    /// Generic version of Unit Of Work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Regenerates the context.
        /// </summary>
        void RegenerateContext();

        /// <summary>
        /// Saves Context changes.
        /// </summary>
        void Save();
    }
}