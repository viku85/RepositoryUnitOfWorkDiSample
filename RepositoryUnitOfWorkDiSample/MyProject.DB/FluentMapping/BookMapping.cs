using MyProject.Model.DataModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MyProject.DB.FluentMapping
{
    /// <summary>
    /// Fluent mapping for <see cref="Book"/> domain model
    /// </summary>
    public class BookMapping
        : EntityTypeConfiguration<Book>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookMapping"/> class.
        /// </summary>
        public BookMapping()
        {
            HasKey(m => m.Id);          // Primary key
            Property(m => m.Id)         // Identity column
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(m => m.BookTitle).IsRequired();
            Property(m => m.ISBN);
            Property(m => m.PublisherName).IsRequired();

            ToTable("Book");
        }
    }
}