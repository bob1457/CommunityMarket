using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonMarket.Core.Entities;
using CommonMarket.Core.Interface;
using CommonMarket.Services.ProductServices;
using CommonMarket.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CommonMarket.Web.Controllers
{
    public class ProductController : BaseController
    {
        #region Identity impelementation

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
        private readonly IProductServices _productServices;
        private readonly IMerchantService _merchantService;

        public ProductController(ICategoryService categoryService, IProductServices productServices, IMerchantService merchantService)
        {
            _categoryService = categoryService;
            _productServices = productServices;
            _merchantService = merchantService;
        }


        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductDetails(int id)
        {

            return View();
        }



        public JsonResult CategoryList()
        {
            var db = new CommonMarketEntities(); //this implementation is violation of layered architecture, will fix later.

            var result = from category in db.ProductCategories
                select new
                {
                    id = category.Id,
                    name = category.ProductCategoryName
                };            
            
            return Json(result.ToList(), JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public void AddProduct(Product product, Core.Entities.ProductCategory category)
        {
            var newProduct = new Product();
            

            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            var prfileId = currentUser.UserProfile.Id;
            var supplierId = _merchantService.FindSupplierBy(prfileId).Id;

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                newProduct.ProductName = product.ProductName;
                newProduct.ProductDescLong = product.ProductDescLong;
                newProduct.ProductDescShort = product.ProductDescShort;
                newProduct.ProductImgLargeUrl = "/Content/Assets/Images/product_default_lg.png";
                newProduct.ProductImgSmallUrl = "/Content/Assets/Images/product_default_sm.png";
                newProduct.UnitName = product.UnitName;
                newProduct.Notes = product.Notes;
                newProduct.UnitsInStock = 0;//Not implemented
                newProduct.UnitPrice = product.UnitPrice;
                newProduct.SupplierId = supplierId;
                newProduct.ProductAvailable = product.ProductAvailable;
                newProduct.CreateDate = DateTime.Now;
                newProduct.UpdateDate = DateTime.Now;


                _productServices.AddNewProduct(newProduct, category);
            }

        }
    }
}