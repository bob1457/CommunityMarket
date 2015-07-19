using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class SupplierMap : EntityTypeConfiguration<Supplier>
    {
        public SupplierMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CompnayName)
                .HasMaxLength(50);

            this.Property(t => t.ContactFirstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CompanyLogoImgUrl)
                .HasMaxLength(255);

            this.Property(t => t.CompanyIconImgUrl)
                .HasMaxLength(255);

            this.Property(t => t.ContactLastName)
                .HasMaxLength(50);

            this.Property(t => t.ShortBio)
                .HasMaxLength(400);

            this.Property(t => t.SupplierWebSite)
                .HasMaxLength(150);

            this.Property(t => t.Notes)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Supplier");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompnayName).HasColumnName("CompnayName");
            this.Property(t => t.ContactFirstName).HasColumnName("ContactFirstName");
            this.Property(t => t.CompanyLogoImgUrl).HasColumnName("CompanyLogoImgUrl");
            this.Property(t => t.CompanyIconImgUrl).HasColumnName("CompanyIconImgUrl");
            this.Property(t => t.ContactLastName).HasColumnName("ContactLastName");
            this.Property(t => t.ShortBio).HasColumnName("ShortBio");
            this.Property(t => t.SupplierWebSite).HasColumnName("SupplierWebSite");
            this.Property(t => t.ProductCategoryId).HasColumnName("ProductCategoryId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Notes).HasColumnName("Notes");
        }
    }
}
