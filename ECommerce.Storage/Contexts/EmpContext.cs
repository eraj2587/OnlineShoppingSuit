using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using ECommerce.Domain;
using ECommerce.Storage.Configuration;
using Newtonsoft.Json;

namespace ECommerce.Storage.Contexts
{
    public class EmpContext : DbContext, IEmpDbContext
    {
        static EmpContext()
        {
            Database.SetInitializer<EmpContext>(null);
        }

        public EmpContext()
            : base("EmpDBConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EmpContext, Migrations.Configuration>("EmpDBConnectionString"));
        }

        public EmpContext(string connectionStringName)
            : base(connectionStringName)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public EmpContext(DbConnection dbConnection)
            : base(dbConnection, false)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //Adding configurations
            modelBuilder.Configurations.Add(new OrderConfiguration());
            //modelBuilder.Entity<Order>().Property(x => x.CreatedDate).HasColumnType("datetime2");
        }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<Product> Product { get; set; }
        public IDbSet<EntityChangeAudit> EntityChangeAudits { get; set; }

        public void CommitChanges()
        {
            try
            {
                SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {
                var sb = new StringBuilder("Commit Changes Failed with:");
                foreach (DbEntityValidationResult error in exception.EntityValidationErrors)
                {
                    foreach (DbValidationError dbValidationError in error.ValidationErrors)
                    {
                        sb.AppendLine(dbValidationError.PropertyName + "###" + dbValidationError.ErrorMessage);
                    }
                }
                // _logger.LogError(sb.ToString());
                throw;
            }
        }

        public override int SaveChanges()
        {
            var changeInfo = ChangeTracker.Entries()
                 .Where(
                     t =>
                         t.State == EntityState.Added || t.State == EntityState.Modified
                // || t.State == EntityState.Deleted
                         ).ToList();

            var commitId = Guid.NewGuid();
            var commitedOnUTC = DateTime.UtcNow;

            foreach (var objInfo in changeInfo)
            {

                if (objInfo.Entity is ModelBase)
                {
                    var objBase = objInfo.Entity as ModelBase;

                    switch (objInfo.State)
                    {
                        case EntityState.Added:
                            objBase.CreatedBy = Thread.CurrentPrincipal.Identity.Name;
                            objBase.CreatedOn = DateTime.UtcNow;
                            break;
                        //case EntityState.Deleted:
                        case EntityState.Modified:
                            objBase.UpdatedBy = Thread.CurrentPrincipal.Identity.Name;
                            objBase.UpdatedOn = DateTime.UtcNow;
                            break;
                    }
                }

                var audit = new EntityChangeAudit
                {
                    EntityType = objInfo.Entity.GetType().FullName,
                    ChangeState = objInfo.State.ToString(),
                    CommitId = commitId,
                    CommitedOnUTC = commitedOnUTC,
                    CommitedOnLocal = DateTime.Now,
                    CommitedBy = Thread.CurrentPrincipal.Identity.Name
                };

                // PropertyName: Before|After
                var changedProperties = new Dictionary<string, EntityChangeAudit.ChangePropertyPair>();

                foreach (var propName in objInfo.CurrentValues.PropertyNames)
                {
                    changedProperties.Add(propName, new EntityChangeAudit.ChangePropertyPair
                    {
                        PropertName = propName,
                        Before = objInfo.State == EntityState.Added ? null : objInfo.OriginalValues[propName],
                        After = objInfo.State == EntityState.Deleted ? null : objInfo.CurrentValues[propName]
                    });
                }

                audit.ChangedValues = JsonConvert.SerializeObject(changedProperties, new JsonSerializerSettings
                {
                    TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
                });

                EntityChangeAudits.Add(audit);
            }

            return base.SaveChanges();
        }
    }

    public class EntityChangeAudit
    {
        public int EntityChangeAuditId { get; set; }
        public Guid CommitId { get; set; }
        public string EntityType { get; set; }
        public string ChangedValues { get; set; }
        public string ChangeState { get; set; }
        public DateTime CommitedOnUTC { get; set; }
        public DateTime CommitedOnLocal { get; set; }
        public string CommitedBy { get; set; }


        // Each changed property will have before and after value as object.
        // used to be sterilized and stored as JSON
        public class ChangePropertyPair
        {
            public string PropertName { get; set; }
            public object Before { get; set; }
            public object After { get; set; }
        }
    }

   
}