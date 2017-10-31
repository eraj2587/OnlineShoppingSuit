using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Linq;
using System.Linq;
using ECommerce.Domain;

namespace ECommerce.Storage.Configuration
{
    public class CustomDatabaseInitializer :
        DropCreateDatabaseIfModelChanges<DbContext>
        //CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DbContext context)
        {
            
            base.Seed(context);
        }
    }
}
