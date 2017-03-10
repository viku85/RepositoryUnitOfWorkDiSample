using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProject.DB.FluentMapping;
using Microsoft.EntityFrameworkCore;
using MyProject.Model.DataModel;

namespace MyProject.DB.FluentMapping
{
    public class FluentMapping
    {
        public FluentMapping(ModelBuilder dbModelBuilder)
        {
            new BookMapping(dbModelBuilder.Entity<Book>());
        }
    }
}
