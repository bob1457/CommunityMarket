using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class ShoppingListStatuMap : EntityTypeConfiguration<ShoppingListStatu>
    {
        public ShoppingListStatuMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ListStatus)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ShoppingListStatus");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ListStatus).HasColumnName("ListStatus");
        }
    }
}
