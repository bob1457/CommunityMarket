using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Order");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.SupplierId).HasColumnName("SupplierId");
            this.Property(t => t.OrderSubmitDate).HasColumnName("OrderSubmitDate");
            this.Property(t => t.OrderUpdateDate).HasColumnName("OrderUpdateDate");
            this.Property(t => t.OrderStatusId).HasColumnName("OrderStatusId");
            this.Property(t => t.Paid).HasColumnName("Paid");
            this.Property(t => t.PaidAmount).HasColumnName("PaidAmount");
            this.Property(t => t.PaymentMethodId).HasColumnName("PaymentMethodId");
            this.Property(t => t.PaymentDate).HasColumnName("PaymentDate");
            this.Property(t => t.ShippingDate).HasColumnName("ShippingDate");
            this.Property(t => t.ShippingMethodId).HasColumnName("ShippingMethodId");
            this.Property(t => t.InvoiceId).HasColumnName("InvoiceId");

            // Relationships
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.CustomerId);
            this.HasRequired(t => t.Invoice)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.InvoiceId);
            this.HasRequired(t => t.OrderStatu)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.OrderStatusId);
            this.HasRequired(t => t.OrderStatu1)
                .WithMany(t => t.Orders1)
                .HasForeignKey(d => d.OrderStatusId);
            this.HasRequired(t => t.PaymentMethod)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.PaymentMethodId);
            this.HasRequired(t => t.ShippingMethod)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.ShippingMethodId);
            this.HasRequired(t => t.Supplier)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.SupplierId);

        }
    }
}
