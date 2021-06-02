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

        public ActionResult AllPost()
        {
            return View(db.Posts.ToList());
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