using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using CommonMarket.DataAccess.Models.Mapping;



namespace CommonMarket.DataAccess.Models
{
    public partial class CommonMarketContext : DbContext
    {
        static CommonMarketContext()
        {
            Database.SetInitializer<CommonMarketContext>(null);
        }

        public CommonMarketContext()
            : base("Name=CommonMarketContext")
        {
        }

        public DbSet<AdditionalProductImg> AdditionalProductImgs { get; set; }
        public DbSet<AppUserProfile> AppUserProfiles { get; set; }
        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<DiscountType> DiscountTypes { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatu> OrderStatus { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductFee> ProductFees { get; set; }
        public DbSet<ProductTax> ProductTaxes { get; set; }
        public DbSet<ProductWishList> ProductWishLists { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingListStatu> ShoppingListStatus { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierProduct> SupplierProducts { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<WishList> WishLists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AdditionalProductImgMap());
            modelBuilder.Configurations.Add(new AppUserProfileMap());
            modelBuilder.Configurations.Add(new AspNetRoleMap());
            modelBuilder.Configurations.Add(new AspNetUserClaimMap());
            modelBuilder.Configurations.Add(new AspNetUserLoginMap());
            modelBuilder.Configurations.Add(new AspNetUserMap());
            modelBuilder.Configurations.Add(new CatalogMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new DiscountMap());
            modelBuilder.Configurations.Add(new DiscountTypeMap());
            modelBuilder.Configurations.Add(new FeeMap());
            modelBuilder.Configurations.Add(new InvoiceMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new OrderDetailMap());
            modelBuilder.Configurations.Add(new OrderStatuMap());
            modelBuilder.Configurations.Add(new PaymentMap());
            modelBuilder.Configurations.Add(new PaymentMethodMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductCategoryMap());
            modelBuilder.Configurations.Add(new ProductFeeMap());
            modelBuilder.Configurations.Add(new ProductTaxMap());
            modelBuilder.Configurations.Add(new ProductWishListMap());
            modelBuilder.Configurations.Add(new ShippingMap());
            modelBuilder.Configurations.Add(new ShippingMethodMap());
            modelBuilder.Configurations.Add(new ShoppingListMap());
            modelBuilder.Configurations.Add(new ShoppingListStatuMap());
            modelBuilder.Configurations.Add(new SupplierMap());
            modelBuilder.Configurations.Add(new SupplierProductMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new TaxMap());
            modelBuilder.Configurations.Add(new WishListMap());
        }
    }
}
