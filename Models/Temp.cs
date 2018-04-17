using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Temp
    {
        public int PostID { get; set; }
        public string PostName { get; set; }
        public DateTime PostTime { get; set; }
        public string PostDetails { get; set; }
        public List<Comment> CommentsList { get; set; }

    }
}