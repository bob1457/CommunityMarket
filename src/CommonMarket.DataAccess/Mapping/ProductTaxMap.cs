using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class ProductTaxMap : EntityTypeConfiguration<ProductTax>
    {
        public ProductTaxMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductTax");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.TaxId).HasColumnName("TaxId");

            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ProductTaxes)
                .HasForeignKey(d => d.ProductId);
            this.HasRequired(t => t.Tax)
                .WithMany(t => t.ProductTaxes)
                .HasForeignKey(d => d.TaxId);

        }
    }
}
