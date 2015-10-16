using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CommonMarket.Core.Interface;
using CommonMarket.Core.Product;
using CommonMarket.DataAccess;
using CommonMarket.Services.MarketingServices;
using CommonMarket.Services.OrderServices;
using CommonMarket.Services.ProductServices;
using CommonMarket.Services.ShopServices;
using CommonMarket.Services.UserServices;
using Ninject;

using CommonMarket.DataAccess.Base;
using Ninject.Web.Common;


namespace CommonMarket.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            // put bindings here
            //kernel.Bind<IBaseRepository<Roles>().To<BasedRepository<Roles>();
            kernel.Bind<IValueCalculator>().To<ValueCalculator>(); //Test binding

            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IDbContext>().To<CommunityMarketContext>();
            kernel.Bind<ICategoryService>().To<CategoryService>();
            kernel.Bind<ICustomerService>().To<CustomerService>();
            kernel.Bind<IMerchantService>().To<MerchantService>();
            kernel.Bind<IProductServices>().To<ProductServices>();
            kernel.Bind <ICartService>().To<CartService>();
            kernel.Bind<ISearchSerivce>().To<SearchSerivce>();
            kernel.Bind<IDiscountService>().To<DiscountService>();
            kernel.Bind<ICheckOutService>().To<CheckOutService>();
            kernel.Bind<ICustomerOrderService>().To<CustomerOrderService>();
            kernel.Bind<IOrderProcessingService>().To<OrderProcessingService>();
            kernel.Bind<IPromotionService>().To<PromotionService>();

            kernel.Bind<IQuickSearchRepository>().To<QuickSearchRepository>();

            kernel.Bind<IDepartmentService>().To<DepartmentService>();
            kernel.Bind<IProductRepository>().To<ProductRepository>();
            kernel.Bind<ICartContentRepository>().To<CartContentRepository>();
            kernel.Bind<IOrderRepository>().To<OrderRepository>();
            kernel.Bind<IMerchantRepository>().To<MerchantRepository>();
            kernel.Bind<IOrderProcessingRepository>().To<OrderProcessingRepository>();
            kernel.Bind<IOrderByVendorRepository>().To<OrderByVendorRepository>();
            kernel.Bind<IPromotionRepository>().To<PromotionRepository>();

        }
    }
}