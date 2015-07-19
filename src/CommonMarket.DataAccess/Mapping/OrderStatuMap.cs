using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class OrderStatuMap : EntityTypeConfiguration<OrderStatu>
    {
        public OrderStatuMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.OrderStatusName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.OrderStatusDesc)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("OrderStatus");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OrderStatusName).HasColumnName("OrderStatusName");
            this.Property(t => t.OrderStatusDesc).HasColumnName("OrderStatusDesc");
        }
    }
}
