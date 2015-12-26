using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CustomerAlias)
                .HasMaxLength(50);

            this.Property(t => t.BillingAddress)
                .HasMaxLength(255);

            this.Property(t => t.ShippingAddress)
                .HasMaxLength(255);

            this.Property(t => t.Notes)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Customer");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CustomerAlias).HasColumnName("CustomerAlias");
            this.Property(t => t.BillingAddress).HasColumnName("BillingAddress");
            this.Property(t => t.ShippingAddress).HasColumnName("ShippingAddress");
            this.Property(t => t.Notes).HasColumnName("Notes");
        }
    }
}
