using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class ProductCategoryMap : EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ProductCategoryName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ProductCategoryImgUrl)
                .HasMaxLength(255);

            this.Property(t => t.ProductCategoryDesc)
                .HasMaxLength(255);

            this.Property(t => t.Notes)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ProductCategory");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProductCategoryName).HasColumnName("ProductCategoryName");
            this.Property(t => t.ProductCategoryImgUrl).HasColumnName("ProductCategoryImgUrl");
            this.Property(t => t.ProductCategoryDesc).HasColumnName("ProductCategoryDesc");
            this.Property(t => t.Notes).HasColumnName("Notes");
        }
    }
}
