using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace THE_APP.Controllers
{
    [Authorize(Roles = "freelancer")]
    public class FreelancerController : Controller
    {
        // GET: Freelancer
        public ActionResult Index()
        {
            return Redirect("/");
        }
    }
}