using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Account
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Nullable<bool> IsAuthenticated { get; set; }
    }
}