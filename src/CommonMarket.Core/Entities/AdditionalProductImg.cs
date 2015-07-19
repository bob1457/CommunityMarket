namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdditionalProductImg
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string AdditionalImgLargeUrl { get; set; }

        [StringLength(250)]
        public string AdditionalImgSmallUrl { get; set; }

        public int ProductId { get; set; }

        [StringLength(450)]
        public string ImgDesc { get; set; }

        public virtual Product Product { get; set; }
    }
}
