﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace THE_APP.Models
{
    public class PostsRate
    {
        public int Id { get; set; }

        public virtual PostModel Post { get; set; }

        public int PostId { get; set; }

        public virtual ApplicationUser Freelancer { get; set; }

        public string FreelancerId { get; set; }

        public int Rate { get; set; }
    }
}