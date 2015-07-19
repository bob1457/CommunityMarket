using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class PaymentMethodMap : EntityTypeConfiguration<PaymentMethod>
    {
        public PaymentMethodMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.PaymentMethodName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PaymentMethodDesc)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("PaymentMethod");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PaymentMethodName).HasColumnName("PaymentMethodName");
            this.Property(t => t.PaymentMethodDesc).HasColumnName("PaymentMethodDesc");
        }
    }
}
