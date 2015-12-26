using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CommonMarket.Web.Models
{
    public class IdentityRepository : IDisposable
    {
        private ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;
        //private RoleManager<IdentityRole> _roleManager;

        private IdentityRepository()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_context));
            //_roleManager = new RoleManager<IdentityRole>(new UserStore<IdentityUser>(_context));
        }



        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}