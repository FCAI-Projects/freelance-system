using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using THE_APP.Models;

namespace THE_APP.ViewModels
{
    public class HomeViewModel
    {
        public RegisterViewModel RegisterModel { get; set; }
        public LoginViewModel LoginModel { get; set; }
        public IEnumerable<PostModel> Posts { get; set; }
        public PostModel SinglePost { get; set; }
    }
}