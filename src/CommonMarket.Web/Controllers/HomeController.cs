using System.Web.Mvc;
using System.Web;
using System.Collections.Generic;
using CommonMarket.Core.Entities;
using CommonMarket.Core.Interface;
using CommonMarket.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using CommonMarket.Web.Models;
using MvcSiteMapProvider.Linq;

namespace CommonMarket.Web.Controllers
{
    public class HomeController : BaseController
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
            IEnumerable<Core.Entities.ProductCategory> allCategories = _categoryService.FindAllCategories();

            return PartialView("_CategoryList", allCategories);
        }

        //[ChildActionOnly]
        public ActionResult GetSuppliers()
        {
            var supplier = UserManager.Users.Where(m => m.Roles.Any(r => r.RoleId == "6ca46ec2-a996-4788-92ec-5c255a174eb4"));
            //var supplier = RoleManager.FindByName("Merchant").Users;
            return PartialView("_SupplierList", supplier);
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
