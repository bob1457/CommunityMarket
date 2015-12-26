namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
            ProductCategories = new HashSet<ProductCategory>();
            //Discounts = new HashSet<Discount>();
            Taxes = new HashSet<Tax>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string SKU { get; set; }

        [StringLength(50)]
        public string VendorProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [StringLength(100)]
        public string ProductDescShort { get; set; }

        [StringLength(255)]
        public string ProductDescLong { get; set; }

        [StringLength(250)]
        public string ProductImgLargeUrl { get; set; }

        [StringLength(250)]
        public string ProductImgSmallUrl { get; set; }

        public int SupplierId { get; set; }

        [Required]
        [StringLength(50)]
        public string UnitName { get; set; }

        public int? QuantityPerUnit { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public short UnitsInStock { get; set; }

        public bool ProductAvailable { get; set; }

        public int? DiscountId { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime UpdateDate { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }

        //public virtual ICollection<Discount> Discounts { get; set; }

        public virtual ICollection<Tax> Taxes { get; set; }
    }
}
