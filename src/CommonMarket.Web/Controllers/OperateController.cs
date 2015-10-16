using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using CommonMarket.Core.Entities;
using CommonMarket.Core.Interface;
using CommonMarket.Services.ProductServices;
using CommonMarket.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;

namespace CommonMarket.Web.Controllers
{
    [Authorize]
    public class OperateController : BaseController
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

        private readonly IProductServices _productServices;
        private readonly IMerchantService _merchantService;
        private readonly IOrderProcessingService _orderProcessingService;

        public OperateController(IProductServices productServices, IMerchantService merchantService, IOrderProcessingService orderProcessingService )
        {
            _productServices = productServices;
            _merchantService = merchantService;
            _orderProcessingService = orderProcessingService;
        }


        // GET: Operate
        public ActionResult Index()
        {
            //var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            //int profileId = 


            return View();
        }


        [ChildActionOnly]
        public ActionResult GetProductList(int? page)
        {
            const int pageSize = 10; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);

            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            var prfileId = currentUser.UserProfile.Id;
            var supplierId = _merchantService.FindSupplierBy(prfileId).Id;

            var allProducts = _productServices.FindAllProducts().OrderByDescending(d => d.CreateDate).Where(s => s.SupplierId == supplierId && s.ProductAvailable == true);

            int TotalProductCount = allProducts.Count();

            ViewBag.ProductCount = TotalProductCount;

            var products = allProducts.ToPagedList(pageNumber, pageSize);

            return PartialView("_AllProducts", products);
        }

        public ActionResult ListAdditionalImag(int id) //product id
        {
            var imgList = _productServices.GetImgListByProduct(id);

            ViewBag.pId = id;

            return PartialView("_AdditionalImagList", imgList);
        }

        public ActionResult ListProducts(int? page)
        {
            const int pageSize = 10; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);


            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            var prfileId = currentUser.UserProfile.Id;
            var supplierId = _merchantService.FindSupplierBy(prfileId).Id;

            ViewBag.supplierId = supplierId;

            var allProducts = _productServices.FindAllProducts().OrderByDescending(d => d.CreateDate).Where(s => s.SupplierId == supplierId 
                && s.ProductAvailable == true);

            var products = allProducts.ToPagedList(pageNumber, pageSize);

            return PartialView("_AllProducts", products);
        }

        public ActionResult GetAllOrders(int? page) //All order by supplier
        {
            const int pageSize = 10; //for testing purpose, to be adjustetd, PAGING NOT IMPLEMENTED YET
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);

            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            var prfileId = currentUser.UserProfile.Id;
            var supplierId = _merchantService.FindSupplierBy(prfileId).Id;

            ViewBag.SupplierId = supplierId;

            var allOrders = _orderProcessingService.OrderListBySupplier(supplierId).ToPagedList(pageNumber, pageSize);

            return PartialView("_AllOrders", allOrders);
        }

        //Ajax call coming GET
        public ActionResult GetOrderItemsByVendorOrder(int oid)
        {
            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            var prfileId = currentUser.UserProfile.Id;
            var supplierId = _merchantService.FindSupplierBy(prfileId).Id;

            IEnumerable<Core.Entities.OrderItem>  orderItems = _orderProcessingService.GetOrderItmesByVendorOrder(supplierId, oid);

            //var customer = _orderProcessingService.GetCustomerByOrderId(oid);

            //ViewBag.customerEmail = customer.ContactEmail;



            decimal total = 0;

            foreach (var item in orderItems)
            {
                total = total + item.Product.UnitPrice * item.Quantity;

                ViewBag.Total = total;

            }

            return PartialView("_OrderItems", orderItems);
        }


        public JsonResult GetOrderStatus()  //id = order id
        {

            var statusList = _orderProcessingService.GetStatusList();

            return Json(statusList.ToList(), JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public void UpdateOrderByVendor(int orderId, int statusId, bool isPaid)
        //{
        //    _orderProcessingService.UpdateOrderByVendor(orderId, statusId, isPaid);
        //}

        [HttpPost]
        public void UpdateOrderByVendor(OrderByVendor orderbyVendor)
        {
            orderbyVendor.UpdateDate = DateTime.Now;
            _orderProcessingService.UpdateOrderByVendor(orderbyVendor);
        }
    }
}