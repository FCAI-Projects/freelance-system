using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace THE_APP.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You have enter job Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "You have enter job type")]
        [Display(Name= "JobType")]
        public string Type { get; set; }

        [Required(ErrorMessage = "You have enter Budget")]
        public int Budget { get; set; }

        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "You have enter job Description")]
        public string Description { get; set; }

        public int ProposalNum { get; set; } = 0;

        public bool? isAccepted { get; set; }

        public bool? AccpeptProposal { get; set; }

        public virtual ApplicationUser Client { get; set; }

        public string ClientId { get; set; }
    }
}