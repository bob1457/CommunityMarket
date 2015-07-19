namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
            OrderFees = new HashSet<OrderFee>();
            Shippings = new HashSet<Shipping>();
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime OrderSubmitDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime OrderUpdateDate { get; set; }

        public int OrderStatusId { get; set; }

        public bool IsPaid { get; set; }

        public int InvoiceId { get; set; }

        public bool IsCanceled { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual OrderStatu OrderStatu { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual ICollection<OrderFee> OrderFees { get; set; }

        public virtual ICollection<Shipping> Shippings { get; set; }
    }
}
