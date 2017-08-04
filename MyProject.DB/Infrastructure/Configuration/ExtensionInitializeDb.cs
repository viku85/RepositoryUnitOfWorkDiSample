using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyProject.DB.DataSeed.Infrastructure;
using System;

namespace MyProject.DB.Infrastructure.Configuration
{
    public static class ExtensionInitializeDb
    {
        /// <summary>
        /// Initializes the database.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns>Chained service provider.</returns>
        public static IServiceProvider InitDb(this IServiceProvider serviceProvider)
        {
            return serviceProvider.InitDb(null, false);
        }

        /// <summary>
        /// Initializes the database.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="appRootPath">The application root path.</param>
        /// <param name="seedDb">if set to <c>true</c>seeds initial values to database.</param>
        /// <returns>Chained service provider.</returns>
        public static IServiceProvider InitDb(
            this IServiceProvider serviceProvider,
            Func<IServiceProvider, string> appRootPath,
            bool seedDb)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
            {
                serviceScope.ServiceProvider.GetService<MyProjectContext>()
                      .Database.Migrate();
                serviceScope.SeedDb(seedDb, appRootPath);
            }

            return serviceProvider;
        }

        /// <summary>
        /// Seeds the database.
        /// </summary>
        /// <param name="serviceScope">The service scope.</param>
        /// <param name="seedDb">if set to <c>true</c> [seed database].</param>
        /// <param name="appRootPath">The application root path.</param>
        private static void SeedDb(
            this IServiceScope serviceScope,
            bool seedDb,
            Func<IServiceProvider, string> appRootPath)
        {
            if (seedDb)
            {
                serviceScope.ServiceProvider.SeedDatabase(appRootPath?.Invoke(serviceScope.ServiceProvider));
            }
        }
    }
}