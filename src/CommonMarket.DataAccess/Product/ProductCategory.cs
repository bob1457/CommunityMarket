namespace CommonMarket.DataAccess.Product
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductCategory")]
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            CategoryProducts = new HashSet<CategoryProduct>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductCategoryName { get; set; }

        [StringLength(255)]
        public string ProductCategoryImgLargeUrl { get; set; }

        [StringLength(250)]
        public string ProductCategoryImgSamllUrl { get; set; }

        [StringLength(255)]
        public string ProductCategoryDesc { get; set; }

        public int DepartmentId { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime UpdateDate { get; set; }

        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}
