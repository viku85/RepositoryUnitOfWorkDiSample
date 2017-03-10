using MyProject.Interface.Repository;

namespace MyProject.Interface.Infrastructure
{
    /// <summary>
    /// MyProject specific Unit Of Work
    /// </summary>
    public partial interface IMyProjectUnitOfWork
        : IUnitOfWork
    {
        // TODO: Any extra code specific for this particular Unit Of Work
    }
}