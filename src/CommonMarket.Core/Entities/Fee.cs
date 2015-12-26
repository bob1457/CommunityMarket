namespace CommonMarket.Core.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Fee")]
    public partial class Fee
    {
        public Fee()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string FeeName { get; set; }

        [Column(TypeName = "money")]
        public decimal FeeAmount { get; set; }

        [StringLength(255)]
        public string FeeDesc { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
