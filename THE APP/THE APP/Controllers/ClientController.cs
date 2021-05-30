using Microsoft.Graph;
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
            db.Posts.Add(pm);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult AllPost()
        {
            return View();
        }

        public ActionResult Propsals()
        {
            return View();
        }
    }
}