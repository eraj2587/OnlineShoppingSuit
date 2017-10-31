using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Domain;

namespace ECommerce.Storage.Configuration
{
    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            this.Property(p => p.OrderId).HasColumnOrder(0);
        }
    }
}
