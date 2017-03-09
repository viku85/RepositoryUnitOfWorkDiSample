using MyProject.Interface.Infrastructure;
using System.Data.Entity;

namespace MyProject.DB.Infrastructure
{
    public sealed partial class MyProjectUnitOfWork
        : UnitOfWork<DbContext>, IMyProjectUnitOfWork
    {
        public MyProjectUnitOfWork(IContextFactory<DbContext> contextFactory)
            : base(contextFactory)
        {
        }

        // TODO: Any extra code specific for this particular Unit Of Work
    }
}