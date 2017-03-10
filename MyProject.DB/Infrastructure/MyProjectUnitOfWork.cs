using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyProject.Interface.Infrastructure;
using MyProject.Interface.Repository;
using System;

namespace MyProject.DB.Infrastructure
{
    /// <summary>
    /// Project specific Unit Of Work.
    /// </summary>
    /// <seealso cref="Infrastructure.UnitOfWork{MyProjectContext}" />
    /// <seealso cref="IMyProjectUnitOfWork" />
    public sealed partial class MyProjectUnitOfWork
        : UnitOfWork<MyProjectContext>, IMyProjectUnitOfWork
    {
    }
}