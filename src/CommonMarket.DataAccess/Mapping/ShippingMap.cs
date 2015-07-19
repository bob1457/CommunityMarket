using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class ShippingMap : EntityTypeConfiguration<Shipping>
    {
        public ShippingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Shipping");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ShippingDate).HasColumnName("ShippingDate");
            this.Property(t => t.ShippingMethodId).HasColumnName("ShippingMethodId");
            this.Property(t => t.OrderId).HasColumnName("OrderId");

            // Relationships
            this.HasRequired(t => t.Order)
                .WithMany(t => t.Shippings)
                .HasForeignKey(d => d.OrderId);
            this.HasRequired(t => t.ShippingMethod)
                .WithMany(t => t.Shippings)
                .HasForeignKey(d => d.ShippingMethodId);

        }
    }
}
