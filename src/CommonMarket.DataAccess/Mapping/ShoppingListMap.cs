using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class ShoppingListMap : EntityTypeConfiguration<ShoppingList>
    {
        public ShoppingListMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ShoppingListTitle)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.ShoppingListDesc)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("ShoppingList");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ShoppingListTitle).HasColumnName("ShoppingListTitle");
            this.Property(t => t.ShoppingListDesc).HasColumnName("ShoppingListDesc");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.ShoppingListStatusId).HasColumnName("ShoppingListStatusId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");

            // Relationships
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.ShoppingLists)
                .HasForeignKey(d => d.CustomerId);
            this.HasRequired(t => t.ShoppingListStatu)
                .WithMany(t => t.ShoppingLists)
                .HasForeignKey(d => d.ShoppingListStatusId);

        }
    }
}
