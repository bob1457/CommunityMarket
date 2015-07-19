using System;
using System.IO;
using CommonMarket.Core.Interface;

using CommonMarket.Web.Models;
using ImageResizer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Drawing;

using CommonMarket.Infrastructure.Utilities;

namespace CommonMarket.Web.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IMerchantService _merchantServie;

        public ProfileController()
        {
        }
        
        public ProfileController(ApplicationUserManager userManager, ICustomerService customerService, IMerchantService merchantServie)
        {
            _customerService = customerService;
            _merchantServie = merchantServie;
            UserManager = userManager;
        }

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


        // GET: Profile
        public async Task<ActionResult> Index(ManageController.ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageController.ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageController.ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageController.ManageMessageId.SetTwoFactorSuccess ? "Your two factor provider has been set."
                : message == ManageController.ManageMessageId.Error ? "An error has occurred."
                : message == ManageController.ManageMessageId.AddPhoneSuccess ? "The phone number was added."
                : message == ManageController.ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());

            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(User.Identity.GetUserId()),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(User.Identity.GetUserId()),
                Logins = await UserManager.GetLoginsAsync(User.Identity.GetUserId()),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(User.Identity.GetUserId()),


                FirstName = currentUser.UserProfile.FirstName,
                LastName = currentUser.UserProfile.LastName,
                Email = currentUser.UserProfile.Email,
                Address1 = currentUser.UserProfile.Address1,
                City = currentUser.UserProfile.City,
                ProvState = currentUser.UserProfile.ProvState,
                PostZipCode = currentUser.UserProfile.PostZipCode,
                Country = currentUser.UserProfile.Country,
                Telephone = currentUser.UserProfile.Telephone,
                AvatarImgUrl = currentUser.UserProfile.AvatarImgUrl,

            };
            return View(model);
        }

        //Ajax change password
        public async Task<ActionResult> PasswordChange(string oldPass, string newPass)
        {

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), oldPass, newPass);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return Json("The password has been successfully changed!");
            }
            AddErrors(result);
            return Json("Error occured, please try it agian!");
        }

        //Ajax Edit Profile
        [HttpPost]
        public async Task<JsonResult> Update(string Id, string FirstName, string LastName, string Address, string City, string Province, string PostalCode, string Country, string Email, string Telephone)
        {
            if (ModelState.IsValid)
            {
                var user = await  UserManager.FindByIdAsync(Id);
                if (user == null)
                {
                    return Json("User not found!");
                }



                user.UserProfile.Email = Email;
                user.UserProfile.FirstName = FirstName;
                user.UserProfile.LastName = LastName;
                user.UserProfile.Address1 = Address;
                user.UserProfile.City = City;
                user.UserProfile.ProvState = Province;
                user.UserProfile.PostZipCode = PostalCode;
                user.UserProfile.Telephone = Telephone;
                user.UserProfile.Country = Country;
                user.UserProfile.UpdateDate = DateTime.Now;

                IdentityResult result = await UserManager.UpdateAsync(user);

                return Json(""); //Need further design, more information to edit for admin, but for users, it should use updateprofile, a separate process?!
            }
            ModelState.AddModelError("", "Something failed.");
            return Json("Error Occured");
        }

        //Receive Ajax call to get profile details
        public async Task<ActionResult> GetProfileDetails()
        {
            //Get current user
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());

            var model = new IndexViewModel
            {
                //HasPassword = HasPassword(),
                //PhoneNumber = await UserManager.GetPhoneNumberAsync(User.Identity.GetUserId()),
                //TwoFactor = await UserManager.GetTwoFactorEnabledAsync(User.Identity.GetUserId()),
                //Logins = await UserManager.GetLoginsAsync(User.Identity.GetUserId()),
                //BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(User.Identity.GetUserId()),


                FirstName = currentUser.UserProfile.FirstName,
                LastName = currentUser.UserProfile.LastName,
                Email = currentUser.UserProfile.Email,
                Address1 = currentUser.UserProfile.Address1,
                City = currentUser.UserProfile.City,
                ProvState = currentUser.UserProfile.ProvState,
                PostZipCode = currentUser.UserProfile.PostZipCode,
                Country = currentUser.UserProfile.Country,
                Telephone = currentUser.UserProfile.Telephone,
                AvatarImgUrl = currentUser.UserProfile.AvatarImgUrl,

            };
            

            return PartialView("_ProfileDetails", model);
        }

        //Receive Ajax call
        public ActionResult GetMerchantDetails(string id)
        {
            //Get current user
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var profileId = UserManager.FindById(id).UserProfile.Id;

            //var currentUser = UserManager.FindById(User.Identity.GetUserId());

            var merchant = _merchantServie.FindSupplierBy(profileId);
            

            return PartialView("_MerchantDetails", merchant);
        }





        //Ajax call to get full name
        public async Task<ActionResult> GetFullName()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());

            var model = new IndexViewModel
            {
                //HasPassword = HasPassword(),
                //PhoneNumber = await UserManager.GetPhoneNumberAsync(User.Identity.GetUserId()),
                //TwoFactor = await UserManager.GetTwoFactorEnabledAsync(User.Identity.GetUserId()),
                //Logins = await UserManager.GetLoginsAsync(User.Identity.GetUserId()),
                //BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(User.Identity.GetUserId()),


                FirstName = currentUser.UserProfile.FirstName,
                LastName = currentUser.UserProfile.LastName,
                //Email = currentUser.UserProfile.Email,
                //Address1 = currentUser.UserProfile.Address1,
                //City = currentUser.UserProfile.City,
                //ProvState = currentUser.UserProfile.ProvState,
                //PostZipCode = currentUser.UserProfile.PostZipCode,
                //Country = currentUser.UserProfile.Country,
                //Telephone = currentUser.UserProfile.Telephone,
                //AvatarImgUrl = currentUser.UserProfile.AvatarImgUrl,

            };


            return PartialView("_Name", model);

        }


        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInAsync(user, isPersistent: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        //
        // POST: /Manage/RememberBrowser
        [HttpPost]
        public ActionResult RememberBrowser()
        {
            var rememberBrowserIdentity = AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(User.Identity.GetUserId());
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, rememberBrowserIdentity);
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/ForgetBrowser
        [HttpPost]
        public ActionResult ForgetBrowser()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/EnableTFA
        [HttpPost]
        public async Task<ActionResult> EnableTFA()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTFA
        [HttpPost]
        public async Task<ActionResult> DisableTFA()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Account/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            // This code allows you exercise the flow without actually sending codes
            // For production use please register a SMS provider in IdentityConfig and generate a code here.
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            ViewBag.Status = "For DEMO purposes only, the current code is " + code;
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Account/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageController.ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // GET: /Account/RemovePhoneNumber
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageController.ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
            }
            return RedirectToAction("Index", new { Message = ManageController.ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Account/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Account/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        [HttpPost]
        public ActionResult UpdateAvatarImg(HttpPostedFileBase file)
        {
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

                        string newFName = user.UserProfile.FirstName + "_" + user.UserProfile.LastName;// + fileExtenstion;

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
                                settings = new ResizeSettings("width=100&height=100&crop=auto&format=png");
                                break;
                            case ".jpg":
                                settings = new ResizeSettings("width=100&height=100&crop=auto&format=jpg");
                                break;
                            case ".jepg":
                                settings = new ResizeSettings("width=100&height=100&crop=auto&format=jpg");
                                break;
                            case ".gif":
                                settings = new ResizeSettings("width=100&height=100&crop=auto&format=gif");
                                break;
                            default:
                                settings = new ResizeSettings("width=100&height=100&crop=auto&format=jpg");
                                break;

                        }

                        ImageBuilder.Current.Build(file, newFileName, settings, false, true);


                        //Update avatarImgUrl in user profile
                        UpdateAvatar(newFName, fileExtenstion);


                        
                    }
                    
                }
            }



            return RedirectToAction("/Index");
        }

        private void UpdateAvatar(string fName, string ext)
        {
            string newUrl = "/Content/Assets/Images/Users/" + fName + ext;

            var user = UserManager.FindById(User.Identity.GetUserId());

            user.UserProfile.AvatarImgUrl = newUrl;

            IdentityResult result = UserManager.Update(user);

        }

        #region Helper

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }


        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}