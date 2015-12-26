namespace CommonMarket.DataAccess.Product
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AppUserProfile
    {
        public AppUserProfile()
        {
            Suppliers = new HashSet<Supplier>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string ProvState { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string PostZipCode { get; set; }

        public string Country { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string AvatarImgUrl { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
