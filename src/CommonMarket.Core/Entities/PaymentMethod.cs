namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PaymentMethod")]
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethodName { get; set; }

        [StringLength(255)]
        public string PaymentMethodDesc { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
