using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProject.DB.FluentMapping;

namespace MyProject.DB.FluentMapping
{
    public class FluentMapping
    {
        public FluentMapping(DbModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Configurations.Add(new BookMapping());
        }
    }
}
