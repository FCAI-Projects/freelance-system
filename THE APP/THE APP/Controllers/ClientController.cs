using Microsoft.AspNet.Identity;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using THE_APP.Models;

namespace THE_APP.Controllers
{
    [Authorize(Roles = "client")]
    public class ClientController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Client
        public ActionResult Index()
        {
            return View();
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
            if (search != null) {
                return View(db.Posts.ToList().Where(post => post.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
            }
            return View(db.Posts.ToList());
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
            var proposals = db.Proposals.Where(p => p.ClientId == UserId & p.status == null);
            return View(proposals);
        }

        public ActionResult AcceptProposal(int id)
        {
            var post = db.Proposals.Single(p => p.id == id);
            post.status = true;
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