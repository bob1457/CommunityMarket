using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonMarket.Core.Entities;
using CommonMarket.Core.Interface;

namespace CommonMarket.Web.Controllers
{
    public class ShopController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMerchantService _merchantServie;

        public ShopController(ICategoryService categoryService, IMerchantService merchantServie)
        {
            _categoryService = categoryService;
            _merchantServie = merchantServie;
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
    }
}