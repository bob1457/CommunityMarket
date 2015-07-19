using CommonMarket.Core.Entities;


namespace CommonMarket.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CommunityMarketContext : DbContext, IDbContext
    {
        public CommunityMarketContext()
            : base("name=CommunityMarketContext")
        {
            //this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<AdditionalProductImg> AdditionalProductImgs { get; set; }
        public virtual DbSet<CategoryProduct> CategoryProducts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<DiscountType> DiscountTypes { get; set; }
        public virtual DbSet<DomainEntity> DomainEntities { get; set; }
        public virtual DbSet<Fee> Fees { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderFee> OrderFees { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<OrderStatu> OrderStatus { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public virtual DbSet<ProductTax> ProductTaxes { get; set; }
        public virtual DbSet<ProductWishList> ProductWishLists { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<Shipping> Shippings { get; set; }
        public virtual DbSet<ShippingMethod> ShippingMethods { get; set; }
        public virtual DbSet<ShoppingList> ShoppingLists { get; set; }
        public virtual DbSet<ShoppingListStatu> ShoppingListStatus { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<SupplierCategory> SupplierCategories { get; set; }
        public virtual DbSet<SupplierProduct> SupplierProducts { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<WishList> WishLists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.ShoppingLists)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.ProductCategories)
                .WithRequired(e => e.Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Discount>()
                .Property(e => e.VoucherCode)
                .IsUnicode(false);

            modelBuilder.Entity<Discount>()
                .Property(e => e.Notes)
                .IsFixedLength();

            modelBuilder.Entity<Discount>()
                .HasMany(e => e.ProductDiscounts)
                .WithRequired(e => e.Discount)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DiscountType>()
                .Property(e => e.DiscountTypeName)
                .IsFixedLength();

            modelBuilder.Entity<DiscountType>()
                .HasMany(e => e.Discounts)
                .WithRequired(e => e.DiscountType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DomainEntity>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.DomainEntity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DomainEntity>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.DomainEntity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Fee>()
                .Property(e => e.FeeAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Fee>()
                .HasMany(e => e.OrderFees)
                .WithRequired(e => e.Fee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invoice>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Invoice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invoice>()
                .HasMany(e => e.Payments)
                .WithRequired(e => e.Invoice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.TotalAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderItems)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderFees)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Shippings)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderItem>()
                .Property(e => e.SubTotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderStatu>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.OrderStatu)
                .HasForeignKey(e => e.OrderStatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment>()
                .Property(e => e.PaymentAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PaymentMethod>()
                .HasMany(e => e.Payments)
                .WithRequired(e => e.PaymentMethod)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.AdditionalProductImgs)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.CategoryProducts)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderItems)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductDiscounts)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductTaxes)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.CategoryProducts)
                .WithRequired(e => e.ProductCategory)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.SupplierCategories)
                .WithRequired(e => e.ProductCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shipper>()
                .HasMany(e => e.Shippings)
                .WithRequired(e => e.Shipper)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ShippingMethod>()
                .HasMany(e => e.Shippings)
                .WithRequired(e => e.ShippingMethod)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ShoppingList>()
                .Property(e => e.ShoppingListDesc)
                .IsFixedLength();

            modelBuilder.Entity<ShoppingListStatu>()
                .HasMany(e => e.ShoppingLists)
                .WithRequired(e => e.ShoppingListStatu)
                .HasForeignKey(e => e.ShoppingListStatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.OrderItems)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.SupplierCategories)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tax>()
                .Property(e => e.TaxRate)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Tax>()
                .HasMany(e => e.ProductTaxes)
                .WithRequired(e => e.Tax)
                .WillCascadeOnDelete(false);
        }
    }
}
