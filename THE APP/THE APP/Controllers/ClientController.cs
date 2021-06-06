using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using THE_APP.Models;
using THE_APP.ViewModels;

namespace THE_APP.Controllers
{
    [Authorize(Roles = "client")]
    public class ClientController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ApplicationDbContext db = new ApplicationDbContext();

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

        // GET: Client
        public ActionResult Index()
        {
            return RedirectToAction("Profile");
        }

        public async Task<ActionResult> Profile()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            AdminViewModel adminView = new AdminViewModel
            {
                Fname = user.Fname,
                Lname = user.Lname,
                Username = user.UserName,
                Email = user.Email,
                Number = user.PhoneNumber,
                PhotoPath = user.PhotoPath
            };

            Admin admin = new Admin
            {
                AdminViewModel = adminView
            };

            return View(admin);
        }

        public async Task<ActionResult> UpdateData(Admin data, HttpPostedFileBase file)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (file != null)
            {
                Random rnd = new Random();
                string extension = Path.GetExtension(file.FileName);
                string newName = rnd.Next(0, 15153515) + DateTime.Now.Millisecond.ToString() + extension;
                file.SaveAs(HttpContext.Server.MapPath("~/Uploads/") + newName);
                user.PhotoPath = newName;
            }
            else {
                user.PhotoPath = user.PhotoPath;
            }
            user.UserName = data.AdminViewModel.Username;
            user.Fname = data.AdminViewModel.Fname;
            user.Lname = data.AdminViewModel.Lname;
            user.Email = data.AdminViewModel.Email;
            user.PhoneNumber = data.AdminViewModel.Number;

            var res = UserManager.Update(user);

            if (res.Succeeded) return Json(new { res = 1 });
            else return Json(new { res = user });
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePassword(Admin data)
        {

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (data.AdminChangePasswordViewModel.Password == data.AdminChangePasswordViewModel.ConfirmPassword)
            {
                var res = UserManager.ChangePassword(user.Id, data.AdminChangePasswordViewModel.OldPassword, data.AdminChangePasswordViewModel.Password);

                if (res.Succeeded) return Json(new { res = 1 });
                else
                {
                    return Json(new { res = res });
                }
            }
            else
            {
                return Json(new { res = 2 });
            }


        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewPost()
        { 
            return View();
        }

        [HttpGet]
        public ActionResult EditPost(int id)
        {
            return View(db.Posts.Single(p => p.Id == id));
        }

        [HttpPost]
        public ActionResult NewPost(PostModel pm)
        {
            if (!ModelState.IsValid) {
                return View("NewPost", pm);
            }
            pm.ClientId = User.Identity.GetUserId();
            pm.CreationDate = DateTime.Now;
            db.Posts.Add(pm);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult AllPost(string search = null)
        {
            string UserId = User.Identity.GetUserId();
            if (search != null) {
                return View(db.Posts.ToList().Where(post => post.ClientId == UserId && post.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
            }
            return View(db.Posts.ToList().Where(post => post.ClientId == UserId));
        }

        public ActionResult EditPost(PostModel pm)
        {
            if (!ModelState.IsValid)
            {
                return View("EditPost", pm);
            }

            var post = db.Posts.Single(p => p.Id == pm.Id);
            post.Title = pm.Title;
            post.Type = pm.Type;
            post.Budget = pm.Budget;
            post.Description = pm.Description;

            db.SaveChanges();

            return RedirectToAction("AllPost");

        }

        [HttpGet]
        public ActionResult DeletePost(int id)
        {
            var post = db.Posts.Single(p => p.Id == id);
            db.Posts.Remove(post);
            db.SaveChanges();

            return RedirectToAction("AllPost");
        }

        public ActionResult Proposals()
        {
            string UserId = User.Identity.GetUserId();
            var proposals = db.Proposals.ToList().Where(p => p.ClientId == UserId & p.status == null);
            foreach (var proposal in proposals) {
                proposal.Post = db.Posts.Single(post => post.Id == proposal.PostId);
            }
            return View(proposals);
        }

        public ActionResult AcceptProposal(int id, int postId)
        {
            var proposal = db.Proposals.Single(p => p.id == id);
            proposal.status = true;
            db.SaveChanges();
            var post = db.Posts.Single(p => p.Id == postId);
            post.AccpeptProposal = true;
            db.SaveChanges();

            return RedirectToAction("Proposals");
        }
        public ActionResult RejectProposal(int id)
        {
            var post = db.Proposals.Single(p => p.id == id);
            post.status = false;
            db.SaveChanges();
            return RedirectToAction("Proposals");
        }
    }
}