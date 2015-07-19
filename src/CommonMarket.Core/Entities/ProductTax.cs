namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductTax")]
    public partial class ProductTax
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int TaxId { get; set; }

        public virtual Product Product { get; set; }

        public virtual Tax Tax { get; set; }
    }
}
