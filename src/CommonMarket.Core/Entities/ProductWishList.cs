namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductWishList")]
    public partial class ProductWishList
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int WishListId { get; set; }
    }
}
