using CommonMarket.Core.Entities;

namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderItem")]
    public partial class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int SupplierId { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal SubTotal { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
