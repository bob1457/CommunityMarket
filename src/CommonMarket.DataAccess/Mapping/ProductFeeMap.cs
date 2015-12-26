using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class ProductFeeMap : EntityTypeConfiguration<ProductFee>
    {
        public ProductFeeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProductFee");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.FeeId).HasColumnName("FeeId");

            // Relationships
            this.HasRequired(t => t.Fee)
                .WithMany(t => t.ProductFees)
                .HasForeignKey(d => d.FeeId);
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ProductFees)
                .HasForeignKey(d => d.ProductId);

        }
    }
}
