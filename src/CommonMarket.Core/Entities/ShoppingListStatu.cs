namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ShoppingListStatu
    {
        public ShoppingListStatu()
        {
            ShoppingLists = new HashSet<ShoppingList>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ListStatus { get; set; }

        public virtual ICollection<ShoppingList> ShoppingLists { get; set; }
    }
}
