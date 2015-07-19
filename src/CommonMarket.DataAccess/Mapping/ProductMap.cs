using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SKU)
                .HasMaxLength(50);

            this.Property(t => t.VendorProductId)
                .HasMaxLength(50);

            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ProductDescShort)
                .HasMaxLength(100);

            this.Property(t => t.ProductDescLong)
                .HasMaxLength(255);

            this.Property(t => t.ProductImgUrl)
                .HasMaxLength(150);

            this.Property(t => t.UnitName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Notes)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Product");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SKU).HasColumnName("SKU");
            this.Property(t => t.VendorProductId).HasColumnName("VendorProductId");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.ProductDescShort).HasColumnName("ProductDescShort");
            this.Property(t => t.ProductDescLong).HasColumnName("ProductDescLong");
            this.Property(t => t.ProductImgUrl).HasColumnName("ProductImgUrl");
            this.Property(t => t.SupplierId).HasColumnName("SupplierId");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");
            this.Property(t => t.UnitName).HasColumnName("UnitName");
            this.Property(t => t.QuantityPerUnit).HasColumnName("QuantityPerUnit");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.DiscountId).HasColumnName("DiscountId");
            this.Property(t => t.UnitsInStock).HasColumnName("UnitsInStock");
            this.Property(t => t.ProductAvailable).HasColumnName("ProductAvailable");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.OrderCutoffDate).HasColumnName("OrderCutoffDate");

            // Relationships
            this.HasRequired(t => t.Discount)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.DiscountId);
            this.HasRequired(t => t.ProductCategory)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.CategoryId);

        }
    }
}
