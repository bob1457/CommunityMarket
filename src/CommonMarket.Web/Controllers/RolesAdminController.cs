using CommonMarket.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;

namespace CommonMarket.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesAdminController : BaseController
    {
        public RolesAdminController()
        {
        }

        public RolesAdminController(ApplicationUserManager userManager,
            ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
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

        //
        // GET: /Roles/
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        //Ajax get all roles
        public ActionResult GetAllRoles()
        {
            var roles = RoleManager.Roles;

            return PartialView("_RoleList", roles);//Json(roles.ToList(), JsonRequestBehavior.AllowGet); //
        }


        //
        // GET: /Roles/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            // Get the list of Users in this Role
            var users = new List<ApplicationUser>();

            // Get the list of Users in this Role
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();
            return View(role);
        }

        //
        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                //var role = new IdentityRole(roleViewModel.Name);
                var role = new ApplicationRole(roleViewModel.Name); //Custom role attribute/property
                // Save the new Description property:
                role.Description = roleViewModel.Description;

                var roleresult = await RoleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        //Ajax adding role
        [HttpPost]
        public async Task<JsonResult> AddRole(string roleName, string roleDesc)
        {
            var role = new ApplicationRole(roleName);
            role.Description = roleDesc;

            var roleresult = await RoleManager.CreateAsync(role);

            if (!roleresult.Succeeded)
            {
                //ModelState.AddModelError("", roleresult.Errors.First());
                return Json("Error! Please try it again!");
            }
            return Json("The Role " + role + " has been added successfully!");
        }





        //
        // GET: /Roles/Edit/Admin
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            RoleViewModel roleModel = new RoleViewModel { Id = role.Id, Name = role.Name, Description = role.Description };
            // Update the new Description property for the ViewModel:
            roleModel.Description = role.Description;

            return View(roleModel);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Name,Id, Description")] RoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(roleModel.Id);
                role.Name = roleModel.Name;
                role.Description = roleModel.Description;
                await RoleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
            return View();
        }

        //Ajax update 
        public JsonResult Update(string Id, string Name, string Desc )
        {
            if (ModelState.IsValid)
            {
                var role = RoleManager.FindById(Id);
                role.Name = Name;
                role.Description = Desc;
                RoleManager.Update(role);
                return Json("Updated successfully!");
            }
            return Json("Error occured!");
        }



        //
        // GET: /Roles/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // POST: /Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id, string deleteUser)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var role = await RoleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                IdentityResult result;
                if (deleteUser != null)
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                else
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }


        public JsonResult GetRoleList()
        {
            var roles = RoleManager.Roles;

            return Json(roles, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult AddUserToRole( string UserEmail, string RoleName)
        {
            var user = UserManager.FindByEmail(UserEmail);

            if (user != null)
            {
                string userid = user.Id;

                UserManager.AddToRole(user.Id, RoleName);

                return Json("Added successfully!");
            }
            else
            {
                return Json("User Not Found!");
            }
            
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult RemoveUserFromRole(string UserEmail, string RoleName)
        {
            var user = UserManager.FindByEmail(UserEmail);

            if (user != null)
            {
                string userid = user.Id;

                UserManager.RemoveFromRole(user.Id, RoleName);

                return Json("Removed successfully!");
            }
            else
            {
                return Json("User Not Found!");
            }
            
        }
        
    }
}
