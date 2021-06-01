using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THE_APP.Models;

namespace THE_APP.Controllers
{
     
    [Authorize(Roles = "admin")]    
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Admin
        public ActionResult Index()
        {   
            return View();
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
    }
}