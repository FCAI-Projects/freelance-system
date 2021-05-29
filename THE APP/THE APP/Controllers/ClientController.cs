using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace THE_APP.Controllers
{
    public class ClientController : Controller
    {
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

        public ActionResult Propsals()
        {
            return View();
        }

    }
}