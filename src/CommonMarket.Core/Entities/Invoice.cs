namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Invoice")]
    public partial class Invoice
    {
        public Invoice()
        {
            Orders = new HashSet<Order>();
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; }

        public int OrderId { get; set; }

        public bool IsPaid { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime InoviceDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
