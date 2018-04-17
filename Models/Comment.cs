using System;

namespace Blog.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public String UserName { get; set; }
        public string CommentBody { get; set; }
        public DateTime Time { get; set; }
        public int PostId { get; set; }
    }
}