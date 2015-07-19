namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShippingMethod")]
    public partial class ShippingMethod
    {
        public ShippingMethod()
        {
            Shippings = new HashSet<Shipping>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ShippingMethodName { get; set; }

        [StringLength(255)]
        public string ShippingMethodDesc { get; set; }

        public bool IsAvailable { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public virtual ICollection<Shipping> Shippings { get; set; }
    }
}
