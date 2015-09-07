using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Web.UI.WebControls.WebParts;
using CommonMarket.Core.Interface;
using CommonMarket.DataAccess;
using CommonMarket.Services.ProductServices;
using CommonMarket.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CommonMarket.Web.Controllers
{


    //[Authorize]
    public class MerchantController : BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IMerchantService _merchantServie;
        private readonly IProductServices _productServices;

        #region Identity system

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

        public MerchantController(ICustomerService customerService, IMerchantService merchantServie, IProductServices productServices)
        {
            _customerService = customerService;
            _merchantServie = merchantServie;
            _productServices = productServices;
        }


        // GET: Merchant
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetMerchantDetails(string id) //to load to user profile page for editing
        {
            //CommunityMarketContext db = new CommunityMarketContext();

            var profileId = UserManager.FindById(id).UserProfile.Id;

            var merchant = _merchantServie.FindSupplierBy(profileId);

            //string query = "SELECT * FROM Supplier WHERE UserProfileId = @p0";

            //var merchant = db.Suppliers.SqlQuery(query, profileId).SingleOrDefault();

            //return PartialView("_MerchantDetails", merchant);
            return Json(merchant, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MerchantDetails(string id) //load to MerchantDetails page here id is userId in asp.net identity db
        {
            //var merchant = UserManager.Users.Where(p => p.UserProfile.Id == id);

            ViewBag.ProfileId = id;

            //Get supplierId

            var profileId = UserManager.FindById(id).UserProfile.Id;

            int supplierId = _merchantServie.FindSupplierBy(profileId).Id;

            ViewBag.SupplierId = supplierId;

            return View();
            //return PartialView("_MerchanteInfo", merchant);
        }

        [ChildActionOnly]
        public ActionResult GetMerchantInfo(string id)
        {
            var merchant = UserManager.FindById(id);

            return PartialView("_MerchantInfo", merchant);
        }

        [ChildActionOnly]
        public ActionResult GetProductListByMerchant(int id) //id = SupplierId
        {
            var products =
                _productServices.FindAllProducts().OrderByDescending(d => d.CreateDate).Where(s => s.SupplierId == id && s.ProductAvailable == true);

            //Get merchant idenytity userid

            var merchant = _merchantServie.FindSupplierById(id);

            var merchantProfilid = merchant.UserProfileId;

            var user = UserManager.FindByName(merchant.CompanyIconImgUrl);


            ViewBag.MerchantId = user.Id;

            return PartialView("_ProductInfo", products);
        }

    }
}