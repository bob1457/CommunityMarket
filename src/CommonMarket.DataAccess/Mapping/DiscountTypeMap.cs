using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class DiscountTypeMap : EntityTypeConfiguration<DiscountType>
    {
        public DiscountTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DiscountTypeName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("DiscountType");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DiscountTypeName).HasColumnName("DiscountTypeName");
            this.Property(t => t.DiscountAmount).HasColumnName("DiscountAmount");
        }
    }
}
