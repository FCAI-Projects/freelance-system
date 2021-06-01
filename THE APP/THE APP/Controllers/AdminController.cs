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
      AdminViewModel adminView = new AdminViewModel
      {
        Fname = user.Fname,
        Lname = user.Lname,
        Email = user.Email,
        Number = user.PhoneNumber
      };

      Admin admin = new Admin
      {
        AdminViewModel = adminView
      };

      return View(admin);
    }

    public async Task<ActionResult> UpdateData(Admin data)
    {
      var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

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

    public ActionResult AllPost()
    {
      return View(db.Posts.ToList());
    }

    [HttpGet]
    public ActionResult EditPost(int id)
    {
      return View(db.Posts.Single(p => p.Id == id));
    }

    [HttpPost]
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
  }
}