namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DiscountType")]
    public partial class DiscountType
    {
        public DiscountType()
        {
            Discounts = new HashSet<Discount>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string DiscountTypeName { get; set; }
        
        public virtual ICollection<Discount> Discounts { get; set; }
    }
}
