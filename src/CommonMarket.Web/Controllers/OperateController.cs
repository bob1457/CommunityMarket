using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonMarket.Core.Interface;
using CommonMarket.Services.ProductServices;
using Microsoft.AspNet.Identity;
using PagedList;

namespace CommonMarket.Web.Controllers
{
    [Authorize]
    public class OperateController : BaseController
    {
        private readonly IProductServices _productServices;
        private readonly IMerchantService _merchantService;

        public OperateController(IProductServices productServices)
        {
            _productServices = productServices;
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

            var allProducts = _productServices.FindAllProducts().OrderByDescending(d => d.CreateDate);

            var products = allProducts.ToPagedList(pageNumber, pageSize);

            return PartialView("_AllProducts", products);
        }


        public ActionResult ListProducts(int? page)
        {
            const int pageSize = 10; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);

            var allProducts = _productServices.FindAllProducts().OrderByDescending(d => d.CreateDate);

            var products = allProducts.ToPagedList(pageNumber, pageSize);

            return PartialView("_AllProducts", products);
        }
    }
}