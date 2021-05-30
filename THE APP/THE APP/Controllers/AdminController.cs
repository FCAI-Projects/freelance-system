using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using THE_APP.Models;
using THE_APP.ViewModels;

namespace THE_APP.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            return RedirectToAction("Profile");
        }

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

        public async Task<ActionResult> Profile()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            AdminViewModel admin = new AdminViewModel { 
                Fname = user.Fname,
                Lname = user.Lname,
                Email = user.Email,
                Number = user.PhoneNumber
            };

            return View(admin);
        }

        public async Task<ActionResult> Update(AdminViewModel data)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            user.Fname = data.Fname;
            user.Lname = data.Lname;
            user.Email = data.Email;
            user.PhoneNumber = data.Number;

            var res = UserManager.Update(user);

            if (res.Succeeded) return Json(new { res = 1 });
            else return Json(new { res = user });


        }
    }
}