namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Fee")]
    public partial class Fee
    {
        public Fee()
        {
            OrderFees = new HashSet<OrderFee>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string FeeName { get; set; }

        [Column(TypeName = "money")]
        public decimal FeeAmount { get; set; }

        [StringLength(255)]
        public string FeeDesc { get; set; }

        public virtual ICollection<OrderFee> OrderFees { get; set; }
    }
}
