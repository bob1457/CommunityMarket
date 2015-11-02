using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonMarket.Core.Entities;
using CommonMarket.Core.Interface;
using CommonMarket.Services.ProductServices;
using CommonMarket.Web.Models;
using ImageResizer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using CommonMarket.Infrastructure.Utilities;

namespace CommonMarket.Web.Controllers
{
    public class ProductController : BaseController
    {
        #region Identity impelementation

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

        //private static int counter;

        private readonly ICategoryService _categoryService;
        private readonly IProductServices _productServices;
        private readonly IMerchantService _merchantService;
        private readonly IDiscountService _discountService;
        //private readonly ICartService _cartService;

        public ProductController(ICategoryService categoryService, IProductServices productServices, 
            IMerchantService merchantService, IDiscountService discountService)
        {
            _categoryService = categoryService;
            _productServices = productServices;
            _merchantService = merchantService;
            _discountService = discountService;
            //_cartService = cartService;
        }

      
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductDetails(int id)
        {
            ViewBag.Productid = id;

            return View();
        }

        [ChildActionOnly]
        public ActionResult GetProductDetails(int id)
        {
            var product = _productServices.FindProductById(id);

            ViewBag.SupplierId = product.SupplierId;

            return PartialView("_ProductInfo", product);
        }

        [ChildActionOnly]
        public ActionResult GetProductImages(int id) //product id
        {


            return PartialView("_ProductImages");
        }
        

        [ChildActionOnly]
        public ActionResult GetVendorInfo(int id) //supplier id
        {
            var vendor = _merchantService.FindSupplierById(id);

            int profileId = vendor.UserProfileId;

            var vendorProfile = UserManager.Users.Single(p => p.UserProfile.Id == profileId);


            return PartialView("_VenderInfo", vendorProfile);
        }

        [ChildActionOnly]
        public ActionResult GetVendorImg(int id)
        {
            var vendor = _merchantService.FindSupplierById(id);

            int profileId = vendor.UserProfileId;

            var vendorProfile = UserManager.Users.Single(p => p.UserProfile.Id == profileId);


            return PartialView("_VenderImg", vendorProfile);
        }

        [ChildActionOnly]
        public ActionResult GetVendorInfo2(int id) //supplier id
        {
            var vendor = _merchantService.FindSupplierById(id);

            int profileId = vendor.UserProfileId;

            var vendorProfile = UserManager.Users.Single(p => p.UserProfile.Id == profileId);


            return PartialView("_VenderInfo2", vendorProfile);
        }
        //[ChildActionOnly]
        //public ActionResult GetCartView()
        //{
        //    var cart = _cartService.Lines;

        //    return PartialView("_CartView", cart);
        //}

       



        //public JsonResult CategoryList()
        //{
        //    var db = new CommonMarketEntities(); //this implementation is violation of layered architecture, will fix later.

        //    var result = from category in db.ProductCategories
        //        select new
        //        {
        //            id = category.Id,
        //            name = category.ProductCategoryName
        //        };            
            
        //    return Json(result.ToList(), JsonRequestBehavior.AllowGet);

        //}

        public JsonResult GetGategoryList()
        {
            var result = _categoryService.FindAllCategories();

            return Json(result.ToList(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetProductBySupplierId(int id)
        {

            return PartialView("");
        }

        public JsonResult GetSupplierByProductId(int id)
        {
            

            return Json("");
        }

        [HttpPost]
        public void AddProduct(Product product, Core.Entities.ProductCategory category)
        {
            var newProduct = new Product();
            

            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            var prfileId = currentUser.UserProfile.Id;
            var supplierId = _merchantService.FindSupplierBy(prfileId).Id;

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                newProduct.ProductName = product.ProductName;
                newProduct.ProductDescLong = product.ProductDescLong;
                newProduct.ProductDescShort = product.ProductDescShort;
                newProduct.ProductImgLargeUrl = "/Content/Assets/Images/product_default_lg.png";
                newProduct.ProductImgSmallUrl = "/Content/Assets/Images/product_default_sm.png";
                newProduct.UnitName = product.UnitName;
                newProduct.Notes = product.Notes;
                newProduct.UnitsInStock = 0;//Not implemented
                newProduct.UnitPrice = product.UnitPrice;
                newProduct.SupplierId = supplierId;
                newProduct.ProductAvailable = product.ProductAvailable;
                newProduct.CreateDate = DateTime.Now;
                newProduct.UpdateDate = DateTime.Now;


                _productServices.AddNewProduct(newProduct, category);
            }

        }

        [HttpPost]
        public ActionResult FileUpload(int id)
        {
            HttpPostedFile myFile = System.Web.HttpContext.Current.Request.Files["UploadedImage"];// Request.Files["MyFile"];

            string fName = myFile.FileName;
            string fileExtenstion = FileProcessor.GetFileExtension(fName);

            string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("/Content/Assets/Images/Products/Originals"), Path.GetFileName(myFile.FileName));
            string purePath = System.Web.HttpContext.Current.Server.MapPath("/Content/Assets/Images/Products");


            var fileUpload = new FileUpload();

            fileUpload.Upload(myFile, path);



            #region Generate small and large image

            ResizeSettings settings_sm = new ResizeSettings();
            ResizeSettings settings_lg = new ResizeSettings();

            switch (fileExtenstion)
            {
                case ".png":
                    settings_sm = new ResizeSettings("width=60&height=60&crop=auto&format=png");
                    settings_lg = new ResizeSettings("width=400&height=300&crop=auto&format=png");
                    break;
                case ".jpg":
                    settings_sm = new ResizeSettings("width=60&height=60&crop=auto&format=jpg");
                    settings_lg = new ResizeSettings("width=400&height=300&crop=auto&format=jpg");
                    break;
                case ".jepg":
                    settings_sm = new ResizeSettings("width=60&height=60&crop=auto&format=jpg");
                    settings_lg = new ResizeSettings("width=400&height=300&crop=auto&format=jpg");
                    break;
                case ".gif":
                    settings_sm = new ResizeSettings("width=60&height=60&crop=auto&format=gif");
                    settings_lg = new ResizeSettings("width=400&height=300&crop=auto&format=gif");
                    break;
                default:
                    settings_sm = new ResizeSettings("width=60&height=60&crop=auto&format=jpg");
                    settings_lg = new ResizeSettings("width=400&height=3000&crop=auto&format=jpg");
                    break;

            }

            string fileName_sm = "product_" + id + "_sm";
            string fileName_lg = "product" + id + "_lg";

            string newFileName_sm = Path.Combine(purePath, fileName_sm);
            string newFileName_lg = Path.Combine(purePath, fileName_lg);

            ImageBuilder.Current.Build(myFile, newFileName_sm, settings_sm, false,
                  true);
            ImageBuilder.Current.Build(myFile, newFileName_lg, settings_lg, false,
               true);


            

            #endregion



            //Update image url in db
            var product = _productServices.FindProductById(id);

            product.ProductImgSmallUrl = "/Content/Assets/Images/products/" + fileName_sm + fileExtenstion;
            product.ProductImgLargeUrl = "/Content/Assets/Images/products/" + fileName_lg + fileExtenstion;

            _productServices.UpdateProductImg(product);

            return Json("Image has been updated!");
        }

        [HttpPost]
        public ActionResult AdditionalImageUploade(int id) //id is product Id
        {
            HttpPostedFile myFile = System.Web.HttpContext.Current.Request.Files["UploadedImage"];// Request.Files["MyFile"];

            string fName = myFile.FileName;
            string fileExtenstion = FileProcessor.GetFileExtension(fName);

            string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("/Content/Assets/Images/Products/Originals"), Path.GetFileName(myFile.FileName));
            string purePath = System.Web.HttpContext.Current.Server.MapPath("/Content/Assets/Images/Products");


            var fileUpload = new FileUpload();

            fileUpload.Upload(myFile, path);

            #region Generate small and large image

            ResizeSettings settings_sm = new ResizeSettings();
            ResizeSettings settings_lg = new ResizeSettings();

            switch (fileExtenstion)
            {
                case ".png":
                    settings_sm = new ResizeSettings("width=60&height=60&crop=auto&format=png");
                    settings_lg = new ResizeSettings("width=400&height=300&crop=auto&format=png");
                    break;
                case ".jpg":
                    settings_sm = new ResizeSettings("width=60&height=60&crop=auto&format=jpg");
                    settings_lg = new ResizeSettings("width=400&height=300&crop=auto&format=jpg");
                    break;
                case ".jepg":
                    settings_sm = new ResizeSettings("width=60&height=60&crop=auto&format=jpg");
                    settings_lg = new ResizeSettings("width=400&height=300&crop=auto&format=jpg");
                    break;
                case ".gif":
                    settings_sm = new ResizeSettings("width=60&height=60&crop=auto&format=gif");
                    settings_lg = new ResizeSettings("width=400&height=300&crop=auto&format=gif");
                    break;
                default:
                    settings_sm = new ResizeSettings("width=60&height=60&crop=auto&format=jpg");
                    settings_lg = new ResizeSettings("width=400&height=3000&crop=auto&format=jpg");
                    break;

            }

            //counter = counter + 1;

            Guid guid = Guid.NewGuid();
            
            string fileName_addi_sm = "product_" + id + "addi_sm_" + guid;
            string fileName_addi_lg = "product" + id + "addi_lg_" + guid;

            string fileUrl_sm = "/Content/Assets/Images/products/" + fileName_addi_sm + fileExtenstion;
            string fileUrl_lg = "/Content/Assets/Images/products/" + fileName_addi_lg + fileExtenstion;

            string newFileName_sm = Path.Combine(purePath, fileName_addi_sm);
            string newFileName_lg = Path.Combine(purePath, fileName_addi_lg);

            ImageBuilder.Current.Build(myFile, newFileName_sm, settings_sm, false,
                  true);
            ImageBuilder.Current.Build(myFile, newFileName_lg, settings_lg, false,
               true);

            #endregion

            //Add to database
            AddProductImages(id, fileUrl_sm, fileUrl_lg);

            return Json("Image loaded successfully!");
        }

        private void AddProductImages(int id, string fileNameSm, string fileNameLg)
        {
            var Image = new AdditionalProductImg();

            Image.AdditionalImgLargeUrl = fileNameLg;
            Image.AdditionalImgSmallUrl = fileNameSm;
            Image.ProductId = id;

            _productServices.AddAdditionalImage(Image);

        }

        //[ChildActionOnly]
        public ActionResult GetAdditionalImages(int id) //id: product id
        {
            var images = _productServices.GetImgListByProduct(id);

            //return Json(images.ToList(), JsonRequestBehavior.AllowGet);
            return PartialView("_ProductImages", images);
        }

        public ActionResult ListAdditionalImag(int id) //product id
        {
            var imgList = _productServices.GetImgListByProduct(id);

            return PartialView("_AdditionalImagList", imgList);
        }

        [HttpPost]
        public void UpdateProduct(Product product)
        {
            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            var prfileId = currentUser.UserProfile.Id;
            var supplierId = _merchantService.FindSupplierBy(prfileId).Id;

            //product.ProductImgLargeUrl = "";
            //product.ProductImgSmallUrl = "";
            product.SupplierId = supplierId;

            //var updatedProduct = _productServices.FindProductById(product.Id);
            
            _productServices.UpdateProduct(product);
        }

        [HttpPost]
        public void UpdateProductImg(Product product)
        {
            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            var prfileId = currentUser.UserProfile.Id;
            var supplierId = _merchantService.FindSupplierBy(prfileId).Id;

            //product.ProductImgLargeUrl = "";
            //product.ProductImgSmallUrl = "";
            product.SupplierId = supplierId;

            //var updatedProduct = _productServices.FindProductById(product.Id);

            _productServices.UpdateProductImg(product);
        }


        [HttpPost]
        public void AddCommentsOnProduct(Comment comment)
        {
            var newComment = new Comment();

            try
            {
                //if (ModelState.IsValid)
                //{
                    newComment.DomainEntityId = 1;
                    newComment.CommentedBy = User.Identity.GetUserId();
                    newComment.CommentBody = comment.CommentBody;
                    newComment.EntityRecordId = comment.EntityRecordId;
                    newComment.CreateDate = DateTime.Now;

                    _productServices.AddCommentsOnProduct(newComment);
                //}
                
            }
            catch (Exception)
            {
                
                throw;
            }
            
            
        }


        [HttpPost]
        public void AddCommentsOnSupplier(Comment comment)
        {
            var newComment = new Comment();

            try
            {
                //if (ModelState.IsValid)
                //{
                newComment.DomainEntityId = 2;
                newComment.CommentedBy = User.Identity.GetUserId();
                newComment.CommentBody = comment.CommentBody;
                newComment.EntityRecordId = comment.EntityRecordId;
                newComment.CreateDate = DateTime.Now;

                _productServices.AddCommentsOnSupplier(newComment);
                //}

            }
            catch (Exception)
            {

                throw;
            }


        }



        public ActionResult GetUserInfo(string id)
        {
            var user = UserManager.FindById(id);

            int profileId = user.UserProfile.Id;

            var userProfile = UserManager.Users.Single(p => p.UserProfile.Id == profileId);

            return PartialView("_CommentBy", userProfile);
        }

        public ActionResult DisplayCommentsOnProducts(int id) //id: product id
        {
            var comments = _productServices.GetComments(id);

            return PartialView("_CommentsOnProduct", comments);
        }


        public ActionResult DisplayCommentsOnSupplier(int id) //id: supplier id
        {
            var comments = _productServices.GetComments(id);

            return PartialView("_CommentsOnProduct", comments);
        }



        #region Product Discount and Promotion

        public JsonResult DiscountTypeList()
        {
            var allTypes = _discountService.GetAllTypeList();

            return Json(allTypes.ToList(), JsonRequestBehavior.AllowGet);
        }


        #endregion
    }
}