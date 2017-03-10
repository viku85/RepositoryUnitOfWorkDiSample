using System;
using System.Threading.Tasks;

namespace MyProject.Interface.Infrastructure
{
    /// <summary>
    /// Generic version of Unit Of Work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        int Save();

        Task<int> SaveAsync();
    }
}