using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class WishListMap : EntityTypeConfiguration<WishList>
    {
        public WishListMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("WishList");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.PriorityId).HasColumnName("PriorityId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
        }
    }
}
