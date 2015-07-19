namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderFee")]
    public partial class OrderFee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int FeeId { get; set; }

        public virtual Fee Fee { get; set; }

        public virtual Order Order { get; set; }
    }
}
