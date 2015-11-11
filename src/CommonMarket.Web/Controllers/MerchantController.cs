using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonMarket.Core.Entities;
using CommonMarket.Core.Interface;
using CommonMarket.Infrastructure.Utilities;
using CommonMarket.Web.Models;
using ImageResizer;
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
        private readonly IOrderProcessingService _orderProcessingService;

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

        public MerchantController(ICustomerService customerService, IMerchantService merchantServie, 
            IProductServices productServices, IOrderProcessingService orderProcessingService)
        {
            _customerService = customerService;
            _merchantServie = merchantServie;
            _productServices = productServices;
            _orderProcessingService = orderProcessingService;
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

            return PartialView("_MerchantDetails", merchant);
            //return Json(merchant, JsonRequestBehavior.AllowGet);
        }

        public string GetMerchantBio(int id)
        {
            var merchant = _merchantServie.FindSupplierById(id);

            return merchant.ShortBio;
        }


        public ActionResult GetMerchantEdit(string id)
        {
            var profileId = UserManager.FindById(id).UserProfile.Id;

            //var currentUser = UserManager.FindById(User.Identity.GetUserId());

            var merchant = _merchantServie.FindSupplierBy(profileId);


            return PartialView("_MerchantEdit", merchant);
        }

        public ActionResult MerchantDetails(string id) //load to MerchantDetails page here id is userId in asp.net identity db
        {
            //var merchant = UserManager.Users.Where(p => p.UserProfile.Id == id);

            ViewBag.ProfileId = id;

            //Get supplierId

            ViewBag.uId = id;

            var profileId = UserManager.FindById(id).UserProfile.Id;

            int supplierId = _merchantServie.FindSupplierBy(profileId).Id;

            ViewBag.SupplierId = supplierId;

            return View();
            //return PartialView("_MerchanteInfo", merchant);
        }


        public ActionResult GetMerchantHeadInfo(string id)
        {
            var profileId = UserManager.FindById(id).UserProfile.Id;

            var supplier = _merchantServie.FindSupplierBy(profileId);

            return PartialView("_MerchantStoreHead", supplier);
        }


        //Receive Ajax call
        [HttpPost]
        public void UpdateMerchantInfo(Supplier supplier)
        {
            var profileId = UserManager.FindById(User.Identity.GetUserId()).UserProfile.Id;

            var merchant = _merchantServie.FindSupplierBy(profileId);

            merchant.ContactFirstName = supplier.ContactFirstName;
            merchant.ContactLastName = supplier.ContactLastName;
            merchant.CompnayName = supplier.CompnayName;
            merchant.CompanyIconImgUrl = supplier.CompanyIconImgUrl; //actually this one is used to store merchant's email
            merchant.SupplierWebSite = supplier.SupplierWebSite;
            merchant.ShortBio = supplier.ShortBio;

            if (ModelState.IsValid)
            {
                try
                {
                    _merchantServie.UpdateSupplierInfo(merchant);
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public void UploadLogoImg() //to be reviewd for file save location, etc.
        {
            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["UploadedImage"];

            var user = UserManager.FindById(User.Identity.GetUserId());

            if (file != null)
            {
                string fileName = file.FileName;
                string fileExtenstion = FileProcessor.GetFileExtension(fileName);


                if (file.ContentLength > 0 && file.ContentLength < 10 * 1024 * 1024)
                {
                    if (fileExtenstion == ".jpg" || fileExtenstion == ".jepg" || fileExtenstion == ".png" ||
                        fileExtenstion == ".gif")
                    {

                        string path = Path.Combine(Server.MapPath("~/Content/Assets/Images/Users/"), Path.GetFileName(file.FileName));

                        string picPath = Path.Combine("~/Content/Assets/Images/Users/", file.FileName);

                        //string extension = FileProcessor.GetFileExtension(file.FileName);

                        string purePath = Server.MapPath("~/Content/Assets/Images/Users/");

                        file.SaveAs(path);

                        //Image img = Image.FromFile(path);

                        string newFName = user.Id + "_logo";// + fileExtenstion;

                        string newFileName = Path.Combine(purePath, newFName);

                        //if (img.Width > 100 || img.Height > 100)
                        //{
                        //    //resize image
                        //    //

                        //    ImageProcessor.SaveResizedImage(purePath, fileName, newFileName, 100, 100);
                        //}

                        ResizeSettings settings = new ResizeSettings();

                        switch (fileExtenstion)
                        {
                            case ".png":
                                settings = new ResizeSettings("width=35&height=35&crop=auto&format=png");
                                break;
                            case ".jpg":
                                settings = new ResizeSettings("width=35&height=35&crop=auto&format=jpg");
                                break;
                            case ".jepg":
                                settings = new ResizeSettings("width=35&height=35&crop=auto&format=jpg");
                                break;
                            case ".gif":
                                settings = new ResizeSettings("width=35&height=35&crop=auto&format=gif");
                                break;
                            default:
                                settings = new ResizeSettings("width=35&height=35&crop=auto&format=jpg");
                                break;

                        }

                        ImageBuilder.Current.Build(file, newFileName, settings, false, true);
                        
                        //Update the database with the image Url

                        var newUrl = "~/Content/Assets/Images/Users/" + newFName + fileExtenstion;

                        var profileId = UserManager.FindById(user.Id).UserProfile.Id;

                        var supplier = _merchantServie.FindSupplierBy(profileId);

                        supplier.CompanyLogoImgUrl = newUrl;

                        _merchantServie.UpdateSupplierLogoUrl(supplier.Id, newUrl);

                    }
                    
                   

                }

                

                
            }
        }


        public void UpdateCustomerInfo(Customer customer)
        {
            
            
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

        public ActionResult GetSalesHisotryForSupplier()
        {
            var profileId = UserManager.FindById(User.Identity.GetUserId()).UserProfile.Id;

            var supplier = _merchantServie.FindSupplierBy(profileId);

            var orders = _orderProcessingService.GetOrderItemssbyVendor(supplier.Id); //GetOrdersByMerchant(supplier.Id);

            //if (orders != null)
            //{
            //    return PartialView("_OrderBySupplier", orders);
            //}
            //else
            //{
            //    return PartialView("");
            //}

            return PartialView("_OrderBySupplier", orders);
        }

    }
}