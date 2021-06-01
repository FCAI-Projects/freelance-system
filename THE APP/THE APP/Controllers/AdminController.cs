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