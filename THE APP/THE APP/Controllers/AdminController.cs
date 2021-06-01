﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using THE_APP.Models;

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

    public ActionResult Users()
    {
        return View(db.Users.ToList());
    }
    public ActionResult ViewUser(String id)
    {
        var User = db.Users.ToList().Single(user => user.Id == id);
        if (User == null)
        {
            return HttpNotFound();
        }
        return View(User);
    }
    public ActionResult Delete(String id)
    {
        var User = db.Users.Single(x => x.Id == id);
        if (User == null)
        {
            return HttpNotFound();
        }
        return View(User);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfiremed(String id)
    {
        var User = db.Users.Find(id);
        db.Users.Remove(User);
        db.SaveChanges();
        return RedirectToAction("Users");
    }

    [HttpGet]
    public ActionResult Edit(String id)
    {
        var User = db.Users.Single(x => x.Id == id);

        if (User == null)
            return HttpNotFound();

        return View(User);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(ApplicationUser user)
    {
        if (ModelState.IsValid)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Users");
        }
        return View(User);
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
    [HttpGet]
    public ActionResult IsAccepted()
    {

      return View(db.Posts.ToList().Where(p => p.isAccepted == null));
    }
    public ActionResult Accsept(int id)
    {
      var post = db.Posts.Single(p => p.Id == id);
      post.isAccepted = true;
      db.SaveChanges();
      return RedirectToAction("IsAccepted");
    }
    public ActionResult Rigect(int id)
    {
      var post = db.Posts.Single(p => p.Id == id);
      post.isAccepted = false;
      db.SaveChanges();
      return RedirectToAction("IsAccepted");
    }
  }
}
