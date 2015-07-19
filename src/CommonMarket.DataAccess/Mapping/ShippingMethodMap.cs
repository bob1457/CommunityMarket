using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class ShippingMethodMap : EntityTypeConfiguration<ShippingMethod>
    {
        public ShippingMethodMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ShippingMethodName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ShippingMethodDesc)
                .HasMaxLength(255);

            this.Property(t => t.Notes)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ShippingMethod");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ShippingMethodName).HasColumnName("ShippingMethodName");
            this.Property(t => t.ShippingMethodDesc).HasColumnName("ShippingMethodDesc");
            this.Property(t => t.Notes).HasColumnName("Notes");
        }
    }
}
