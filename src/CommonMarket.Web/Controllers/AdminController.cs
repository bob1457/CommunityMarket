using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Web.HttpContext;
using CommonMarket.Core.Entities;
//using CommonMarket.core.Entities;
using CommonMarket.Core.Interface;
using CommonMarket.Web.Models;
using ImageResizer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using CommonMarket.Infrastructure.Utilities;
//using CommonMarket.Infrastructure.Utilities;
using PagedList;

namespace CommonMarket.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
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

        private readonly ICategoryService _categoryService;
        private readonly ICustomerService _customerService;
        private readonly IMerchantService _merchantServie;
        private readonly IDepartmentService _departmentService;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IPromotionService _promotionService;


        public AdminController(ICategoryService categoryService, ICustomerService customerService, 
            IMerchantService merchantServie, IDepartmentService departmentService, 
            IOrderProcessingService orderProcessingService, IPromotionService promotionService )
        {
            _categoryService = categoryService;
            _customerService = customerService;
            _merchantServie = merchantServie;
            _departmentService = departmentService;
            _orderProcessingService = orderProcessingService;
            _promotionService = promotionService;
        }

        #region Home page with partial view
        //

        //Home page
        public ActionResult Index()
        {
            return View();
        }

        
        //Users and Roles
        [ChildActionOnly]
        public ActionResult GetUserList(int? page)
        {
            const int pageSize = 10; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);

            var users = UserManager.Users.OrderByDescending((d => d.UserProfile.CreateDate)).ToPagedList(pageNumber, pageSize);

            return PartialView("_UserList", users);
        }
        
        [ChildActionOnly]
        public ActionResult GetRoleList()
        {
            var roles = RoleManager.Roles;
            return PartialView("_RoleList", roles);
        }



        public ActionResult GetSuppliers(int? page) //Testing
        {
            //var suppliers = RoleManager.FindByName("Merchant").Users;

            //var merchant = from role in RoleManager.Roles
            //    where role.Name == "Merchant"
            //    from user in role.Users
            //    select user.UserId;

            const int pageSize = 10; //for testing purpose, to be adjustetd, PAGING NOT IMPLEMENTED YET
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);
            

            var supplier = UserManager.Users.Where(m => m.Roles.Any(r => r.RoleId == "6ca46ec2-a996-4788-92ec-5c255a174eb4")).OrderByDescending(d=>d.UserProfile.LastName).ToPagedList(pageNumber, pageSize);

            return PartialView("_SupplierList", supplier);
        }
        
        //Ajax operations
        //
        public ActionResult GetAllRoles()
        {
            var roles = RoleManager.Roles;

            return PartialView("_RoleList", roles);//Json(roles.ToList(), JsonRequestBehavior.AllowGet); //
        }

        #endregion

        #region Category


        //[ChildActionOnly]  //Create another method for ajax paging, leave this as it was
        public ActionResult GetCategoryList(int? page)
        {
            const int pageSize = 10; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);

            IEnumerable<Core.Entities.ProductCategory> allCategories = _categoryService.FindAllCategories().OrderBy(d => d.CreateDate);

            ViewBag.CategoryCount = allCategories.Count();

            var categories = allCategories.ToPagedList(pageNumber, pageSize);

            //if(Request.IsAjaxRequest())
            //    return PartialView()

            return PartialView("_AllCategories", categories);
        }

        //public JsonResult CategoryList()
        //{
            
        //    var category = _categoryService.FindAllCategories();
            
        //    return Json(category, JsonRequestBehavior.AllowGet);
        //}



        [HttpPost]
        public void AddCategory(Core.Entities.ProductCategory category)
        {
            var newCategory = new Core.Entities.ProductCategory();

            if (ModelState.IsValid)
            {
                newCategory.ProductCategoryName = category.ProductCategoryName;
                newCategory.ProductCategoryDesc = category.ProductCategoryDesc;
                newCategory.ProductCategoryImgSamllUrl = "/Content/Assets/Images/category_default_sm.png";
                newCategory.ProductCategoryImgLargeUrl = "/Content/Assets/Images/category_default_lg.png";
                newCategory.Notes = "";
                newCategory.DepartmentId = 1;
                newCategory.CreateDate = DateTime.Now;
                newCategory.UpdateDate = DateTime.Now;

                _categoryService.AddNewCategory(newCategory);
            }
            

        }


        [HttpPost]
        public void UpdateCategory(Core.Entities.ProductCategory category)
        {
            //ProductCategory categoryForUpdate = _categoryService.FindCategoryById(category.Id);

            //categoryForUpdate.ProductCategoryName = category.ProductCategoryName;
            //categoryForUpdate.ProductCategoryDesc = category.ProductCategoryDesc;
            //categoryForUpdate.UpdateDate = DateTime.Now;

            

            _categoryService.UpdateCategory(category);

        }

        public ActionResult ListCategories(int? page)
        {
            const int pageSize = 10; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);

            var allCategories = _categoryService.FindAllCategories().OrderBy(d => d.CreateDate);

            var categoryList = allCategories.ToPagedList(pageNumber, pageSize);

            return PartialView("_AllCategories", categoryList);
        }




        #endregion

        #region Department

        [ChildActionOnly]
        public ActionResult GetDepartmentList(int? page)
        {
            const int pageSize = 10; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);

            IEnumerable<Department> allDepartments =
                _departmentService.FindAllDepartments().OrderByDescending(n => n.DepartmentName);

            var departments = allDepartments.ToPagedList(pageNumber, pageSize);

            return PartialView("_AllDepartments", departments);
        }

        #endregion

        #region Order Fullfillment

        public ActionResult AllOrderList(int? page)
        {

            const int pageSize = 10; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);

            var allOrders = _orderProcessingService.GetAllOrders();//.ToPagedList(pageNumber, pageSize);

            var pagedList = allOrders.ToPagedList(pageNumber, pageSize);

            return PartialView("_AllOrderList", pagedList);
        }


        public ActionResult GetAllOrderItems(int id) //id: order id
        {
            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            
            IEnumerable<Core.Entities.OrderItem> orderItems = _orderProcessingService.GetOrderItemsFromOrder(id);
            
            decimal total = 0;

            foreach (var item in orderItems)
            {
                //if (item.Product.DiscountId != null)
                //{
                //    int? dId = item.Product.DiscountId;
                //    var discount = GetDiscountById(dId);

                //    if (discount.ValueType == 0)
                //    {
                //        decimal rate = 1 - Convert.ToDecimal(discount.DiscountValue / 100.00);

                //        ViewBag.dicountAmt = rate;

                //        var newPrice = item.Product.UnitPrice * rate;
                //        total = total + newPrice * item.Quantity;
                //    }
                //    else
                //    {
                //        //Deal with flat amount discount
                //    }
                //}
                //else
                //{
                //    total = total + item.Product.UnitPrice * item.Quantity;
                //}

                total = total + item.Product.UnitPrice * item.Quantity;

                ViewBag.Total = total;

            }

            return PartialView("_AllOrderItems", orderItems);
        }


        #endregion
        
        #region Helper
        /**/
        [HttpPost]
        public ActionResult FileUpload(int id)
        {
            HttpPostedFile myFile = System.Web.HttpContext.Current.Request.Files["UploadedImage"];// Request.Files["MyFile"];

            string fName = myFile.FileName;
            string fileExtenstion = FileProcessor.GetFileExtension(fName);

            string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("/Content/Assets/Images"), Path.GetFileName(myFile.FileName));
            string purePath = System.Web.HttpContext.Current.Server.MapPath("/Content/Assets/Images");


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

            string fileName_sm = "category_" + id + "_sm";
            string fileName_lg = "category_" + id + "_lg";

            string newFileName_sm = Path.Combine(purePath, fileName_sm);
            string newFileName_lg = Path.Combine(purePath, fileName_lg);

            ImageBuilder.Current.Build(myFile, newFileName_sm, settings_sm, false,
                  true);
            ImageBuilder.Current.Build(myFile, newFileName_lg, settings_lg, false,
               true);


            //int fileDim_sm_x = 60;
            //int fileDim_sm_y = 60;

            //string fileName_sm = "category_"  + id + "_sm" + fileExtenstion;

            

            //Image img = Image.FromFile(path);


            //if (img.Width > fileDim_sm_x || img.Height > fileDim_sm_y)
            //{
            //    //resize image
            //    //

            //    ImageProcessor.SaveResizedImage(purePath, fName, fileName_sm, fileDim_sm_x, fileDim_sm_y);
            //}

            #endregion

            

            //Update image url in db
            var category = _categoryService.FindCategoryById(id);

            category.ProductCategoryImgSamllUrl = "/Content/Assets/Images/" + fileName_sm + fileExtenstion;
            category.ProductCategoryImgLargeUrl = "/Content/Assets/Images/" + fileName_lg + fileExtenstion;

            _categoryService.UpdateCategory(category);

            return Json("Image has been updated!");
        }

        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {

                    result = false;
                }
            }
            return result;
        }

        #endregion

        #region Merchant Service

        public JsonResult MerchantStatus(string id) //user id
        {
           var profileId = UserManager.FindById(id).UserProfile.Id;

            var suppler = _merchantServie.FindSupplierById(profileId);

            bool isActive = suppler.IsActive;

            return Json(isActive);
        }


        public JsonResult ActivateMerchantAccount(string id) // id = user id 
        {

            //Check if supplier account exists, if not, create an account
            //
            var profileId = UserManager.FindById(id).UserProfile.Id;
            var email = UserManager.FindById(id).UserName; //find user login name (in email) by userid

            var supplier = _merchantServie.FindSupplierById(profileId);

            if (supplier != null)
            {
                supplier.IsActive = true;
                supplier.UserProfileId = profileId;
                supplier.CompanyIconImgUrl = email; //user this field to pass the username(email) to supplier information table

                _merchantServie.UpdateSupplier(supplier.Id);
            }
            else
            {
                var newSupplier = new Supplier();

                if (ModelState.IsValid)
                {
                    newSupplier.ContactFirstName = UserManager.FindById(id).UserProfile.FirstName;
                    newSupplier.ContactLastName = UserManager.FindById(id).UserProfile.LastName;
                    newSupplier.IsActive = true;
                    newSupplier.UserProfileId = profileId;
                    
                    _merchantServie.AddSupplier(newSupplier);
                }

                
            }

            //Make account active
            //



            return Json(id + " Activated!");
        }

        public JsonResult DeActivateMerchantAccount(string id) // id = user id 
        {

           

            //Make account inactive
            //



            return Json(id + " Deactivated!");
        }

        public ActionResult GetTransactionsBySupplier(string id) //id: supplier userid
        {
            var userInfo = UserManager.FindById(id);
            var profileId = userInfo.UserProfile.Id;

            var supplier = _merchantServie.FindSupplierBy(profileId);

            var transactions = _orderProcessingService.GetOrderItemssbyVendor(supplier.Id);

            return PartialView("_TransactionsByVendor", transactions);
        }



        #endregion

        #region Customer Service

        [HttpPost]
        public void AddCustomer(string customerAlias)
        {
            var customer = new Customer();

            if (ModelState.IsValid)
            {
                customer.CustomerAlias = customerAlias;
                customer.IsActive = true;
                _customerService.AddCustomer(customer);
            }
        }

        #endregion

        private Discount GetDiscountById(int? id)
        {
            return _promotionService.FindAllDiscounts().FirstOrDefault(d => d.Id == id);//.ToList();

        }

    }
}