using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonMarket.Core.Interface;
using Microsoft.AspNet.Identity;

namespace CommonMarket.Web.Controllers
{
    [Authorize]
    public class OperateController : BaseController
    {
        private readonly IMerchantService _merchantService;

        public OperateController(IMerchantService merchantService)
        {
            _merchantService = merchantService;
        }


        // GET: Operate
        public ActionResult Index()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            //int profileId = 


            return View();
        }
    }
}