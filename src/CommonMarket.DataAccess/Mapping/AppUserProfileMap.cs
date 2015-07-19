using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CommonMarket.DataAccess.Models.Mapping
{
    public class AppUserProfileMap : EntityTypeConfiguration<AppUserProfile>
    {
        public AppUserProfileMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("AppUserProfiles");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.ProvState).HasColumnName("ProvState");
            this.Property(t => t.Address1).HasColumnName("Address1");
            this.Property(t => t.Address2).HasColumnName("Address2");
            this.Property(t => t.PostZipCode).HasColumnName("PostZipCode");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.AvatarImgUrl).HasColumnName("AvatarImgUrl");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
        }
    }
}
