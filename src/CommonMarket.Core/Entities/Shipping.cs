namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Shipping")]
    public partial class Shipping
    {
        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ShippingDate { get; set; }

        public int ShippingMethodId { get; set; }

        public int ShipperId { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public virtual Shipper Shipper { get; set; }

        public virtual ShippingMethod ShippingMethod { get; set; }
    }
}
