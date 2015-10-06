using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonMarket.Core.Entities;
using CommonMarket.Core.Interface;
using CommonMarket.Web.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


namespace CommonMarket.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerOrderService _customerOrderService;

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

        public CustomerController(ICustomerService customerService, ICustomerOrderService customerOrderService)
        {
            _customerService = customerService;
            _customerOrderService = customerOrderService;
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

            if (customer != null)
            {
                return PartialView("_CustomerDetails", customer);
            }
            else
            {
                var currentUser = UserManager.FindById(id);
                var fName = currentUser.UserProfile.FirstName;
                var lName = currentUser.UserProfile.LastName;
                var alias = fName + " " + lName;
                var email = currentUser.UserProfile.Email;

                AddCustomer(alias, email, profileId);

                var newCustomer = _customerService.FindCustomerBy(profileId);

                return PartialView("_CustomerDetails", newCustomer);

            }
            //return Json(customer, JsonRequestBehavior.AllowGet);
            
        }

        private void AddCustomer(string customerAlias, string email, int profileId)
        {
            var customer = new Customer();

            if (ModelState.IsValid)
            {
                customer.CustomerAlias = customerAlias;
                customer.UserProfileId = profileId;
                customer.IsActive = true;
                customer.ContactEmail = email;

                _customerService.AddCustomer(customer);
            }
        }


        public ActionResult GetPurcahseHisotryForCustomer()
        {
            var profileId = UserManager.FindById(User.Identity.GetUserId()).UserProfile.Id;

            var customer = _customerService.FindCustomerBy(profileId);

            var orders = _customerOrderService.GetOrderByCustomer(customer.Id);

            return PartialView("_OrderByCustomer", orders);
        }
    }
}