using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class CatalogMap : EntityTypeConfiguration<Catalog>
    {
        public CatalogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CatalogTitle)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.CatelogShortDec)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.CatelogLongDesc)
                .HasMaxLength(450);

            // Table & Column Mappings
            this.ToTable("Catalog");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CatalogTitle).HasColumnName("CatalogTitle");
            this.Property(t => t.CatelogShortDec).HasColumnName("CatelogShortDec");
            this.Property(t => t.CatelogLongDesc).HasColumnName("CatelogLongDesc");
            this.Property(t => t.SupplierId).HasColumnName("SupplierId");
            this.Property(t => t.IsPublished).HasColumnName("IsPublished");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");

            // Relationships
            this.HasRequired(t => t.Supplier)
                .WithMany(t => t.Catalogs)
                .HasForeignKey(d => d.SupplierId);

        }
    }
}
