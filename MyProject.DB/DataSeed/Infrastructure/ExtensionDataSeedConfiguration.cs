using Microsoft.Extensions.DependencyInjection;
using MyProject.DB.DataSeed.MasterModel;
using MyProject.Interface.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MyProject.DB.DataSeed.Infrastructure
{
    /// <summary>
    /// Data Seed configuration for the application.
    /// </summary>
    public static class ExtensionDataSeedConfiguration
    {
        /// <summary>
        /// Seeds the database.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="applicationBasePath">The application base path.</param>
        public static void SeedDatabase(this IServiceProvider serviceProvider, string applicationBasePath)
        {
            SeedDatabaseAsync(serviceProvider, applicationBasePath).Wait();
        }

        /// <summary>
        /// Seeds the database asynchronous.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="applicationBasePath">The application base path.</param>
        /// <returns>Task for seeding into DB.</returns>
        private static Task SeedDatabaseAsync(this IServiceProvider serviceProvider, string applicationBasePath)
        {
            var uow = serviceProvider.GetService<IMyProjectUnitOfWork>();
            return SeedAsync(uow, applicationBasePath);
        }

        /// <summary>
        /// Seeds the specified MyProject unit of work.
        /// </summary>
        /// <param name="myProjectUnitOfWork">The MyProject UnitOfWork.</param>
        /// <param name="applicationBasePath">The application base path.</param>
        /// <returns>Task for seeding into DB.</returns>
        private static async Task SeedAsync(IMyProjectUnitOfWork myProjectUnitOfWork, string applicationBasePath)
        {
            // Order is important as id generation are dependent on each other.
            // Parent to child item is followed.
            await new BookSeed(myProjectUnitOfWork.BookRepository, applicationBasePath).SeedAsync();

            await myProjectUnitOfWork.SaveAsync();
        }
    }
}