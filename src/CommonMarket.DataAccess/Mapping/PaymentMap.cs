using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class PaymentMap : EntityTypeConfiguration<Payment>
    {
        public PaymentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Payment");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PaymentMethodId).HasColumnName("PaymentMethodId");
            this.Property(t => t.OrderId).HasColumnName("OrderId");
            this.Property(t => t.PaymentDate).HasColumnName("PaymentDate");
            this.Property(t => t.PaymentAmount).HasColumnName("PaymentAmount");

            // Relationships
            this.HasRequired(t => t.Order)
                .WithMany(t => t.Payments)
                .HasForeignKey(d => d.OrderId);
            this.HasRequired(t => t.PaymentMethod)
                .WithMany(t => t.Payments)
                .HasForeignKey(d => d.PaymentMethodId);

        }
    }
}
