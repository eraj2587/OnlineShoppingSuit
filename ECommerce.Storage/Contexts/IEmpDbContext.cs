using System.Data.Entity;
using ECommerce.Domain;
using ECommerce.Storage.Repository;

namespace ECommerce.Storage.Contexts
{
    public interface IEmpDbContext : IUnitOfWork
    {
        IDbSet<Order> Orders { get; set; }
        IDbSet<Product> Product { get; set; }
        IDbSet<EntityChangeAudit> EntityChangeAudits { get; set; }
    }
}