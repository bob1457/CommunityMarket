using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class FeeMap : EntityTypeConfiguration<Fee>
    {
        public FeeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FeeName)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.FeeDesc)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Fee");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FeeName).HasColumnName("FeeName");
            this.Property(t => t.FeeAmount).HasColumnName("FeeAmount");
            this.Property(t => t.FeeDesc).HasColumnName("FeeDesc");
        }
    }
}
