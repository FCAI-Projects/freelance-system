using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
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



        public async Task<ActionResult> Index(HomeViewModel model, string search = null)
        {
            if (search != null) {
                model.Posts = db.Posts.ToList().Where(post => post.isAccepted == true);
                foreach (var m in model.Posts) {
                    m.Client = db.Users.Single(u => u.Id == m.ClientId);
                }
                model.Posts = model.Posts.Where(post => post.Title.Contains(search) || post.Client.Fname.Contains(search) || post.Client.Lname.Contains(search) || post.CreationDate.ToString().Contains(search));
                return View(model);
            }
            
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
                var UserId = User.Identity.GetUserId();
                model.Posts = db.Posts.ToList().Where(post => post.isAccepted == true);
                model.SavedPosts = db.SavedPosts.ToList().Where(post => post.FreelancerId == UserId);
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
        public async Task<ActionResult> Register(HomeViewModel model, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                string fileName = "";

                if (file != null)
                {
                    Random rnd = new Random();
                    string extension = Path.GetExtension(file.FileName);
                    string newName = rnd.Next(0, 15153515) + DateTime.Now.Millisecond.ToString() + extension;
                    file.SaveAs(HttpContext.Server.MapPath("~/Uploads/") + newName);
                    fileName = newName;
                }
                else
                {
                    fileName = "default.png";
                }

                var user = new ApplicationUser
                {
                    UserName = model.RegisterModel.Email,
                    Email = model.RegisterModel.Email,
                    Fname = model.RegisterModel.Fname,
                    Lname = model.RegisterModel.Lname,
                    PhoneNumber = model.RegisterModel.Number,
                    PhotoPath = fileName
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
            var UserId = User.Identity.GetUserId();
            HomeViewModel model = new HomeViewModel
            {
                SinglePost = db.Posts.ToList().SingleOrDefault(post => post.Id == id)
            };

            model.Rate = db.PostsRate.SingleOrDefault(r => r.FreelancerId == UserId && r.PostId == model.SinglePost.Id);
            model.SinglePost.Client = db.Users.ToList().SingleOrDefault(user => user.Id == model.SinglePost.ClientId);
            return View(model);
        }

        public ActionResult SavePost(int id) {
            var UserId = User.Identity.GetUserId();
            SavedPostsModel Saved = new SavedPostsModel
            {
                PostId = id,
                FreelancerId = UserId
            };
            db.SavedPosts.Add(Saved);
            db.SaveChanges();
            return RedirectToAction("SavedPosts", "Freelancer");
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
