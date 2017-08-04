using MyProject.DB.DataSeed.Infrastructure;
using MyProject.DB.DataSeed.Infrastructure.Base;
using MyProject.Interface.Repository;
using MyProject.Model.DataModel;
using System.IO;
using System.Threading.Tasks;

namespace MyProject.DB.DataSeed.MasterModel
{
    /// <summary>
    /// Seeds for <see cref="Book" entity./>
    /// </summary>
    /// <seealso cref="BaseSeed{Book}" />
    /// <seealso cref="ISeed" />
    internal class BookSeed
        : BaseSeed<Book>, ISeed
    {
        /// <summary>
        /// The data seed location
        /// </summary>
        private readonly string DataSeedLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookSeed"/> class.
        /// </summary>
        /// <param name="bookRepository">The book repository.</param>
        /// <param name="applicationBasePath">The application base path.</param>
        public BookSeed(IBookRepository bookRepository, string applicationBasePath)
                : base(bookRepository, applicationBasePath)
        {
            DataSeedLocation = Path.Combine(
                    ApplicationBasePath,
                    $"{nameof(MasterModel)}\\{nameof(BookSeed)}.json");
        }

        /// <summary>
        /// Seeds the data asynchronous.
        /// </summary>
        /// <returns>Status for saving the task.</returns>
        public async Task SeedAsync()
        {
            if (!await ModelRepository.AnyAsync())
            {
                var value = JsonRead(DataSeedLocation);
                await ModelRepository.CreateAsync(value);
            }
        }
    }
}