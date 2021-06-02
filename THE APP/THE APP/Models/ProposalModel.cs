using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace THE_APP.Models
{
    public class ProposalModel
    {
        public int id { get; set; }

        public string Proposal { get; set; }

        public ApplicationUser Freelancer { get; set; }
        public string FreelancerId { get; set; }

        public ApplicationUser Client { get; set; }
        public string ClientId { get; set; }

        public PostModel Post { get; set; }
        public int PostId { get; set; }

        public DateTime CreationDate { get; set; }

        public bool? status { get; set; }
    }
}