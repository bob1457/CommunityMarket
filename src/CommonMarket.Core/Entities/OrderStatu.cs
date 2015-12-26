namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderStatu
    {
        public OrderStatu()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderStatusName { get; set; }

        [StringLength(255)]
        public string OrderStatusDesc { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
