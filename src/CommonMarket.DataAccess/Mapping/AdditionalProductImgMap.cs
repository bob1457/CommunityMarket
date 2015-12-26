using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class AdditionalProductImgMap : EntityTypeConfiguration<AdditionalProductImg>
    {
        public AdditionalProductImgMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.AdditionalImgUrl)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.ImgDesc)
                .HasMaxLength(450);

            // Table & Column Mappings
            this.ToTable("AdditionalProductImgs");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AdditionalImgUrl).HasColumnName("AdditionalImgUrl");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.ImgDesc).HasColumnName("ImgDesc");
        }
    }
}
