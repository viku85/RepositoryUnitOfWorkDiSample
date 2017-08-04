using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyProject.Interface.Infrastructure;

namespace MyProject.DB.Infrastructure.Configuration
{
    /// <summary>
    /// DB Configuration.
    /// </summary>
    public static class ExtensionRepositoryConfiguration
    {
        /// <summary>
        /// Adds the database configuration to MVC Core.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="connectionName">Name of the connection.</param>
        public static void AddDb(this IServiceCollection services, string connectionName)
        {
            services.AddDbContext<MyProjectContext>(options =>
            {
                options.UseSqlServer(connectionName,
                    serverOption => serverOption.MigrationsAssembly("MyProject.Web"));
            });
            services.RegisterRepositories();
            services.AddScoped<IMyProjectUnitOfWork>((serv) =>
            {
                return new MyProjectUnitOfWork(serv.GetService<MyProjectContext>(), serv);
            });
        }
    }
}