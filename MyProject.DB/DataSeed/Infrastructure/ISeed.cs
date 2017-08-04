using System.Threading.Tasks;

namespace MyProject.DB.DataSeed.Infrastructure
{
    /// <summary>
    /// Seed interface
    /// </summary>
    public interface ISeed
    {
        /// <summary>
        /// Seeds the data asynchronous.
        /// </summary>
        /// <returns></returns>
        Task SeedAsync();
    }
}