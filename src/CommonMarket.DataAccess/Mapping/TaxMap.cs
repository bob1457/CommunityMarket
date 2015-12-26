using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class TaxMap : EntityTypeConfiguration<Tax>
    {
        public TaxMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TaxName)
                .HasMaxLength(50);

            this.Property(t => t.TaxDesc)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Tax");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TaxName).HasColumnName("TaxName");
            this.Property(t => t.TaxRate).HasColumnName("TaxRate");
            this.Property(t => t.TaxDesc).HasColumnName("TaxDesc");
        }
    }
}
