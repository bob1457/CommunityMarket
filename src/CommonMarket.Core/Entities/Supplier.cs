namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Supplier")]
    public partial class Supplier
    {
        public Supplier()
        {
            OrderItems = new HashSet<OrderItem>();
            Products = new HashSet<Product>();
            ProductCategories = new HashSet<ProductCategory>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string CompnayName { get; set; }

        [Required]
        [StringLength(50)]
        public string ContactFirstName { get; set; }

        [StringLength(255)]
        public string CompanyLogoImgUrl { get; set; }

        [StringLength(255)]
        public string CompanyIconImgUrl { get; set; }

        [StringLength(50)]
        public string ContactLastName { get; set; }

        [StringLength(400)]
        public string ShortBio { get; set; }

        [StringLength(150)]
        public string SupplierWebSite { get; set; }

        public int UserProfileId { get; set; }

        public bool IsActive { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
