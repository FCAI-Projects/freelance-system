using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using THE_APP.Models;
using THE_APP.ViewModels;

namespace THE_APP.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        ApplicationDbContext db = new ApplicationDbContext();

        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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



        public async Task<ActionResult> Index(HomeViewModel model)
        {
            
            if (!User.Identity.IsAuthenticated) {
                model.Posts = db.Posts.ToList().Where(post => post.isAccepted == true);
                return View(model);
            }

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var roles = await UserManager.GetRolesAsync(user.Id);
            if (roles[0] != "freelancer")
            {
                return RedirectToLocal("/" + roles[0]);
            }
            else {
                model.Posts = db.Posts.ToList().Where(post => post.isAccepted == true);
                return View(model);
            }

        }

        //
        // GET: /Home/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return RedirectToAction("Index");
        }

        //
        // POST: /Home/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(HomeViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                model.Posts = db.Posts.ToList().Where(post => post.isAccepted == true);
                return View("Index", model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.LoginModel.Email, model.LoginModel.Password, model.LoginModel.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.LoginModel.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    model.Posts = db.Posts.ToList().Where(post => post.isAccepted == true);
                    return View("Index", model);
            }
        }

        //
        // GET: /Home/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return RedirectToAction("Index");
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(HomeViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.RegisterModel.Email,
                    Email = model.RegisterModel.Email,
                    Fname = model.RegisterModel.Fname,
                    Lname = model.RegisterModel.Lname
                };
                var result = await UserManager.CreateAsync(user, model.RegisterModel.Password);
                if (result.Succeeded)
                {

                    /* Creating Users Roles
                    var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
                    var roleManager = new RoleManager<IdentityRole>(roleStore);
                    await roleManager.CreateAsync(new IdentityRole("admin"));
                    await roleManager.CreateAsync(new IdentityRole("client"));
                    await roleManager.CreateAsync(new IdentityRole("freelancer"));
                    */


                    await UserManager.AddToRoleAsync(user.Id, model.RegisterModel.Role);

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            model.Posts = db.Posts.ToList().Where(post => post.isAccepted == true);
            return View("Index", model);
        }

        public PartialViewResult LoginPartial() {
            return PartialView("_LoginPartial");
        }

        public ActionResult Details(int id)
        {

            HomeViewModel model = new HomeViewModel();
            model.SinglePost = db.Posts.ToList().Single(post => post.Id == id);
            model.SinglePost.Client = db.Users.ToList().Single(user => user.Id == model.SinglePost.ClientId);
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
}
