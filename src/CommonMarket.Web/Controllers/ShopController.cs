using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonMarket.Core.Interface;
using CommonMarket.Services.ProductServices;
using CommonMarket.Web.Models;
using Microsoft.AspNet.Identity.Owin;
using ProductCategory = CommonMarket.Core.Entities.ProductCategory;

namespace CommonMarket.Web.Controllers
{
    public class ShopController : BaseController
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


        private readonly ICategoryService _categoryService;
        private readonly IMerchantService _merchantServie;
        private readonly IProductServices _productServices;

        public ShopController(ICategoryService categoryService, IMerchantService merchantServie, IProductServices productServices)
        {
            _categoryService = categoryService;
            _merchantServie = merchantServie;
            _productServices = productServices;
        }

        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }


        //[ChildActionOnly]
        public ActionResult GetCategoryList()
        {
            IEnumerable<ProductCategory> allCategories = _categoryService.FindAllCategories();

            return PartialView("_CategoryList", allCategories);
        }

        [ChildActionOnly]
        public ActionResult GetFeaturedProducts() //Actually newest products
        {
            var products = _productServices.FindAllProducts().OrderByDescending(d => d.CreateDate).Take(12); //get latest or newest products (4)

            return PartialView("_FeaturedList", products);
        }

        
        public ActionResult GetProductListByCategory(int id)
        {
            var products = _productServices.FindProductByCategory(id);

            return PartialView("_FeaturedList", products);
        }


        public ActionResult GetAllSuppliers()
        {
            var supplier = UserManager.Users.Where(m => m.Roles.Any(r => r.RoleId == "6ca46ec2-a996-4788-92ec-5c255a174eb4"));
            
            return PartialView("_SupplierList", supplier);
        }


        public JsonResult GetSpecialProducts() //get products on promotion with lower price
        {

            return Json("");
        }

    }
}