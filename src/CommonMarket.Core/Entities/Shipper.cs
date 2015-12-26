namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Shipper")]
    public partial class Shipper
    {
        public Shipper()
        {
            Shippings = new HashSet<Shipping>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ShipperName { get; set; }

        [StringLength(250)]
        public string ShipperDesc { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime UpdateDate { get; set; }

        public virtual ICollection<Shipping> Shippings { get; set; }
    }
}
