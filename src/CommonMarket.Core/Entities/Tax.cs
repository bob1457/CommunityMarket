namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tax")]
    public partial class Tax
    {
        public Tax()
        {
            ProductTaxes = new HashSet<ProductTax>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string TaxName { get; set; }

        public decimal TaxRate { get; set; }

        [StringLength(255)]
        public string TaxDesc { get; set; }

        public virtual ICollection<ProductTax> ProductTaxes { get; set; }
    }
}
