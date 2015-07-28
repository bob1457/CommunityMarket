using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonMarket.Core.Interface;
using CommonMarket.Services.ProductServices;
using CommonMarket.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;

namespace CommonMarket.Web.Controllers
{
    [Authorize]
    public class OperateController : BaseController
    {

        #region Identity implementation

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        #endregion

        private readonly IProductServices _productServices;
        private readonly IMerchantService _merchantService;

        public OperateController(IProductServices productServices, IMerchantService merchantService )
        {
            _productServices = productServices;
            _merchantService = merchantService;
        }


        // GET: Operate
        public ActionResult Index()
        {
            //var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            //int profileId = 


            return View();
        }


        [ChildActionOnly]
        public ActionResult GetProductList(int? page)
        {
            const int pageSize = 10; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);

            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            var prfileId = currentUser.UserProfile.Id;
            var supplierId = _merchantService.FindSupplierBy(prfileId).Id;

            var allProducts = _productServices.FindAllProducts().OrderByDescending(d => d.CreateDate).Where(s => s.SupplierId == supplierId && s.ProductAvailable == true);

            int TotalProductCount = allProducts.Count();

            ViewBag.ProductCount = TotalProductCount;

            var products = allProducts.ToPagedList(pageNumber, pageSize);

            return PartialView("_AllProducts", products);
        }


        public ActionResult ListProducts(int? page)
        {
            const int pageSize = 10; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);


            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            var prfileId = currentUser.UserProfile.Id;
            var supplierId = _merchantService.FindSupplierBy(prfileId).Id;


            var allProducts = _productServices.FindAllProducts().OrderByDescending(d => d.CreateDate).Where(s => s.SupplierId == supplierId && s.ProductAvailable == true);

            var products = allProducts.ToPagedList(pageNumber, pageSize);

            return PartialView("_AllProducts", products);
        }
    }
}