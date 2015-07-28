using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonMarket.Core.Entities;
using CommonMarket.Core.Interface;
using CommonMarket.Services.ProductServices;

namespace CommonMarket.Web.Controllers
{
    public class ShopController : BaseController
    {
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


        [ChildActionOnly]
        public ActionResult GetCategoryList()
        {
            IEnumerable<ProductCategory> allCategories = _categoryService.FindAllCategories();

            return PartialView("_CategoryList", allCategories);
        }

        [ChildActionOnly]
        public ActionResult GetFeaturedProducts() //Actually newest products
        {
            var products = _productServices.FindAllProducts().OrderByDescending(d => d.CreateDate).Take(8); //get latest or newest products (4)

            return PartialView("_FeaturedList", products);
        }
    }
}