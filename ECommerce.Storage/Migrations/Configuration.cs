using ECommerce.Domain;

namespace ECommerce.Storage.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ECommerce.Storage.Contexts.EmpContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ECommerce.Storage.Contexts.EmpContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Orders.AddOrUpdate(x=>x.OrderId,
                new Order(){ OrderId = 1, CustomerDetail = new CustomerDetail() { FirstName = "Raj"} },
                new Order(){ OrderId = 2, CustomerDetail = new CustomerDetail() { FirstName = "Ketan"} },
                new Order(){ OrderId = 3, CustomerDetail = new CustomerDetail() { FirstName = "Mahesh"} });
            //
         
        }
    }
}
