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

        public ActionResult GetShippingAddress(int id)
        {
            var shippingAddressByCustomerid = _customerService.FindCustomerAddress(id, 2);

            return PartialView("_ShippingAddress", shippingAddressByCustomerid);
        }

        public ActionResult GetBillingAddress(int id)
        {
            var billingAddressByCustomerid = _customerService.FindCustomerAddress(id, 1);

            return PartialView("_BillingAddress", billingAddressByCustomerid);
        }


        public ActionResult GetShippingAddrForEdit(int id)
        {
            var shippingAddressByCustomerid = _customerService.FindCustomerAddress(id, 2);

            return PartialView("_ShippingAddressEdit", shippingAddressByCustomerid);
        }


        public ActionResult GetBillingAddrForEdit(int id)
        {
            var billingAddressByCustomerid = _customerService.FindCustomerAddress(id, 2);

            return PartialView("_BillingAddressEdit", billingAddressByCustomerid);
        }


        public ActionResult GetCustomerEdit(string id)
        {
            var profileId = UserManager.FindById(id).UserProfile.Id;

            var customer = _customerService.FindCustomerBy(profileId);

            if (customer != null)
            {
                return PartialView("_CustomerEdit", customer);
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

                return PartialView("_CustomerEdit", newCustomer);

            }
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

        public void UpdateCustomerInfo(Customer customer, CustomerAddress billingAddress, CustomerAddress shippingAddress)
        {
            try
            {
                var profileId = UserManager.FindById(User.Identity.GetUserId()).UserProfile.Id;

                var custo = _customerService.FindCustomerBy(profileId);

                custo.CustomerAlias = customer.CustomerAlias;
                custo.BillingAddress = customer.BillingAddress;
                custo.ShippingAddress = customer.ShippingAddress;
                custo.ContactEmail = customer.ContactEmail;
                custo.ContactTel = customer.ContactTel;

                //_customerService.UpdateCustomerInfo(custo);

                //new code: customer address
                var billingAddr = _customerService.FindCustomerAddress(custo.Id, 1); //get billing address
                var shippingAddr = _customerService.FindCustomerAddress(custo.Id, 2); //get shipping address

                if (billingAddr != null)
                {
                    billingAddr.AddressStreet = billingAddress.AddressStreet;
                    billingAddr.AddressCity = billingAddress.AddressCity;
                    billingAddr.AddressProState = billingAddress.AddressProState;
                    billingAddr.AddressPostZipCode = billingAddress.AddressPostZipCode;
                    billingAddr.AddressType = billingAddress.AddressType;
                    billingAddr.CustomerId = customer.Id;
                    billingAddress.AddressCountry = "Canada";
                }

                if (shippingAddr != null)
                {
                    shippingAddr.AddressStreet = shippingAddress.AddressStreet;
                    shippingAddr.AddressCity = shippingAddress.AddressCity;
                    shippingAddr.AddressProState = shippingAddress.AddressProState;
                    shippingAddr.AddressPostZipCode = shippingAddress.AddressPostZipCode;
                    shippingAddr.AddressType = shippingAddress.AddressType;
                    shippingAddr.CustomerId = customer.Id;
                    shippingAddr.AddressCountry = "Canada";
                }
                

                _customerService.UpdateCustomerInfo(custo, billingAddress, shippingAddress);
            }
            catch (Exception)
            {
                
                throw;
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