namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Order")]
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
            Shippings = new HashSet<Shipping>();
            Fees = new HashSet<Fee>();
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime OrderSubmitDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime OrderUpdateDate { get; set; }

        public int OrderStatusId { get; set; }

        public bool IsPaid { get; set; }

        public bool IsCanceled { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        public virtual Customer Customer { get; set; }

        //public virtual Invoice Invoice { get; set; }

        public virtual OrderStatu OrderStatu { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual ICollection<Shipping> Shippings { get; set; }

        public virtual ICollection<Fee> Fees { get; set; }
    }
}
