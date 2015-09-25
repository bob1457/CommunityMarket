namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Discount")]
    public partial class Discount
    {
        public Discount()
        {
            Products = new HashSet<Product>();
            Coupons = new HashSet<Coupon>();
        }

        public int Id { get; set; }

        [StringLength(450)]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int DiscountTypeId { get; set; }

        public int DiscountValue { get; set; }

        public int ValueType { get; set; }

        public int? ValueThreshold { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime UpdateDate { get; set; }

        [StringLength(10)]
        public string Notes { get; set; }

        public virtual DiscountType DiscountType { get; set; }

        public virtual ICollection<Coupon> Coupons { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
