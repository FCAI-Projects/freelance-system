using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THE_APP.Models;
using THE_APP.ViewModels;

namespace THE_APP.Controllers
{
    [Authorize(Roles = "freelancer")]
    public class FreelancerController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ApplicationDbContext db = new ApplicationDbContext();

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

        // GET: Freelancer
        public ActionResult Index()
        {
            return Redirect("/");
        }

        [HttpPost]
        public ActionResult SendProposal(HomeViewModel model) {
            var user = UserManager.FindByIdAsync(User.Identity.GetUserId());
            model.Proposal.FreelancerId = User.Identity.GetUserId();
            model.Proposal.PostId = model.SinglePost.Id;
            model.Proposal.ClientId = model.SinglePost.ClientId;
            model.Proposal.CreationDate = DateTime.Now;
            db.Proposals.Add(model.Proposal);

            var post = db.Posts.Single(p => p.Id == model.SinglePost.Id);
            post.ProposalNum++;
            db.SaveChanges();

            return Redirect("/");
        }
    }
}