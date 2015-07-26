using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CommonMarket.Core.Interface;
using CommonMarket.Core.Product;
using CommonMarket.DataAccess;
using CommonMarket.Services.ProductServices;
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
            kernel.Bind<IProductRepository>().To<ProductRepository>();
        }
    }
}