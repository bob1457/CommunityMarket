using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class SupplierProductMap : EntityTypeConfiguration<SupplierProduct>
    {
        public SupplierProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SupplierProduct");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SupplierId).HasColumnName("SupplierId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");

            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.SupplierProducts)
                .HasForeignKey(d => d.ProductId);
            this.HasOptional(t => t.Supplier)
                .WithMany(t => t.SupplierProducts)
                .HasForeignKey(d => d.SupplierId);

        }
    }
}
