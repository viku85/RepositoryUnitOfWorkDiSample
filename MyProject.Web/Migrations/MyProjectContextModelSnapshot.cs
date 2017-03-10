using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MyProject.DB.Infrastructure;

namespace MyProject.Web.Migrations
{
    [DbContext(typeof(MyProjectContext))]
    partial class MyProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyProject.Model.DataModel.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BookTitle")
                        .IsRequired();

                    b.Property<string>("ISBN");

                    b.Property<string>("PublisherName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Book","MyProject");
                });
        }
    }
}
