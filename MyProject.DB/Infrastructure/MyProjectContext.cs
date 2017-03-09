using System.Data.Entity;

namespace MyProject.DB.Infrastructure
{
    internal sealed class MyProjectContext
        : DbContext
    {
        public MyProjectContext()
            : base("name=MyProjectEntities")
        {
        }

        public MyProjectContext(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure mappings
            new FluentMapping.FluentMapping(modelBuilder);
        }
    }
}