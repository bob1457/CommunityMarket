namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
            ShoppingLists = new HashSet<ShoppingList>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string CustomerAlias { get; set; }

        public int UserProfileId { get; set; }

        [StringLength(255)]
        public string BillingAddress { get; set; }

        [StringLength(255)]
        public string ShippingAddress { get; set; }

        public bool IsActive { get; set; }

        public bool IsPreferred { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<ShoppingList> ShoppingLists { get; set; }
    }
}
