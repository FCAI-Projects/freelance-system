using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THE_APP.Models;

namespace THE_APP.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
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