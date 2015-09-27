namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payment")]
    public partial class Payment
    {
        public int Id { get; set; }

        public int PaymentMethodId { get; set; }

        public int OrderId { get; set; }

        //[Column(TypeName = "datetime2")]
        //public DateTime PaymentDate { get; set; }

        [Column(TypeName = "money")]
        public decimal PaymentAmount { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PaymentDate { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
