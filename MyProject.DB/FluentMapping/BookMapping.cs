using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Model.DataModel;

namespace MyProject.DB.FluentMapping
{
    /// <summary>
    /// Fluent mapping for <see cref="Book"/> domain model
    /// </summary>
    public class BookMapping
    //: EntityTypeBuilder<Book>
    {
        public BookMapping(EntityTypeBuilder<Book> entityTypeBuilder)
        //:base(entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(m => m.Id);
            entityTypeBuilder.Property(m => m.Id).ValueGeneratedOnAdd();

            entityTypeBuilder.Property(m => m.BookTitle).IsRequired();
            entityTypeBuilder.Property(m => m.ISBN);
            entityTypeBuilder.Property(m => m.PublisherName).IsRequired();

            entityTypeBuilder.ToTable("Book", "MyProject");
        }
    }
}