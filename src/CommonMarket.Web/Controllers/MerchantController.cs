using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using CommonMarket.Core.Interface;
using CommonMarket.DataAccess;
using CommonMarket.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CommonMarket.Web.Controllers
{
    [Authorize]
    public class MerchantController : BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IMerchantService _merchantServie;

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

        public MerchantController(ICustomerService customerService, IMerchantService merchantServie)
        {
            _customerService = customerService;
            _merchantServie = merchantServie;
        }


        // GET: Merchant
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetMerchantDetails(string id)
        {
            //CommunityMarketContext db = new CommunityMarketContext();

            var profileId = UserManager.FindById(id).UserProfile.Id;

            var merchant = _merchantServie.FindSupplierBy(profileId);

            //string query = "SELECT * FROM Supplier WHERE UserProfileId = @p0";

            //var merchant = db.Suppliers.SqlQuery(query, profileId).SingleOrDefault();

            //return PartialView("_MerchantDetails", merchant);
            return Json(merchant, JsonRequestBehavior.AllowGet);
        }
    }
}