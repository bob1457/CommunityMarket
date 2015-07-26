using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonMarket.Core.Interface;
using CommonMarket.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CommonMarket.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

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

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCustomerDetails(string id) //user id in ASP.NET Idnetity system
        {
            var profileId = UserManager.FindById(id).UserProfile.Id;

            var customer = _customerService.FindCustomerBy(profileId);

            return Json(customer, JsonRequestBehavior.AllowGet);
        }
    }
}