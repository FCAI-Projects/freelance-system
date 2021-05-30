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
        public JobType Type { get; set; }

        [Required(ErrorMessage = "You have enter Budget")]
        public int Budget { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreationDate { get; set; }

        [Required(ErrorMessage = "You have enter job Description")]
        public string Description { get; set; }

        public int ProposalNum { get; set; }

        public bool isAccepted { get; set; }

        public ApplicationUser Client { get; set; }
    }

    public enum JobType
    {
        fixedType,
        hourlyType
    }
}