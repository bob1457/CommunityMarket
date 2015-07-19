using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CommonMarket.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //Add here custom properties for user profile purpose, such as address, telephone, and other applicaiton specific attributes/properties
        //
        //e.g.:

        //public string Address { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }

        //// Use a sensible display name for views:
        //[Display(Name = "Postal Code")]
        //public string PostalCode { get; set; }

        //// Concatenate the address info for display in tables and such:
        //public string DisplayAddress
        //{
        //    get
        //    {
        //        string dspAddress =
        //            string.IsNullOrWhiteSpace(this.Address) ? "" : this.Address;
        //        string dspCity =
        //            string.IsNullOrWhiteSpace(this.City) ? "" : this.City;
        //        string dspState =
        //            string.IsNullOrWhiteSpace(this.State) ? "" : this.State;
        //        string dspPostalCode =
        //            string.IsNullOrWhiteSpace(this.PostalCode) ? "" : this.PostalCode;

        //        return string
        //            .Format("{0} {1} {2} {3}", dspAddress, dspCity, dspState, dspPostalCode);
        //    }
        //}

        public virtual AppUserProfile UserProfile { get; set; }

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        //{
        //    var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
        //    // Add custom user claims here
        //    return userIdentity;
        //}

    }



    public class AppUserProfile
    {
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
    }

    //Custom role implementation - plugged in here if necessary, note, there are a few files/locations need to be updated for this custom role
    //
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }
        public string Description { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("CommonMarketConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<AppUserProfile> UserProfileInfo { get; set; }


        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}