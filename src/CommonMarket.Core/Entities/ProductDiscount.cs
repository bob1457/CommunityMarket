namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductDiscount")]
    public partial class ProductDiscount
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int DiscountId { get; set; }

        public virtual Discount Discount { get; set; }

        public virtual Product Product { get; set; }
    }
}
