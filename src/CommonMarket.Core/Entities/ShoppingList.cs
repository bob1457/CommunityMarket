namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShoppingList")]
    public partial class ShoppingList
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string ShoppingListTitle { get; set; }

        [StringLength(10)]
        public string ShoppingListDesc { get; set; }

        public int CustomerId { get; set; }

        public int ShoppingListStatusId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime UpdateDate { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ShoppingListStatu ShoppingListStatu { get; set; }
    }
}
