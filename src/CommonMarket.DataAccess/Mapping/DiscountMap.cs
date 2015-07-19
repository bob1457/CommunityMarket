using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class DiscountMap : EntityTypeConfiguration<Discount>
    {
        public DiscountMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Notes)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Discount");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DiscountRate).HasColumnName("DiscountRate");
            this.Property(t => t.Notes).HasColumnName("Notes");
        }
    }
}
