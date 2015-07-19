using System.Web.Mvc;
using System.Collections.Generic;

using CommonMarket.Core.Entities;
using CommonMarket.Core.Interface;
using CommonMarket.Services;

namespace CommonMarket.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMerchantService _merchantServie;

        public HomeController(ICategoryService categoryService, IMerchantService merchantServie)
        {
            _categoryService = categoryService;
            _merchantServie = merchantServie;
        }



        public ActionResult Index()
        {
            //var allCategories = _categoryService.FindAllCategories();

            return View(); //("_CategoryList", allCategories);
        }

        [ChildActionOnly]
        public ActionResult GetCategoryList()
        {
            IEnumerable<ProductCategory> allCategories = _categoryService.FindAllCategories();

            return PartialView("_CategoryList", allCategories);
        }




















        //[Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test() //Testing error page layout
        {
            return View();
        }
    }
}
