namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Discount")]
    public partial class Discount
    {
        public Discount()
        {
            ProductDiscounts = new HashSet<ProductDiscount>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string VoucherCode { get; set; }

        public bool IsActive { get; set; }

        public int DiscountTypeId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        [StringLength(10)]
        public string Notes { get; set; }

        public virtual DiscountType DiscountType { get; set; }

        public virtual ICollection<ProductDiscount> ProductDiscounts { get; set; }
    }
}
