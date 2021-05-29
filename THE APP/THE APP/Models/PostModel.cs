using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace THE_APP.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        public JobType Type { get; set; }

        public int Budget { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

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