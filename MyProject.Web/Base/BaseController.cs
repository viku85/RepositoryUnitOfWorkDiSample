using MyProject.Interface.Infrastructure;
using System.Web.Mvc;

namespace MyProject.Web.Base
{
    /// <summary>
    /// Base controller for the application
    /// </summary>
    public abstract class BaseController
        : Controller
    {
        /// <summary>
        /// Gets my project unit of work.
        /// </summary>
        /// <value>
        /// My project unit of work.
        /// </value>
        protected IMyProjectUnitOfWork MyProjectUnitOfWork { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public BaseController(IMyProjectUnitOfWork unitOfWork)
        {
            MyProjectUnitOfWork = unitOfWork;
        }
    }
}